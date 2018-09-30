//***********************************************
//SkillTreeEditorWindow.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;

//***********************************************
//SkillTreeEditorWindow
//***********************************************
public class SkillTreeEditorWindow : EditorWindow
{
	static EditorWindow window;

	static public EditorWindow Window
	{
		get
		{
			if(window == null)
			{
				window = EditorWindow.GetWindow<SkillTreeEditorWindow>();
			}

			return window;
		}
	}

	//管理用データ
	public class EditorNode
	{
		public int skillId;
		public List<EditorNode> children;
		public List<Conditions> parents;
		public SkillTreeEditorNodeDrawer nodeDrawer;
	}
	
	public class Conditions
	{
		public int lv;
		public EditorNode node;
	}

	List<EditorNode> nodes = new List<EditorNode>();

	public enum MouseEvent
	{
		None = -1,
		EVENT = 0,
		OpenContextMenu =1,
		SelectContextMenu =200,
		ConnectSelect,
		DisConnectSelect,
	}

	public enum MenuEvent
	{
		None,
		ConnectSelect,
		DisConnectSelect,
	}

	//Eventで使用
	MouseEvent currentMouseEvent = MouseEvent.None;
	MenuEvent currentMenuEvent = MenuEvent.None;
	EditorNode currentEventTarget = null;
	EditorNode connectionTarget = null;

	[MenuItem("MECHANIC/SkillTreeEditor/OpenWindow")]
	static void OpenWindow()
	{
		window = EditorWindow.GetWindow<SkillTreeEditorWindow>();
		window.minSize = new Vector2(500, 500);
	}

	//AnimatorWindow出す用
	Graph stateMachineGraph;
	GraphGUI stateMachineGraphGUI;

	private void OnEnable()
	{
		if (stateMachineGraph == null)
		{
			stateMachineGraph = ScriptableObject.CreateInstance<Graph>();
			stateMachineGraph.hideFlags = HideFlags.HideAndDontSave;
		}
		if (stateMachineGraphGUI == null)
		{
			stateMachineGraphGUI = (GetEditor(stateMachineGraph));
		}
	}

	GraphGUI GetEditor(Graph graph)
	{
		GraphGUI graphGUI = CreateInstance("GraphGUIEx") as GraphGUI;
		graphGUI.graph = graph;
		graphGUI.hideFlags = HideFlags.HideAndDontSave;
		return graphGUI;
	}

	
	private void OnGUI()
	{
		//左側ツールチップ
		using (new EditorGUILayout.VerticalScope())
		{
			GUILayout.BeginArea(new Rect(0,0,200,500));

			if (GUILayout.Button("Save"))
			{
				Save();
			}

			if (GUILayout.Button("Load"))
			{
				Load();
			}

			if (GUILayout.Button("Export"))
			{
				Export();
			}

			if (GUILayout.Button("CreateNode"))
			{
				EditorNode editorNode = new EditorNode();
				editorNode.nodeDrawer = new SkillTreeEditorNodeDrawer();

				nodes.Add(editorNode);
			}

			GUILayout.EndArea();
		}

		//右側　ツリー構造
		if (window && stateMachineGraphGUI != null)
		{
			stateMachineGraphGUI.BeginGraphGUI(window, new Rect(200, 0,window.position.width,window.position.height));
			var ev = Event.current;
			//stateMachineGraphGUI.OnGraphGUI();
			BeginWindows();

			//データの中にある描画用クラスで描画
			for(int i = 0; i < nodes.Count;i++)
			{
				Color color = Color.gray;

				//選択されている場合は色を変更
				if (nodes[i] == currentEventTarget)
				{
					color = Color.red;
				}

				//データは今は仮
				nodes[i].nodeDrawer.DrawWindow(i,"aaaaaa", "aaaaa",color);

				//右クリックイベントでマウスイベントがおきてなければ
				if(nodes[i].nodeDrawer.mousebutton == 1 && currentMouseEvent == MouseEvent.None)
				{
					//選択オブジェクトセット
					currentEventTarget = nodes[i];
				}

				//メニューイベントを実行
				if(ActionMenuEvent(nodes[i],nodes[i].nodeDrawer.mousebutton,ev))
				{
					//trueなら実行なのでcurrent系をリセット
					currentMenuEvent = MenuEvent.None;
					currentMouseEvent = MouseEvent.None;
					currentEventTarget = null;
				}

			}

			//マウスイベント実行
			ActionMouseEvent(ev);

			EndWindows();

			//親子関係描画
			DrawLine();

			stateMachineGraphGUI.EndGraphGUI();
		}


	}

	//親子関係描画
	void DrawLine()
	{
		for(int i = 0; i < nodes.Count;i++)
		{
			if(nodes[i].children != null)
			{
				for(int j = 0; j < nodes[i].children.Count;j++)
				{
					Vector2 top = nodes[i].children[j].nodeDrawer.TopPosition;
					Vector2 bottom = nodes[i].nodeDrawer.BottomPosition;

					Handles.DrawLine(nodes[i].nodeDrawer.BottomPosition, nodes[i].children[j].nodeDrawer.TopPosition);

					var cond = nodes[i].children[j].parents.Find(_ => { return _.node == nodes[i]; });

					cond.lv = EditorGUI.IntField(new Rect((top.x + bottom.x) / 2, (top.y + bottom.y) / 2, 100, 25),cond.lv);
				}
			}
		}
	}

	//マウスイベント
	//ウィンドウでのイベントとその外でのイベントを取っているのは両方が別のイベントを指すので両方みないと不都合
	public void ActionMouseEvent(Event ev)
	{
		if (currentEventTarget == null)
			return;

		int buttonNum = currentEventTarget.nodeDrawer.mousebutton;

		//ボタンのナンバーとEnumをあわせてある
		if (currentMouseEvent == MouseEvent.None)
		{
			if (buttonNum <= 1)
			{
				currentMouseEvent = (MouseEvent)buttonNum;
			}
		}

		switch(currentMouseEvent)
		{
			case MouseEvent.EVENT:
				currentMouseEvent = MouseEvent.None;//0はないので今はこうなっている
				break;
			case MouseEvent.OpenContextMenu://メニューオープン
				GenericMenu menu = new GenericMenu();

				menu.AddItem(new GUIContent("Delete"), false, (_)=> { DeleteNode(); }, "item 1");
				menu.AddItem(new GUIContent("Connect"), false, (_) => { ConnectNodeEvent(); }, "item 2");
				menu.AddItem(new GUIContent("DisConnect"), false, (_) => { DisConnectNodeEvent(); }, "item3");
				menu.ShowAsContext();
				currentMouseEvent = MouseEvent.SelectContextMenu;
				break;

			case MouseEvent.SelectContextMenu:

				//ウィンドウ外やメニュー外でクリックがあるのでチェック
				//大体メニューを選ぶと２フレーム後くらいに関数が動く
				//操作が早すぎるってことが無い限りバグらないと思う・・・
				if(ev.type == EventType.MouseDown || buttonNum != -1)
				{
					currentMouseEvent = MouseEvent.None;
				}

				break;
		}
		
	}

	//メニュー時のイベント
	public bool ActionMenuEvent(EditorNode node, int mousebutton, Event ev)
	{
		if (currentMenuEvent == MenuEvent.None)
			return false;

		switch(currentMenuEvent)
		{
			case MenuEvent.ConnectSelect:

				if(mousebutton == 0)
				{
					ConnectNode(currentEventTarget, node);
					return true;
				}
				else if(mousebutton != -1 || (ev.type == EventType.MouseDown))
				{
					return true;
				}

				break;

			case MenuEvent.DisConnectSelect:

				if (mousebutton == 0)
				{
					DisConnectNode(currentEventTarget, node);
					return true;
				}
				else if (mousebutton != -1 || (ev.type == EventType.MouseDown))
				{
					return true;
				}

				break;
		}

		return false;

	}

	//ノード削除
	void DeleteNode()
	{
		//親子関係切
		if (currentEventTarget.parents != null)
		{
			for (int i = 0; i < currentEventTarget.parents.Count; i++)
			{
				currentEventTarget.parents[i].node.children.Remove(currentEventTarget);
			}
		}

		if(currentEventTarget.children != null)
		{
			for(int i = 0; i < currentEventTarget.children.Count; i++)
			{
				int idx = currentEventTarget.children[i].parents.FindIndex((_)=> { return _.node == currentEventTarget; });

				if(idx != -1)
				{
					currentEventTarget.children[i].parents.RemoveAt(idx);
				}

			}
		}



		//デリート
		nodes.Remove(currentEventTarget);
		currentEventTarget = null;
		currentMouseEvent = MouseEvent.None;
	}

	void ConnectNodeEvent()
	{
		currentMenuEvent = MenuEvent.ConnectSelect;
		currentMouseEvent = MouseEvent.ConnectSelect;
	}

	void DisConnectNodeEvent()
	{
		currentMenuEvent = MenuEvent.DisConnectSelect;
		currentMouseEvent = MouseEvent.DisConnectSelect;
	}

	//ノード接続
	void ConnectNode(EditorNode parent,EditorNode child)
	{
		//親＝子供は許さない
		if(parent == child)
		{
			return;
		}

		//初期化済み出ない場合初期化
		if (parent.children == null)
		{
			parent.children = new List<EditorNode>();
		}

		if(parent.parents == null)
		{
			parent.parents = new List<Conditions>();
		}

		if(child.parents == null)
		{
			child.parents = new List<Conditions>();
		}

		if(child.children == null)
		{
			child.children = new List<EditorNode>();
		}

		//すでに存在している場合も許さない
		int idx1 = parent.children.FindIndex((_)=>_ == child);
		int idx2 = child.children.FindIndex((_) => _ == parent);

		if(idx1 != -1 || idx2 != -1)
		{
			return;
		}

		parent.children.Add(child);

		Conditions cond = new Conditions();
		cond.lv = 1;
		cond.node = parent;

		child.parents.Add(cond);
	}

	//接続削除
	void DisConnectNode(EditorNode parent,EditorNode child)
	{
		if (parent == child)
		{
			return;
		}

		if (parent.children == null)
		{
			parent.children = new List<EditorNode>();
		}

		if (parent.parents == null)
		{
			parent.parents = new List<Conditions>();
		}

		if (child.parents == null)
		{
			child.parents = new List<Conditions>();
		}

		if (child.children == null)
		{
			child.children = new List<EditorNode>();
		}

		parent.children.Remove(child);
		int idx = child.parents.FindIndex((_) => _.node == parent);

		if(idx != -1)
		{
			child.parents.RemoveAt(idx);
		}

	}

	//Editor保存
	void Save()
	{
		string path = EditorUtility.SaveFilePanelInProject("保存","EditorTree","asset","保存");

		if(string.IsNullOrEmpty(path))
		{
			return;
		}

		var saveIns = CreateInstance<EditorSkillTreeData>();

		List<EditorSkillTreeData.EditorNodeData> list = new List<EditorSkillTreeData.EditorNodeData>(); 

		for(int i = 0; i < nodes.Count; i++)
		{
			list.Add(ConvertFromEditorNode(nodes[i]));
		}

		saveIns.List = list;

		AssetDatabase.CreateAsset(saveIns, path);
	}

	//Editor読み出し
	void Load()
	{
		string path = EditorUtility.OpenFilePanel("読み込み", Application.dataPath, "asset");

		if (string.IsNullOrEmpty(path))
		{
			return;
		}

		nodes = new List<EditorNode>();

		path = path.Replace(Application.dataPath, "");

		path = "Assets" + path;

		var ins = AssetDatabase.LoadAssetAtPath<EditorSkillTreeData>(path);

		//作成
		//先にデータを作る
		for(int i = 0; i < ins.List.Count;i++)
		{
			var node = new EditorNode();
			node.skillId = ins.List[i].skillDataId;
			node.nodeDrawer = new SkillTreeEditorNodeDrawer();
			node.nodeDrawer.Position = ins.List[i].position;
			nodes.Add(node);
		}

		//親子関係生成
		//idx保存なので一気に接続
		for(int i = 0; i < ins.List.Count;i++)
		{
			nodes[i].children = new List<EditorNode>();
			nodes[i].parents = new List<Conditions>();

			for(int j = 0; j < ins.List[i].children.Count; j++)
			{
				int idx = ins.List[i].children[j];
				nodes[i].children.Add(nodes[idx]);
			}

			for (int j = 0; j < ins.List[i].parents.Count; j++)
			{
				int idx = ins.List[i].parents[j].idx;

				Conditions cond = new Conditions();
				cond.node = nodes[idx];
				cond.lv = ins.List[i].parents[j].lv;
				nodes[i].parents.Add(cond);
			}
		}
	}

	//変換
	EditorSkillTreeData.EditorNodeData ConvertFromEditorNode(EditorNode node)
	{
		EditorSkillTreeData.EditorNodeData nodeData = new EditorSkillTreeData.EditorNodeData();
		nodeData.position = node.nodeDrawer.Position;
		nodeData.skillDataId = node.skillId;

		//元データと順番を変えないのでそのままidx保存する
		if (node.parents != null)
		{
			nodeData.parents = new List<EditorSkillTreeData.Conditions>();
			for (int i = 0; i < node.parents.Count; i++)
			{
				int idx = nodes.FindIndex((_) => _ == node.parents[i].node);
				EditorSkillTreeData.Conditions conditions = new EditorSkillTreeData.Conditions();
				conditions.lv = node.parents[i].lv;
				conditions.idx = idx;
				nodeData.parents.Add(conditions);
			}
		}

		if(node.children != null)
		{
			nodeData.children = new List<int>();
			for(int i = 0; i < node.children.Count;i++)
			{
				int idx = nodes.FindIndex((_) => _ == node.children[i]);
				nodeData.children.Add(idx);
			}
		}

		return nodeData;

	}

	//スキルツリー吐き出し
	void Export()
	{
		string path = EditorUtility.SaveFilePanelInProject("出力", "SkillTree", "asset", "保存");

		if (string.IsNullOrEmpty(path))
		{
			return;
		}

		List<int> rootIdxs = new List<int>();
		List<SkillTreeData.Node> nodeDataList = new List<SkillTreeData.Node>();
		for (int i = 0; i < nodes.Count; i++)
		{
			SkillTreeData.Node node = new SkillTreeData.Node();
			node.skillDataId = nodes[i].skillId;

			if(nodes[i].parents != null)
			node.conditions = new SkillTreeData.Conditions[nodes[i].parents.Count];

			for (int j = 0; j < nodes[i].parents.Count; j++)
			{
				node.conditions[j].lv = nodes[i].parents[j].lv;
				node.conditions[j].skillDataId = nodes[i].parents[j].node.skillId;
			}

			if (node.conditions == null || node.conditions.Length == 0)
			{
				rootIdxs.Add(nodeDataList.Count);
			}

			nodeDataList.Add(node);
		}


		for (int i = 0; i < nodes.Count; i++)
		{
			if (nodes[i].children != null)
			{
				nodeDataList[i].childIdxs = new int[nodes[i].children.Count];
				for (int j = 0; j < nodes[i].children.Count; j++)
				{
					int idx = nodes.FindIndex((_) => _ == nodes[i].children[j]);
					nodeDataList[i].childIdxs[j] = idx;
				}
			}
		}

		var ins = CreateInstance<SkillTreeData>();
		ins.Id = 0;
		ins.RootIdxs = rootIdxs.ToArray();
		ins.Nodes = nodeDataList.ToArray();

		AssetDatabase.CreateAsset(ins, path);
	}



	private void OnDisable()
	{
		window = null;
	}

	
}
