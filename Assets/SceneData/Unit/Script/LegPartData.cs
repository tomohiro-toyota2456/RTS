//***********************************************
//LegPartData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//LegPartData
//***********************************************
public class LegPartData : PartBaseData,ILegPartDataViewer
{
	[SerializeField]
	int mobility;//機動力 移動性能+回避性能値
	[SerializeField]
	LegType legType;

	[SerializeField]
	float[] aptitudes;//地形適正0~2 0%~200%

	public enum LegType
	{
		TwoLegs,//二脚
	}

	public enum Terrain
	{
		Plain
	}

	public int Mobility { get { return mobility; } set { mobility = value; } }
	public float[] Aptitudes { get { return aptitudes; } set { aptitudes = value; } }
	public LegType Type { get { return legType; } set { legType = value; } }
}

public interface ILegPartDataViewer : IPartBaseDataViewer
{
	int Mobility { get; }
	float[] Aptitudes { get; }
	LegPartData.LegType Type { get; }
}
