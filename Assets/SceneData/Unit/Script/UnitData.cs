//***********************************************
//UnitData.cs
//Authot y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//
//***********************************************
public class UnitData
{
	string name;//Unit名
	int curHp;
	float leftWeponFillngTimer;//充填用タイマー
	float rightWeponFillingTimer;//充填用タイマー
	
	//パーツデータ
	LegPartData leg;
	WeponPartData leftWepon;
	WeponPartData rightWepon;
	HeadPartData head;

	public string UnitName { get { return name; } }
	public int CurHp { get { return curHp; } }

	public void Init(string name,HeadPartData headPart,WeponPartData leftWeponPart,WeponPartData rightWeponPart,LegPartData legPart)
	{
		this.name = name;
		head = headPart;
		leftWepon = leftWeponPart;
		rightWepon = rightWeponPart;
		leg = legPart;

		leftWeponFillngTimer = 0;
		rightWeponFillingTimer = 0;
		curHp = CalcMaxHp();
	}

	public int CalcMaxHp()
	{
		int maxHp = 0;
		maxHp = head.Hp + leg.Hp + leftWepon.Hp + rightWepon.Hp;
		return maxHp;
	}
	
	public int AddDamage(int damage)
	{
		curHp -= damage;
		return curHp;
	}

	public void CureHp(int cureVal)
	{
		curHp += cureVal;
		int maxHp = CalcMaxHp();
		curHp = CurHp < maxHp ? curHp : maxHp; 
	}
}
