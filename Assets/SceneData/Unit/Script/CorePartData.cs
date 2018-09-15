//************************************************
//CorePartData
//Author y-harada
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//CorePartData
//************************************************
public class CorePartData : PartBaseData
{
	[SerializeField]
	CoreType coreType;//コアのタイプ 種類によってLvボーナスに違いがでる
	[SerializeField]
	int skillTreeId;//参照するスキルツリーのID

	public enum CoreType
	{
		LightWeight,//軽量型
	}
}
