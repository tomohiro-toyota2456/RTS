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
public class CorePartData : PartBaseData,CorePartDataViewer
{
	[SerializeField]
	CoreType coreType;//コアのタイプ 種類によってLvボーナスに違いがでる
	[SerializeField]
	int skillTreeId;//参照するスキルツリーのID

	public enum CoreType
	{
		LightWeight,//軽量型
	}

	public CoreType CType { get { return coreType; } set { coreType = value; } }
	public int SkillTreeId { get { return skillTreeId; } set { skillTreeId = value; } }
}

public interface CorePartDataViewer : IPartBaseDataViewer
{
	CorePartData.CoreType CType { get; }
	int SkillTreeId { get; }
}
