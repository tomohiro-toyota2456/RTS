//***********************************************
//UnitMover.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//***********************************************
//UnitMover
//***********************************************
public class UnitMover : MonoBehaviour
{
	[SerializeField]
	NavMeshAgent agent;
	public NavMeshAgent Agent { set { agent = value; } }

	public void SetSpeed(float spd)
	{
		agent.speed = spd;
	}

	public void Move(Vector3 target)
	{
		agent.SetDestination(target);
	}
}
