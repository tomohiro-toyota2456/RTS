//***********************************************
//HeadPartData.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************************
//HeadPartData
//***********************************************
public class HeadPartData : PartBaseData,IHeadPartDataViewer
{
	[SerializeField]
	float fov;//視野だが、実際には円で表現するので視野の半径となる
	[SerializeField]
	int accuracy;//命中 

	public float Fov { get { return fov; } set { fov = value; } }
	public int Accuracy { get { return accuracy; } set { accuracy = value; } }
}

public interface IHeadPartDataViewer : IPartBaseDataViewer
{
	float Fov { get; }
	int Accuracy { get; }
}
