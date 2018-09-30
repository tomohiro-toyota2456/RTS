//***********************************************
//UserOrderUnit.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//UserOrderUnit
//***********************************************
public class UserOrderUnit : MonoBehaviour
{
	[SerializeField]
	GameTouchAction touchAction;
	[SerializeField]
	UnitManager unitManager;
	[SerializeField]
	Camera userCamera;

	private void Start()
	{
		touchAction.TouchAction = MoveUnits;
		touchAction.EndDragAction = SelectUnits;
	}

	public void SelectUnits(Vector2 stPos,Vector2 edPos)
	{
		unitManager.SelectUnitFromPosition((targetPos)=> { return CheckHit(targetPos, stPos, edPos); } );
	}

	public void MoveUnits(Vector2 target)
	{
		RaycastHit hit;
		Physics.Raycast(userCamera.ScreenPointToRay(target), out hit, 1000);

		unitManager.ActionSelectionUnits((mover) =>
		{
			mover.Move(hit.point);
		});

	}

	//平面で判定とり
	bool CheckHit(Vector3 targetPos, Vector2 stPos, Vector2 edPos)
	{
		 Vector2 tPos = userCamera.WorldToScreenPoint(targetPos);

		if (tPos.x > stPos.x && tPos.x < edPos.x)
		{
			if (tPos.y < stPos.y && tPos.y > edPos.y)
			{
				return true;
			}
		}

		return false;
	}
}
