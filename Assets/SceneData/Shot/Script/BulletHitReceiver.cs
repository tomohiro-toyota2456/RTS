//***************************************
//BulletHitReceiver.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//***************************************
//BulletHitReceiver.cs
//***************************************
public class BulletReceiver
{
	public BulletHitSender[] Senders{ get;set; }

	//辞書のキーで重複消しするけどおもかったら考える
	Dictionary<int, BulletComponent.BulletParam> hitDict = new Dictionary<int, BulletComponent.BulletParam>();

	/// <summary>
	/// 初期化 
	/// </summary>
	public void Init(BulletHitSender[] senders)
	{
		Senders = senders;

		for(int i = 0; i < Senders.Length; i++)
		{
			Senders[i].Init(AddHitDict);
		}
	}

	/// <summary>
	/// layerの変更
	/// </summary>
	/// <param name="layerNum"></param>
	public void SetLayer(int layerNum)
	{
		for (int i = 0; i < Senders.Length; i++)
		{
			Senders[i].SetLayer(layerNum);
		}
	}

	/// <summary>
	/// ヒット情報を登録
	/// </summary>
	void AddHitDict(BulletComponent.BulletParam bulletParam)
	{
		if(!hitDict.ContainsKey(bulletParam.uniqueId))
		{
			hitDict.Add(bulletParam.uniqueId,bulletParam);
		}
	}

	public void Clear()
	{
		hitDict.Clear();
	}

	/// <summary>
	/// 辞書が保持しているヒット判定を取得
	/// </summary>
	public List<BulletComponent.BulletParam> GetBulletParams()
	{
		List<BulletComponent.BulletParam> list = new List<BulletComponent.BulletParam>();
		foreach(var val in hitDict)
		{
			list.Add(val.Value);
		}

		return list;
	}

}
