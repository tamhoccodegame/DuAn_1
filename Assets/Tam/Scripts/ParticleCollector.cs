using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    ParticleSystem ps;
    
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Collider2D triggerCollider = GameObject.Find("Particle Collector").GetComponent<Collider2D>();
        ps.trigger.SetCollider(0, triggerCollider);
    }

	private void OnParticleTrigger()
	{
		int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for(int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            particles[i] = p;
			FindObjectOfType<Player_Health>().RestoreHealth(2);
		}

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
	}
}
