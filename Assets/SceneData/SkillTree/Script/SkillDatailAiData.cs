//***********************************************
//SkillDetailAiData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//SkillDetailAiData
//***********************************************
public class SkillDatailAiData : ScriptableObject
{
	[SerializeField]
	int id;
	[SerializeField]
	int lv;

	public int Id { get { return id; } set { id = value; } }
	public int Lv { get { return lv; } set { lv = value; } }
}
