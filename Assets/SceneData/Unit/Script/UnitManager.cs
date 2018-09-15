//***********************************************
//UnitManager.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//UnitManager
//***********************************************
public class UnitManager : MonoBehaviour
{
	//デッキの仕組みがないのでとりあえずテスト
	[SerializeField]
	PartModelConnectionData leg;
	[SerializeField]
	PartModelConnectionData left;
	[SerializeField]
	PartModelConnectionData right;
	[SerializeField]
	PartModelConnectionData head;
	[SerializeField]
	CorePartModelConnectionData core;

	public struct BattleUnitData
	{
		public Transform trans;//動いているモデル
		public UnitData unitData;
		//AIデータ？
		//public UnitAI ai;みたいな
		public bool isAlive;
	}

	List<BattleUnitData> unitList = new List<BattleUnitData>();
	UserUnitData[] userUnitDataArray;

	public void SetDeck(UserUnitData[] userUnitDataArray)
	{
		this.userUnitDataArray = userUnitDataArray;
	}

	UserUnitData Test()
	{
		UserUnitData userUnitData = new UserUnitData();
		userUnitData.name = "Test";
		userUnitData.headPart = ScriptableObject.CreateInstance<HeadPartData>();
		userUnitData.rightWeponPart = ScriptableObject.CreateInstance<WeponPartData>();
		userUnitData.leftWeponPart = ScriptableObject.CreateInstance<WeponPartData>();
		userUnitData.legPart = ScriptableObject.CreateInstance<LegPartData>();
		userUnitData.corePart = ScriptableObject.CreateInstance<CorePartData>();
		return userUnitData;
	}

	public void CreateUnit(int deckIdx)
	{
		UserUnitData userUnitData = Test();
		//userUnitData = userUnitDataArray[deckIdx];

		UnitData unitData = new UnitData();
		unitData.Init(userUnitData.name, userUnitData.headPart, userUnitData.leftWeponPart, userUnitData.rightWeponPart, userUnitData.legPart);

		//モデルローダーなんぞないのでテストで固定物でやる
		var h = CreateModel(head);
		var lw = CreateModel(left);
		var rw = CreateModel(right);
		var l = CreateModel(leg);
		var c = CreateCoreModel(core);
		var trans = PartsConnectionFunction.ConnectPartModels(c, h, lw, rw, l);

		BattleUnitData battleUnitData;
		battleUnitData.isAlive = true;
		battleUnitData.trans = trans;
		battleUnitData.unitData = unitData;

		unitList.Add(battleUnitData);
	}
	
	//モデル生成
	public PartModelConnectionData CreateModel(PartModelConnectionData prefab)
	{
		var ins = Instantiate<PartModelConnectionData>(prefab);
		return ins;
	}

	public CorePartModelConnectionData CreateCoreModel(CorePartModelConnectionData prefab)
	{
		var ins = Instantiate<CorePartModelConnectionData>(prefab);
		return ins;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			CreateUnit(0);
		}
	}
}
