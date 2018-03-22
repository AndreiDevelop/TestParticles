using UnityEngine;

public class AudioManager : ObjectTargetBehavior 
{
	private AudioSource _audioSource;

	void Awake()
	{
		_audioSource = transform.GetChild(0).GetComponent<AudioSource> ();
	}

	protected override void ActivateUIElement()
	{
		_audioSource.Play ();
	}

	protected override void DeactivateUIElement()
	{
		_audioSource.Pause ();
	}
}
