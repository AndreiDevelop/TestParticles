using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;

public class ImageTargetManager : DefaultTrackableEventHandler
{
	public delegate void ImageTargetTrackedHandler();

	public event ImageTargetTrackedHandler OnImageTargetFound;
	public event ImageTargetTrackedHandler OnImageTargetLost;

	protected override void OnTrackingFound()
	{
		if (OnImageTargetFound != null)
			OnImageTargetFound ();
	}


	protected override void OnTrackingLost()
	{
		if (OnImageTargetLost != null)
			OnImageTargetLost ();
	}
}
