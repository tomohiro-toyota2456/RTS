//***********************************************
//WeponPartData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//
//***********************************************
public class WeponPartData : PartBaseData,IWeponPartDataViewer
{
	[SerializeField]
	float atk;
	[SerializeField]
	float fillingSec;//充填時間
	[SerializeField]
	int accuracy;//命中 
	[SerializeField]
	float range;//射程
	[SerializeField]
	int criticalPer;//クリティカル率
	[SerializeField]
	float spd;//弾速
	[SerializeField]
	AttackTarget target;//当たるターゲット
	[SerializeField]
	int[] attributeIds;//特性ID 別の特性データからIDで引く用

	//当たる陣営
	public enum AttackTarget
	{
		Army,//自軍 Buff系
		EnemyArmy,//敵軍 普通の攻撃向け
		All//すべて 爆撃等
	}


	public float Atk { get { return atk; } set { atk = value; } }
	public float FillingSec { get { return fillingSec; } set { fillingSec = value; } }
	public int Accuracy { get { return accuracy; } set { accuracy = value; } }
	public float Range { get { return range; } set { range = value; } }
	public int CriticalPer { get { return criticalPer; } set { criticalPer = value; } }
	public float Spd { get { return spd; } set { spd = value; } }
	public AttackTarget Target { get { return target; } set { target = value; } } 
	public int[] AttributeIds { get { return attributeIds; } set { attributeIds = value; } }
}

public interface IWeponPartDataViewer : IPartBaseDataViewer
{
	float Atk { get; }
	float FillingSec { get; }
	int Accuracy { get; }
	float Range { get; }
	int CriticalPer { get; }
	float Spd { get; }
	WeponPartData.AttackTarget Target { get; }
	int[] AttributeIds { get; }
}
