//***************************************
//BulletHitSender.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//***************************************
//BulletHitSender
//ショットの衝突関数内で呼ばれるクラス
//Colliderと同じとこでアタッチ
//***************************************
public class BulletHitSender : MonoBehaviour
{
	/// <summary>
	/// 衝突時に情報をおくるためのアクション
	/// </summary>
	public Action<BulletComponent.BulletParam> HitSendAction { get; set; }

	virtual public void Init(Action<BulletComponent.BulletParam> action)
	{
		HitSendAction = action;
	}

	//ここでやるべきなのかというのはあるが変更
	/// <summary>
	/// レイヤー変更
	/// </summary>
	/// <param name="layerNum"></param>
	virtual public void SetLayer(int layerNum)
	{
		this.gameObject.layer = layerNum;
		Debug.Log("aaaa");
	}
}
