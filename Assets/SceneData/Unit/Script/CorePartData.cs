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
public class CorePartData : PartBaseData,ICorePartDataViewer
{
	[SerializeField]
	CoreType coreType;//コアのタイプ 種類によってLvボーナスに違いがでる
	[SerializeField]
	int skillTreeId;//参照するスキルツリーのID

	public enum CoreType
	{
		Light,//軽量型
		Heavy,//重量
		Assault,//強襲
		Support,//サポート

	}

	public CoreType CType { get { return coreType; } set { coreType = value; } }
	public int SkillTreeId { get { return skillTreeId; } set { skillTreeId = value; } }
}

public interface ICorePartDataViewer : IPartBaseDataViewer
{
	CorePartData.CoreType CType { get; }
	int SkillTreeId { get; }
}
