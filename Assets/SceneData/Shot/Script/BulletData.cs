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

	public int Id { get { return id; } set { id = value; } }
	public int ModelId { get { return modelId; } set { modelId = value; } }
	public int HitEffectId { get { return hitEffectId; } set { hitEffectId = value; } }
}

public interface IBulletDataViewer
{
	int Id { get; }
	int ModelId { get; }
	int HitEffectId { get; }
}
