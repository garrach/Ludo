﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum AnimationDirection {
	left,
	right
}


[RequireComponent(typeof(Camera))]

public class UIView : MonoBehaviour {
	
	private Camera cam;
	private Canvas[] canvases;


	public virtual void Awake() {
		cam = this.GetComponent<Camera>();
		canvases = this.GetComponentsInChildren<Canvas>();


		Application.targetFrameRate = 60;
	}


	public virtual void Show()
	{
		ViewWillAppear();
		GameObject mainCanvas = this.transform.FindChild("UICanvas").gameObject;
		GameObject viewContent = mainCanvas.transform.FindChild("ViewContent").gameObject;
		viewContent.transform.localPosition = Vector3.zero;

		cam.enabled = true;
		foreach (Canvas canvas in canvases) canvas.enabled = true;

		Canvas.ForceUpdateCanvases();
	}




	public virtual void ShowAnimated(AnimationDirection sourceDirection)
	{
		cam.enabled = true;
		foreach (Canvas canvas in canvases) canvas.enabled = true;

		GameObject mainCanvas = this.transform.FindChild("UICanvas").gameObject;
		GameObject viewContent = mainCanvas.transform.FindChild("ViewContent").gameObject;
		RectTransform mainCanvasRectTransform = (RectTransform)mainCanvas.transform;

		if (sourceDirection == AnimationDirection.right) {
			viewContent.transform.localPosition = new Vector3(mainCanvasRectTransform.rect.width,
			                                                 mainCanvasRectTransform.localPosition.y,
			                                                 mainCanvasRectTransform.localPosition.z);
		}
		else 
		{
			viewContent.transform.localPosition = new Vector3(-mainCanvasRectTransform.rect.width,
			                                                 mainCanvasRectTransform.localPosition.y,
			                                                 mainCanvasRectTransform.localPosition.z);
		}

		ViewWillAppear();
		iTween.MoveTo(viewContent.gameObject, iTween.Hash("x", 0, "time", 0.33f, "islocal", true));
		Invoke("Show", 0.33f);
	}


	public virtual void HideAnimated(AnimationDirection getAwayDirection)
	{
		cam.enabled = true;
		foreach (Canvas canvas in canvases) canvas.enabled = true;
		
		Canvas.ForceUpdateCanvases();
		
		GameObject mainCanvas = this.transform.FindChild("UICanvas").gameObject;
		GameObject viewContent = mainCanvas.transform.FindChild("ViewContent").gameObject;
		RectTransform mainCanvasRectTransform = (RectTransform)mainCanvas.transform;

		mainCanvas.transform.localPosition = new Vector3(0,
		                                                 mainCanvasRectTransform.localPosition.y,
		                                                 mainCanvasRectTransform.localPosition.z);
		
		if (getAwayDirection == AnimationDirection.right) {
			iTween.MoveTo(viewContent.gameObject, iTween.Hash("x", -mainCanvasRectTransform.rect.width, "time", 0.33f, "islocal", true));

		}
		else 
		{
			iTween.MoveTo(viewContent.gameObject, iTween.Hash("x", mainCanvasRectTransform.rect.width, "time", 0.33f, "islocal", true));
		}

		ViewWillDisappear();
		Invoke("Hide", 0.33f);
	}




	public virtual void Hide()
	{
		cam.enabled = false;
		foreach (Canvas canvas in canvases) canvas.enabled = false;

		Canvas.ForceUpdateCanvases();
	}





	public virtual void ViewWillAppear() {

	}

	public virtual void ViewWillDisappear() {
		
	}
	
}











