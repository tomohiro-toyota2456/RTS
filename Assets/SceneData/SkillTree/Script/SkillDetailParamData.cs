//***********************************************
//SkillDetailParamData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//SkillDetailParamData
//***********************************************
public class SkillDetailParamData : ScriptableObject,SkillDetailParamDataViewer
{
	[SerializeField]
	int id;
	[SerializeField]
	int lv;
	[SerializeField]
	Params param;

	[System.Serializable]
	public struct Params
	{
		public int hp;
		public int cost;
		public float fillingSec;
		public int accuracy;
		public float range;
		public int criticalPer;
		public int[] attributeIds;
		public float fov;
		public int mobility;
		float[] aptitudes;
	}

	public int Id { get { return id; } set { id = value; } }
	public int Lv { get { return lv; } set { lv = value; } }
	public Params Param { get { return param; } set { param = value; } }
}

public interface SkillDetailParamDataViewer
{
	int Id { get;}
	int Lv { get; }
	SkillDetailParamData.Params Param { get; }
}
