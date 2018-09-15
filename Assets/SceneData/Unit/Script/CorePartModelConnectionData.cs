//************************************************
//CorePartModelConnectionData
//Author y-harada
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//CorePartModelConnectionData
//************************************************
public class CorePartModelConnectionData : MonoBehaviour
{
	[SerializeField]
	Transform headConnectionTrans;
	[SerializeField]
	Transform leftWeponConnectionTrans;
	[SerializeField]
	Transform rightWeponConnectionTrans;
	[SerializeField]
	Transform legConnectionTrans;

	public Transform HeadConnectionTrans { get { return headConnectionTrans; } }
	public Transform LeftWeponConnectionTrans { get { return leftWeponConnectionTrans; } }
	public Transform RightWeponConnectionTrans { get { return rightWeponConnectionTrans; } }
	public Transform LegConnectionTrans { get { return legConnectionTrans; } }
}
