//************************************************
//BuffData.cs
//Author y-harada
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//BuffData
//************************************************
public class BuffData : ScriptableObject
{
	[SerializeField]
	int id;
	[SerializeField]
	ParamType paramType;
	[SerializeField]
	BehaviorType behaviorType;
	[SerializeField]
	EffectType effectType;
	[SerializeField]
	BuffType buffType;
	[SerializeField]
	float behaviorValue;//動作タイプ数値 回数なら 整数 時間なら秒単位で
	[SerializeField]
	float effectValue;//影響数値　固定ならその数値が 割合なら0~1の間で指定
	[SerializeField]
	float interval;//動作間隔


	//影響するパラメータ
	public enum ParamType
	{
		Hp,
		Atk,
		Spd,
		Damage,//パラメータ？ではあるが　ダメージに対する行動 Buffならダメージカット　DeBuffならダメージ上昇 
	}

	public enum BehaviorType
	{
		Count,//回数
		Time,//時間
	}

	//影響タイプ　上昇か減少か
	public enum BuffType
	{
		DeBuff,
		Buff,
	}

	//影響　割合か固定か
	public enum EffectType
	{
		Ratio,
		Fixation,
	}

	public int Id { get { return id; } set { id = value; } }
	public ParamType PType { get { return paramType; } set { paramType = value; } }
	public BehaviorType BeType { get { return behaviorType; } set { behaviorType = value; } }
	public BuffType BType { get { return buffType; } set { buffType = value; } }
	public EffectType EType { get { return effectType; } set { effectType = value; }}
	public float BehaviorValue { get { return behaviorValue; } set { behaviorValue = value; } }
	public float EffectValue { get { return effectValue; } set { effectValue = value; } }
	public float Interval { get { return interval; } set { interval = value; } }

}
