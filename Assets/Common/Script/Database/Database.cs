//***************************************
//Database.cs
//Author y-harada
//***************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***************************************
//Database
//***************************************
public class Database<T,B> : MonoBehaviour where T : class where B : class
{
	[SerializeField]
	T[] data;

	public B Search(int id)
	{
		for(int i = 0; i < data.Length; i++)
		{
			IData d = data[i] as IData;

			if(id == d.Id)
			{
				return data[i] as B;
			}
		}
		return null;
	}

	public List<B> SearchAll(int id)
	{
		List<B> list = new List<B>();
		for (int i = 0; i < data.Length; i++)
		{
			IData d = data[i] as IData;

			if (id == d.Id)
			{
				list.Add(data[i] as B);
			}
		}

		return list;
	}

	public List<B> GetList()
	{
		List<B> list = new List<B>();
		for (int i = 0; i < data.Length; i ++)
		{
			list.Add(data[i] as B);
		}

		return list;
	}

	public void SetData(T[] data)
	{
		this.data = data;
	}
}
