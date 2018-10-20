using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponPartShootData : MonoBehaviour
{
	[SerializeField]
	Transform shootPositionTrans;

	public Transform Position { get { return shootPositionTrans; } }
}
