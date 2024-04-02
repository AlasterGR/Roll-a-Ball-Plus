using System;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    #region Variables
    public VisualFX[] visualEffects;
	#endregion

	#region Unity Methods
	/*protected override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{

	}

	private void Update()
	{

	}*/
	#endregion

	#region Methods
	public GameObject PlayVFXAtPoint(string _name, Vector3 _position, Transform _parentTransform)
	{
		// Get Sound
		VisualFX visualEffect = GetVFX(_name);
		if (visualEffect == null)
			return null;

		// Create a new game object
		GameObject visualEffectGameObject = Instantiate(visualEffect.visualEffectPrefab, _position, Quaternion.identity);
		visualEffectGameObject.name = "VisualEffect";

		// Set child
		if (_parentTransform != null)
			visualEffectGameObject.transform.parent = _parentTransform;

		// Get Particle System component
		ParticleSystem particleSystem = visualEffectGameObject.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule mainModule = particleSystem.main;

		// Set values
		mainModule.loop = visualEffect.loop;
		mainModule.simulationSpeed = visualEffect.simulationSpeed;
		visualEffectGameObject.transform.localScale *= visualEffect.scale;
		if (mainModule.loop == false)
			Destroy(visualEffectGameObject, (mainModule.duration / visualEffect.simulationSpeed)); // HAS TO BE REMOVED IF POOLING IS ADDED!!!

		// Play visual effect
		particleSystem.Play();

		return visualEffectGameObject;
	}

	public VisualFX GetVFX(string _name)
	{
		VisualFX visualEffect = Array.Find(visualEffects, v => v.name == _name);
		if (visualEffect == null)
		{
			Debug.LogWarning("VFX: " + _name + " not found!");
			return null;
		}

		return visualEffect;
	}
	#endregion
}
