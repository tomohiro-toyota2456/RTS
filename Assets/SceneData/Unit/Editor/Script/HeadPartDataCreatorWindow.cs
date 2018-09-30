//************************************************
//HeadPartDataCreatorWindow.cs
//Author y-harada
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//************************************************
//HeadPartDataCreatorWindow
//************************************************
public class HeadPartDataCreatorWindow : EditorWindow
{
	static ReorderableListEditorWindow<HeadPartData> win = new ReorderableListEditorWindow<HeadPartData>();
	static HeadPartDataCreatorWindow window;
	[MenuItem("MECHANIC/UnitPartData/OpenHeadPartWindow")]
	public static void Open()
	{
		window = GetWindow<HeadPartDataCreatorWindow>();
		win.FilePath = FilePathConfig.HeadPartDataPath;
	}

	private void OnEnable()
	{
		if(window == null)
		{
			window = GetWindow<HeadPartDataCreatorWindow>();
		}
	}

	private void OnGUI()
	{
		win.OnGUI();
	}
}
