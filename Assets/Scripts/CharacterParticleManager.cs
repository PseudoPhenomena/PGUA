using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticleManager : MonoBehaviour {

	public ParticleSystem Pickupflash;
	public ParticleSystem Pickupspark;
	public ParticleSystem Pickuphighlight;

	public ParticleSystem Aura;
	public ParticleSystem Auraburst;
	public ParticleSystem Auraspark;

	private ParticleSystem.EmitParams particleSettings = new ParticleSystem.EmitParams ();

	private void Start()
	{
		
	}

	public void PlayPickup()
	{
		// null check
		if (Pickupflash != null && Pickupspark != null && Pickuphighlight) 
		{
			Pickupflash.Emit (particleSettings, 1);
			Pickupspark.Emit (particleSettings, 100);
			Pickuphighlight.Emit (particleSettings, 30);

			if (Pickupflash.isStopped) 
			{
				Pickupflash.Play ();
			}
			if (Pickupspark.isStopped) 
			{
				Pickupspark.Play ();
			}
			if (Pickuphighlight.isStopped) 
			{
				Pickuphighlight.Play ();
			}
		} 
		else 
		{
			Debug.Log ("Character does not have a pick up particle designated.");
		}
	}

	// Starts the player's aura particle if it isn't playing
	public void StartAura()
	{
		if (Aura != null && Auraburst != null && Auraspark != null) 
		{
			if (!Aura.isPlaying) 
			{
				Aura.Play ();
			}
			if (!Auraspark.isPlaying) 
			{
				Auraspark.Play ();
			}
			if (!Auraburst.isPlaying) 
			{
				Auraburst.Play ();
			}

			Auraburst.Emit (particleSettings, 3);
			Auraspark.Emit (particleSettings, 20);
		}
	}

	// Stops the player's aura particle if it is playing
	public void StopAura()
	{
		if (Aura != null && Auraburst != null && Auraspark != null) 
		{
			if (Aura.isPlaying) 
			{
				Aura.Stop ();
			}
			if (Auraspark.isPlaying) 
			{
				Auraspark.Stop ();
			}
			if (Auraburst.isPlaying) 
			{
				Auraburst.Stop ();
			}
		}
	}
}
