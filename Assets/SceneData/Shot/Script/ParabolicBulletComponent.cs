using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicBulletComponent : BulletComponent
{

	float rad = 0;

	public override void Shoot(Vector3 stPos, Vector3 edPos)
	{
		Vector3 stP = stPos;
		Vector3 edP = edPos;
		stP.y = 0;
		edP.y = 0;

		float len = (edP - stP).magnitude;

		Vector3 target = edPos - stPos;

		rad = Mathf.Atan2(target.z, target.x);

		//f(x)=ax^2 + bx + c
		float b = Mathf.Tan(60);
		float a = (target.y - b * len) / (len * len);

		StartCoroutine(shoot(new Vector3(len,target.y,0), stPos, a, b));
	}

	IEnumerator shoot(Vector3 target,Vector3 offset,float a,float b)
	{
		float x = 0;
		for(; x <= target.x; x+=this.spd)
		{
			float y = a * (x * x) + b * x;
			y += offset.y;

			float xP = Mathf.Cos(rad) * x;
			float zP = Mathf.Sin(rad) * x;

			transform.position = new Vector3(xP +offset.x, y, zP+offset.z);

			yield return 0;
		}

	}
}
