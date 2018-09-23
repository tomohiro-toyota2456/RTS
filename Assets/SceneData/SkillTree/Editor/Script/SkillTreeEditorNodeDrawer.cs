using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillTreeEditorNodeDrawer
{
	static readonly Vector2 windowSize = new Vector2(250, 150);
	Rect windowRect = new Rect(new Vector2(200,100), windowSize);

	public Vector2 TopPosition
	{
		get
		{
			Vector2 ans = windowRect.position;
			ans.x += windowRect.width / 2;
			return ans;
		}
	}

	public Vector2 BottomPosition
	{
		get
		{
			Vector2 ans = windowRect.position;
			ans.x += windowRect.width / 2;
			ans.y += windowRect.height;
			return ans;
		}
	}

	public Vector2 Position { get { return windowRect.position; } set { windowRect.position = value; } }

	string skillName;
	string skillDataDesc;
	public void DrawWindow(int id,string skillName,string skillDataDesc,Color color)
	{
		this.skillName = skillName;
		this.skillDataDesc = skillDataDesc;

		Color prev = GUI.backgroundColor;

		GUI.backgroundColor = color;
		windowRect = GUI.Window(id,windowRect, DrawNodeWindow, "Window");
		GUI.backgroundColor = prev;
	}

	void DrawNodeWindow(int id)
	{
		var ev = Event.current;
		mousebutton = -1;
		if (ev.type == EventType.MouseDown )
		{
			mousebutton = ev.button;
		}

		GUI.DragWindow();
		EditorGUILayout.LabelField("スキル名", skillName);
		EditorGUILayout.LabelField("説明", skillDataDesc);
	}

	public int mousebutton = -1;

	public bool CheckHit(Vector2 mousePosition)
	{
		Debug.Log(mousePosition);
		Debug.Log(windowRect.position);

		if(mousePosition.x >= windowRect.position.x && mousePosition.x <= windowRect.position.x + windowRect.width)
		{
			if(mousePosition.y >= windowRect.position.y && mousePosition.y <= windowRect.position.y + windowRect.height)
			{
				return true;
			}
		}

		return false;
	}
}
