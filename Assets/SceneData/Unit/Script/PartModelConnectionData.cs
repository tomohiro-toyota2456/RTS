//***********************************************
//PartModelConnectionData
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//PartModelConnectionData
//***********************************************
public class PartModelConnectionData : MonoBehaviour
{
	[SerializeField]
	Transform connectionTrans;

	public Transform ConnectionTrans { get { return connectionTrans; } }
}
