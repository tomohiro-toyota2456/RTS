﻿//***********************************************
//SkillTreeData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//SkillTree
//***********************************************
public class SkillTreeData : ScriptableObject
{
	[SerializeField]
	int id;
	[SerializeField]
	int[] rootIdxs;
	[SerializeField]
	Node[] nodes;

	[System.Serializable]
	public struct Conditions
	{
		public int skillDataId;
		public int lv;
	}

	[System.Serializable]
	public class Node
	{
		public Conditions[] conditions;
		public int[] childIdxs;
		public int skillDataId;
	}

	public int Id { get { return id; } set { id = value; } }
	public int[] RootIdxs { get { return rootIdxs; } set { rootIdxs = value; } }
	public Node[] Nodes { get { return nodes; } set { nodes = value; } }
}
