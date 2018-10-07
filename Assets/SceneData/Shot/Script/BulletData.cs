//***************************************
//BulletData.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***************************************
//BulletData
//玉データ
//***************************************
public class BulletData : ScriptableObject,IBulletDataViewer
{
	[SerializeField]
	int id;
	[SerializeField]
	int modelId;
	[SerializeField]
	int hitEffectId;//着弾エフェクト
	[SerializeField]
	float speed;//速度

	public int Id { get { return id; } set { id = value; } }
	public int ModelId { get { return modelId; } set { modelId = value; } }
	public int HitEffectId { get { return hitEffectId; } set { hitEffectId = value; } }
	public float Speed { get { return speed; } set { speed = value ; } }
}

public interface IBulletDataViewer
{
	int Id { get; }
	int ModelId { get; }
	int HitEffectId { get; }
	float Speed { get; }
}
