using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ParticlesManager : MonoBehaviour 
{
	private const string PATH_TO_PARTICLES = "MobileParticles";
	private const float DELAY_INITIALIZE = 0.01f;

	[SerializeField]
	private List<GameObject> _particlesList = new List<GameObject>();

	[SerializeField]
	private ImageTargetManager _imageTargetManager = null;

	[SerializeField]
	private Text _numParticleText = null;

	private int _particleIndexToActivate = 0;
	private Transform _curTransform = null;
	private GameObject _currentParticle = null;

	void Start()
	{
		_curTransform = transform;

		StartCoroutine (InitializeCoroutine ());
	}

	private IEnumerator InitializeCoroutine()
	{
		Object[] _bufMass = Resources.LoadAll (PATH_TO_PARTICLES);

		foreach (Object bufObject in _bufMass) 
		{
			_currentParticle = bufObject as GameObject;
			_currentParticle = Instantiate (_currentParticle, _curTransform.position, _curTransform.rotation);

			yield return new WaitForSeconds (DELAY_INITIALIZE);

			_currentParticle.transform.SetParent (_curTransform);
			_currentParticle.SetActive (false);

			_particlesList.Add (_currentParticle);
		}

		_currentParticle = _particlesList.First ();
	}

	void OnEnable()
	{
		if(_imageTargetManager != null)
		{
			_imageTargetManager.OnImageTargetFound += ActivateParticleFromList;
			_imageTargetManager.OnImageTargetLost += DeactivateParticleFromList;
		}
	}

	void OnDisable()
	{
		if(_imageTargetManager != null)
		{
			_imageTargetManager.OnImageTargetFound -= ActivateParticleFromList;
			_imageTargetManager.OnImageTargetLost -= DeactivateParticleFromList;
		}
	}

	public void NextParticleToActivate()
	{
		_particleIndexToActivate = ((_particleIndexToActivate + 1) <= _particlesList.Count- 1) ? _particleIndexToActivate + 1 : 0;
		SetNewCurrentParticle ();
	}

	public void PrevParticleToActivate()
	{
		_particleIndexToActivate = ((_particleIndexToActivate - 1) >= 0) ? _particleIndexToActivate - 1 : _particlesList.Count - 1;
		SetNewCurrentParticle ();
	}

	private void SetNewCurrentParticle()
	{
		_currentParticle.SetActive(false);
		_currentParticle = _particlesList [_particleIndexToActivate];
		_currentParticle.SetActive(true);

		_numParticleText.text = _particleIndexToActivate.ToString();
	}

	private void ActivateParticleFromList()
	{
		_currentParticle.SetActive (true);
	}

	private void DeactivateParticleFromList()
	{
		_currentParticle.SetActive (false);
	}
}
