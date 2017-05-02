using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticleManager : MonoBehaviour {

	public ParticleSystem PickupSpark;
	public ParticleSystem PickupOuter;
	public ParticleSystem PickupInner;

	public ParticleSystem Aura;
	public ParticleSystem Auraburst;
	public ParticleSystem Auraspark;

    public ParticleSystem MissFlash;
    public ParticleSystem MissSpark;
    public ParticleSystem MissHighlight;


    private ParticleSystem.EmitParams particleSettings = new ParticleSystem.EmitParams ();

	private void Start()
	{
		
	}

	public void PlayPickup()
	{
		// null check
		if (PickupSpark != null && PickupOuter != null && PickupInner) 
		{
			PickupSpark.Emit (particleSettings, 50);
			PickupOuter.Emit (particleSettings, 2);
			PickupInner.Emit (particleSettings, 2);

			if (PickupSpark.isStopped) 
			{
				PickupSpark.Play ();
			}
			if (PickupOuter.isStopped) 
			{
				PickupOuter.Play ();
			}
			if (PickupInner.isStopped) 
			{
				PickupInner.Play ();
			}
		} 
		else 
		{
			Debug.Log ("Character does not have a pick up particle designated.");
		}
	}
    public void PlayMiss()
    {
        // null check
        if (MissFlash != null && MissSpark != null && MissHighlight)
        {
            MissFlash.Emit(particleSettings, 1);
            MissSpark.Emit(particleSettings, 50);
            MissHighlight.Emit(particleSettings, 30);

            if (MissFlash.isStopped)
            {
                PickupSpark.Play();
            }
            if (MissSpark.isStopped)
            {
                PickupOuter.Play();
            }
            if (MissHighlight.isStopped)
            {
                PickupInner.Play();
            }
        }
        else
        {
            Debug.Log("Character does not have a miss particle designated.");
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
