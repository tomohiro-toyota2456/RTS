﻿//***********************************************
//GameTouchAction.cs
//Author y-harada
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


//ゲーム中に使うタッチ用アクション
//タップ
//ピンチイン・アウト
//ドラッグ（シングルタップのみ)
public class GameTouchAction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	[SerializeField]
	Image image;//範囲描画用 左下基準にすること
	[SerializeField]
	RectTransform screenObj;//タッチ判定をするオブジェクト　基本スクリーンサイズと同じにする

	Vector2 beginDragPos;
	RectTransform rectTransform;
	Vector2 screenSize;

	Action<Vector2, Vector2> endDragAction;
	public Action<Vector2, Vector2> EndDragAction { set { endDragAction = value; } }

	Action<Vector2> touchAction;
	public Action<Vector2> TouchAction { set { touchAction = value; } }

	//ドラッグ終了時の送り込む座標
	Vector2 returnStPos;
	Vector2 returnEdPos;

	bool isDrag = false;

	private void Start()
	{
		rectTransform = image.GetComponent<RectTransform>();
	}

	Vector2 GetSize(Vector2 _posSt, Vector2 _posEd)
	{
		Vector2 size;

		size.x = _posEd.x - _posSt.x;
		size.y = -_posEd.y + _posSt.y;

		return size;
	}

	Vector2 Offset(Vector2 _pos)
	{
		Vector2 offset;

		offset.x = screenSize.x / Screen.width;
		offset.y = screenSize.y / Screen.height;

		Vector2 ans;

		ans.x = offset.x * _pos.x;
		ans.y = offset.y * _pos.y;

		return ans;
	}

	//ドラッグ開始時
	public void OnBeginDrag(PointerEventData _data)
	{
		screenSize.x = screenObj.rect.width;
		screenSize.y = screenObj.rect.height;
		beginDragPos = _data.position;
		isDrag = true;
		image.gameObject.SetActive(true);
	}

	//ドラッグ中
	public void OnDrag(PointerEventData _data)
	{
		Vector2 stPos;
		Vector2 edPos;

		stPos = beginDragPos;
		edPos = _data.position;

		if (beginDragPos.x > _data.position.x)
		{
			stPos.x = _data.position.x;
			edPos.x = beginDragPos.x;
		}

		if (beginDragPos.y < _data.position.y)
		{
			stPos.y = _data.position.y;
			edPos.y = beginDragPos.y;
		}


		returnStPos = stPos;
		returnEdPos = edPos;

		//yの差をとる
		float diff = stPos.y - edPos.y;
		Vector2 buf = stPos;

		//描画の都合上 yを下に下げる
		buf.y -= diff;

		rectTransform.anchoredPosition = Offset(buf);
		rectTransform.sizeDelta = GetSize(Offset(stPos), Offset(edPos));

	}

	//ドラッグ終了時
	public void OnEndDrag(PointerEventData _data)
	{
		if (endDragAction != null)
		{
			endDragAction(returnStPos, returnEdPos);
		}


		isDrag = false;

		image.gameObject.SetActive(false);
	}

	//クリック時
	public void OnPointerClick(PointerEventData _data)
	{
		if (isDrag)
			return;

		if (touchAction != null)
		{
			touchAction(_data.position);
		}

	}
}
