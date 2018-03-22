using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectTargetBehavior : MonoBehaviour 
{
	[SerializeField]
	protected ImageTargetManager _imageTargetManager = null;

	[SerializeField]
	protected GameObject _objectSlave = null;

	protected void Awake()
	{
		DeactivateUIElement ();
	}

	protected void OnEnable()
	{
		if(_imageTargetManager != null)
		{
			_imageTargetManager.OnImageTargetFound += ActivateUIElement;
			_imageTargetManager.OnImageTargetLost += DeactivateUIElement;
		}
	}

	protected void OnDisable()
	{
		if(_imageTargetManager != null)
		{
			_imageTargetManager.OnImageTargetFound -= ActivateUIElement;
			_imageTargetManager.OnImageTargetLost -= DeactivateUIElement;
		}
	}

	protected virtual void ActivateUIElement()
	{
		_objectSlave.SetActive (true);
	}

	protected virtual void DeactivateUIElement()
	{
		_objectSlave.SetActive (false);
	}
}
