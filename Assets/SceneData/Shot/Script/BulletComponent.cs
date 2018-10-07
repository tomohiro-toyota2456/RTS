//**************************************
//BulletComponent.cs
//Author y-harada
//**************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**************************************
//BulletComponent
//**************************************
public class BulletComponent : MonoBehaviour
{
	[SerializeField]
	Rigidbody rdbody;

	BulletParam bulletParam;
	float range;
	float spd;

	/// <summary>
	/// 外部に送るようデータ
	/// </summary>
	public struct BulletParam
	{
		public int uniqueId;
		public float atk;
		public int accuracy;
		public int criticalPer;
		public int[] buffIds;
		public Vector3 hitPoint;
		public int effectId;
	}

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Init(int uniqueId,float atk,int accuracy,float range,int criticalPer,int[] attributeIds,IBulletDataViewer bulletDataViewer)
	{
		bulletParam.uniqueId = uniqueId;//ショット自体のID　BulletDataのIDでもない
		bulletParam.atk = atk;
		bulletParam.accuracy = accuracy;
		bulletParam.criticalPer = criticalPer;
		bulletParam.buffIds = attributeIds;
		bulletParam.effectId = bulletDataViewer.HitEffectId;
		this.range = range;
		this.spd = bulletDataViewer.Speed;
	}

	/// <summary>
	/// 衝突処理
	/// </summary>
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.contacts.Length != 0)
		{
			bulletParam.hitPoint = collision.contacts[0].point;
		}
	}

}
