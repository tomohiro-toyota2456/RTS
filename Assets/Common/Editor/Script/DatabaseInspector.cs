using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(DatabaseManager))]
public class DatabaseInspector : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		DatabaseManager databaseManager = (DatabaseManager)this.target;

		if (GUILayout.Button("DataSet"))
		{
			var nowScene = EditorSceneManager.GetActiveScene();

			var headPartDatabase = databaseManager.GetHeadPartDatabase();
			headPartDatabase.SetData(LoadAssets<HeadPartData>(FilePathConfig.HeadPartDataPath));
			var corePartDatabase = databaseManager.GetCorePartDatabase();
			corePartDatabase.SetData(LoadAssets<CorePartData>(FilePathConfig.CorePartDataPath));
			var weponPartDatabase = databaseManager.GetWeponPartDatabase();
			weponPartDatabase.SetData(LoadAssets<WeponPartData>(FilePathConfig.WeponPartDataPath));
			var legPartDatabase = databaseManager.GetLegPartDatabase();
			legPartDatabase.SetData(LoadAssets<LegPartData>(FilePathConfig.LegPartDataPath));

			EditorSceneManager.MarkSceneDirty(nowScene);
		}
	}

	T[] LoadAssets<T>(string path) where T : ScriptableObject
	{
		List<T> list = new List<T>();
		var guids = AssetDatabase.FindAssets("", new string[1] { path });
		foreach (var guid in guids)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guid);
			var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

			T data = asset as T;
			if(data != null)
			{
				list.Add(data);
			}
		}

		return list.ToArray();
	}
}
