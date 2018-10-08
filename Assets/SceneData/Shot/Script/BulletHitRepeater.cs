//***************************************
//BulletRepeater.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//***************************************
//BulletRepeater
//Senderを集約して Receiverに送る
//***************************************
public class BulletHitRepeater : BulletHitSender
{
	[SerializeField]
	BulletHitSender[] senders;

	/// <summary>
	/// 初期化 所持しているSenderにアクションセット
	/// </summary>
	override public void Init(Action<BulletComponent.BulletParam> action)
	{
		for(int i = 0; i < senders.Length;i++)
		{
			senders[i].HitSendAction = SendHit;
		}

		HitSendAction = action;
	}

	public override void SetLayer(int layerNum)
	{
		for (int i = 0; i < senders.Length; i++)
		{
			senders[i].SetLayer(layerNum);
		}
	}

	/// <summary>
	/// 中継用関数
	/// </summary>
	void SendHit(BulletComponent.BulletParam bulletParam)
	{
		HitSendAction(bulletParam);
	}
}
