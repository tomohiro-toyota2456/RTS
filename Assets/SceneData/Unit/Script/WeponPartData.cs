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
	int[] attributeIds;//特性ID 別の特性データからIDで引く用


	public float Atk { get { return atk; } set { atk = value; } }
	public float FillingSec { get { return fillingSec; } set { fillingSec = value; } }
	public int Accuracy { get { return accuracy; } set { accuracy = value; } }
	public float Range { get { return range; } set { range = value; } }
	public int CriticalPer { get { return criticalPer; } set { criticalPer = value; } }
	public int[] AttributeIds { get { return attributeIds; } set { attributeIds = value; } }
}

public interface IWeponPartDataViewer : IPartBaseDataViewer
{
	float Atk { get; }
	float FillingSec { get; }
	int Accuracy { get; }
	float Range { get; }
	int CriticalPer { get; }
	int[] AttributeIds { get; }
}
