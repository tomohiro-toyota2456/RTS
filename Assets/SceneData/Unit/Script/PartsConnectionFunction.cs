//***********************************************
//PartsConnectionFunction
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//PartsConnectionFunction
//***********************************************
public class PartsConnectionFunction
{
	public static Transform ConnectPartModels(	CorePartModelConnectionData core, PartModelConnectionData head,PartModelConnectionData leftWepon,
																							PartModelConnectionData rightWepon,PartModelConnectionData leg)
	{
		Vector3 zero = new Vector3(0, 0, 0);
		//移動や向きの関係からレッグが親でコアがその子　残りはコアの子になる
		core.LegConnectionTrans.SetParent(leg.ConnectionTrans);
		core.LegConnectionTrans.localPosition = zero;
		//core.LegConnectionTrans.localScale = new Vector3(1, 1, 1);
		head.ConnectionTrans.SetParent(core.HeadConnectionTrans);
		head.ConnectionTrans.localPosition = zero;

		leftWepon.ConnectionTrans.SetParent(core.LeftWeponConnectionTrans);
		leftWepon.ConnectionTrans.localPosition = zero;

		rightWepon.ConnectionTrans.SetParent(core.RightWeponConnectionTrans);
		rightWepon.ConnectionTrans.localPosition = zero;

		return leg.transform;
	}
}
