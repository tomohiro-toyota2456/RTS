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
	protected Rigidbody rdbody;

	protected BulletParam bulletParam;
	protected float range;
	protected float spd;
	protected Vector3 stPos;
	protected Vector3 nowPos;

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
	public void Init(int teamId,int uniqueId,IWeponPartDataViewer weponPartDataViewer,IBulletDataViewer bulletDataViewer)
	{
		bulletParam.uniqueId = uniqueId;//ショット自体のID　BulletDataのIDでもない
		bulletParam.atk = weponPartDataViewer.Atk;
		bulletParam.accuracy = weponPartDataViewer.Accuracy;
		bulletParam.criticalPer = weponPartDataViewer.CriticalPer;
		bulletParam.buffIds = weponPartDataViewer.AttributeIds;
		bulletParam.effectId = bulletDataViewer.HitEffectId;
		this.range = weponPartDataViewer.Range;
		this.spd = weponPartDataViewer.Spd;
		SetLayer(teamId, weponPartDataViewer.Target);
	}

	public int SetLayer(int teamId,WeponPartData.AttackTarget target)
	{
		int layer = 0;
		switch(target)
		{
			case WeponPartData.AttackTarget.Army:
				layer = 11 + teamId;
				break;
			case WeponPartData.AttackTarget.EnemyArmy:
				layer = 9 + teamId;
				break;
			case WeponPartData.AttackTarget.All:
				layer = 13;
				break;
		}

		return layer;
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

		var sender = collision.gameObject.GetComponent<BulletHitSender>();
		if (sender != null)
		{
			sender.HitSendAction(bulletParam);
		}
	}

	/// <summary>
	/// 発射
	/// </summary>
	virtual public void Shoot(Vector3 stPos,Vector3 edPos)
	{
		Vector3 vec = edPos - stPos;
		vec = vec.normalized;
		vec = vec * spd;

		rdbody.velocity = vec.normalized;
	}

	private void Update()
	{
		Vector3 st = stPos;
		Vector3 ed = nowPos;

		//yの動きはみない
		st.y = 0;
		ed.y = 0;

		var val = (ed - st).magnitude;

		if(val >= range)
		{
			//消滅
		}
	}

}
