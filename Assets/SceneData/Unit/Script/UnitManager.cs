//***********************************************
//UnitManager.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

	public class BattleUnitData
	{
		public BulletReceiver bulletReceiver;
		public Transform trans;//動いているモデル
		public UnitData unitData;
		//AIデータ？
		//public UnitAI ai;みたいな
		public bool isAlive = false;
		public bool isSelection = false;
	}

	static readonly int MaxUnitSum = 30;

	List<BattleUnitData> unitList = new List<BattleUnitData>();
	UserUnitData[] userUnitDataArray;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			CreateUnit(0);
		}
	}

	public void SetDeck(UserUnitData[] userUnitDataArray)
	{
		this.userUnitDataArray = userUnitDataArray;
	}

	UserUnitData Test()
	{
		var head = DatabaseManager.Instance.GetHeadPartDatabase().Search(1);

		UserUnitData userUnitData = new UserUnitData();
		userUnitData.name = "Test";
		userUnitData.headPart = ScriptableObject.CreateInstance<HeadPartData>();
		userUnitData.rightWeponPart = ScriptableObject.CreateInstance<WeponPartData>();
		userUnitData.leftWeponPart = ScriptableObject.CreateInstance<WeponPartData>();
		userUnitData.leftWeponPart.Spd = 2;
		userUnitData.rightWeponPart.Spd = 2;
		userUnitData.leftWeponPart.Range = 1000;
		userUnitData.rightWeponPart.Range = 1000;
		userUnitData.legPart = ScriptableObject.CreateInstance<LegPartData>();
		userUnitData.corePart = ScriptableObject.CreateInstance<CorePartData>();
		return userUnitData;
	}

	BulletData Test2()
	{
		BulletData data = ScriptableObject.CreateInstance<BulletData>();
		return data;
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

		BattleUnitData battleUnitData = new BattleUnitData();
		battleUnitData.isAlive = true;
		battleUnitData.trans = trans;
		battleUnitData.unitData = unitData;
		battleUnitData.bulletReceiver = new BulletReceiver();
		var coreRepeater = c.GetComponent<BulletHitRepeater>();
		var legRepeater = l.GetComponent<BulletHitRepeater>();

		//ヒットをまとめる
		BulletHitRepeater[] repeaters = new BulletHitRepeater[2] { coreRepeater, legRepeater };
		battleUnitData.bulletReceiver.Init(repeaters);
		battleUnitData.bulletReceiver.SetLayer(1);

		//武器初期化
		var lwepon = lw.GetComponent<UnitShooter>();
		var rwepon = rw.GetComponent<UnitShooter>();

		lwepon.Init(0, userUnitData.leftWeponPart, Test2());
		rwepon.Init(0, userUnitData.rightWeponPart, Test2());

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

	//選ばれているかチェック
	public void SelectUnitFromPosition(System.Func<Vector3,bool> checkFunction)
	{
		for(int i = 0; i < unitList.Count; i++)
		{
			BattleUnitData data = unitList[i];
			if(data.isAlive)
			{
				data.isSelection = checkFunction(data.trans.position);
			}
		}

	}

	//選ばれているユニットにアクションを起こさせる　今は動くだけ
	public void ActionSelectionUnits(System.Action<UnitMover> unitAction)
	{
		for (int i = 0; i < unitList.Count;i++)
		{
			if(unitList[i].isSelection)
			{
				var mover = unitList[i].trans.gameObject.GetComponent<UnitMover>();
				unitAction(mover);
			}
		}
	}
}
