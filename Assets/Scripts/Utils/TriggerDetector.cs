using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TriggerDetector : MonoBehaviour {

	ParticleSystem ps;
    public  seikuken  Boss;

    private Color   startColor;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    [HideInInspector] public List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    [HideInInspector] public List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    [HideInInspector] public List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        Boss = FindObjectOfType< seikuken >();
        ps.trigger.SetCollider(0, Boss);
        startColor = ps.startColor;
    }

    void OnParticleTrigger()
    {
        if (!ps.CompareTag("Boss"))
        {
            // get the particles which matched the trigger conditions this frame
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

            for (int i = 0; i < numInside; i++)
            {
                ParticleSystem.Particle p = inside[i];
                // Debug.Log(inside[i].position);
                inside[i] = p;
            }
            // iterate through the particles which entered the trigger and make them red
            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(255, 0, 0, 255);
                enter[i] = p;
            }

            // iterate through the particles which exited the trigger and make them green
            for (int i = 0; i < numExit; i++)
            {
                ParticleSystem.Particle p = exit[i];
                p.startColor = startColor;
                exit[i] = p;
            }

            // re-assign the modified particles back into the particle system
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
            // Boss.inside = inside;
        }
    }
}
