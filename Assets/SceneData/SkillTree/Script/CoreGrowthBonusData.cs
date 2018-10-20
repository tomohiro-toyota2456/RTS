//***************************************
//CoreGrowthBonusData.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***************************************
//CoreGrowthBonusData
//コアのタイプによって成長ボーナスを与えるクラス
//大した量もなさそうなのでStaticで固定
//***************************************
public static class CoreGrowthBonusData
{
	public static SkillDetailParamData.Params CalcBonus(CorePartData.CoreType coreType,int lv,int maxLv)
	{
		switch(coreType)
		{
			case CorePartData.CoreType.Light:
				return CalcLightType(lv, maxLv);
			case CorePartData.CoreType.Heavy:
				return CalcHeavyType(lv, maxLv);
			case CorePartData.CoreType.Assault:
				return CalcAssaultType(lv, maxLv);
			case CorePartData.CoreType.Support:
				return CalcSupportType(lv, maxLv);
		}

		return new SkillDetailParamData.Params();
	}

	static SkillDetailParamData.Params CalcLightType(int lv,int maxLv)
	{
		SkillDetailParamData.Params p = new SkillDetailParamData.Params();
		p.cost = 0;
		p.accuracy = 0;
		p.attributeIds = null;
		p.criticalPer = 0;
		p.fillingSec = 0;
		p.fov = 0;
		p.hp = 0;
		p.mobility = CompletionFunctions.Comp(50, CompletionFunctions.LvConvertCompVal(lv, maxLv), CompletionFunctions.Liner);
		p.range = 0;
		return p;
	}

	static SkillDetailParamData.Params CalcHeavyType(int lv, int maxLv)
	{
		SkillDetailParamData.Params p = new SkillDetailParamData.Params();
		p.cost = 0;
		p.accuracy = 0;
		p.attributeIds = null;
		p.criticalPer = 0;
		p.fillingSec = 0;
		p.fov = 0;
		p.hp = CompletionFunctions.Comp(100, CompletionFunctions.LvConvertCompVal(lv, maxLv), CompletionFunctions.Liner);
		p.mobility = 0;
		p.range = 0;
		return p;
	}

	static SkillDetailParamData.Params CalcAssaultType(int lv, int maxLv)
	{
		SkillDetailParamData.Params p = new SkillDetailParamData.Params();
		p.cost = 0;
		p.accuracy = 0;
		p.attributeIds = null;
		p.criticalPer = 0;
		p.fillingSec = CompletionFunctions.Comp(-50, CompletionFunctions.LvConvertCompVal(lv, maxLv), CompletionFunctions.Liner);
		p.fov = 0;
		p.hp = 0;
		p.mobility = 0;
		p.range = 0;
		return p;
	}

	static SkillDetailParamData.Params CalcSupportType(int lv, int maxLv)
	{
		SkillDetailParamData.Params p = new SkillDetailParamData.Params();
		p.cost = 0;
		p.accuracy = 0;
		p.attributeIds = null;
		p.criticalPer = 0;
		p.fillingSec = 0;
		p.fov = CompletionFunctions.Comp(50, CompletionFunctions.LvConvertCompVal(lv, maxLv), CompletionFunctions.Liner); 
		p.hp = 0;
		p.mobility = 0;
		p.range = 0;
		return p;
	}
}
