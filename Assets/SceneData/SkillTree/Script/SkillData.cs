//***********************************************
//SkillData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//SkillData
//
//概要
//上昇値等は
//SkillDetailParamで引く SkillData IDとLvで引く
//***********************************************
public class SkillData : ScriptableObject
{
	[SerializeField]
	int id;
	[SerializeField]
	SkillType skillType;//スキルがパラメータ上昇なのかAIなのか
	[SerializeField]
	Conditions cond;//スキル適用条件
	[SerializeField]
	int condDetail;//enum変換用
	[SerializeField]
	int maxLv;

	public enum SkillType
	{
		PARAM,
		AI,
	}

	public enum Conditions
	{
		NONE,//条件なし
		WEPON_TYPE,//武器種
		LEG_TYPE,//脚部種
	}

	public int Id { get { return id; } set { id = value; } }
	public SkillType Type { get { return skillType; } set { skillType = value; } }
	public Conditions Cond { get { return cond; } set { cond = value; } }
	public int CondDetail { get { return condDetail; } set { condDetail = value; } }
	public int MaxLv { get { return maxLv; } set { maxLv = value; } }
}
