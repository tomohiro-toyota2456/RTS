//***************************************
//UnitShooter.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***************************************
//UnitShooter
//***************************************
public class UnitShooter : MonoBehaviour
{
	[SerializeField]
	BulletComponent bulletPrefab;
	[SerializeField]
	Transform firingTransform;//射撃位置

	IWeponPartDataViewer weponPartDataViewer;
	IBulletDataViewer bulletDataViewer;
	int teamId;

	//初期化
	public void Init(int teamId, IWeponPartDataViewer weponPartDataViewer, IBulletDataViewer bulletDataViewer)
	{
		this.teamId = teamId;
		this.weponPartDataViewer = weponPartDataViewer;
		this.bulletDataViewer = bulletDataViewer;
	}

	/// <summary>
	/// 初期化
	/// 球データはウェポンから引けるはず
	/// </summary>
	public void Init(int teamId,IWeponPartDataViewer weponPartDataViewer)
	{
		this.teamId = teamId;
		this.weponPartDataViewer = weponPartDataViewer;
	}

	/// <summary>
	/// 射撃を行う
	/// </summary>
	public void Shoot(Vector3 targetPos)
	{
		Shoot(firingTransform.position, targetPos);
	}

	void Shoot(Vector3 stPos, Vector3 edPos)
	{
		var bullet = CreateBullet(weponPartDataViewer.BulletId);
		bullet.Init(teamId, 1, weponPartDataViewer, bulletDataViewer);
		bullet.Shoot(stPos, edPos);
	}

	BulletComponent CreateBullet(int id)
	{
		//生成ルールが決まり次第IDからロード等行う
		return Instantiate<BulletComponent>(bulletPrefab);
	}

	/*
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			Shoot(new Vector3(100,0,-1000));
		}
	}
	*/
}

