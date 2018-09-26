//***********************************************
//EditorSkillTreeData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//EditorSkillTreeData
//***********************************************
public class EditorSkillTreeData : ScriptableObject
{
	[SerializeField]
	List<EditorNodeData> list;

	[System.Serializable]
	public class Conditions
	{
		public int idx;
		public int lv;
	}

	[System.Serializable]
	public class EditorNodeData
	{
		public List<int> children;
		public List<Conditions> parents;
		public int skillDataId;
		public Vector2 position;
	}

	public List<EditorNodeData> List { get { return list; } set { list = value; } }
}
