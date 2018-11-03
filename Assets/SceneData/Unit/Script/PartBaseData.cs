//***********************************************
//PartBaseData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//PartBaseData
//各部位の基礎部分データ
//***********************************************
public class PartBaseData : ScriptableObject,IPartBaseDataViewer,IData
{
	[SerializeField]
	int id;
	[SerializeField]
	int cost;//ユニット出現に必要になる数値
	[SerializeField]
	int hp;
	[SerializeField]
	int modelId;

	public int Id { get { return id; } set { id = value; } }
	public int Cost { get { return cost; } set { cost = value; } }
	public int Hp { get { return hp; } set { hp = value; } }
	public int ModelId { get { return modelId; } set { modelId = value; } }

}

public interface IPartBaseDataViewer
{
	int Id { get; }
	int Cost { get; }
	int Hp { get; }
	int ModelId { get; }
}
