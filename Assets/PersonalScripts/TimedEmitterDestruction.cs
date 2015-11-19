using UnityEngine;
using System.Collections;

public class TimedEmitterDestruction : MonoBehaviour {

    private EllipsoidParticleEmitter[] _epEmitters;
    [HideInInspector]public float _particleLifespan;
    public bool _beginParticleLifespan;

    void Awake()
    {
        _epEmitters = transform.GetComponentsInChildren<EllipsoidParticleEmitter>();

        // Stops particle systems from showing
        foreach (EllipsoidParticleEmitter emitter in _epEmitters)
        {
            emitter.emit = false;
        }

        // Sets defaults for particle systems
        _particleLifespan = 2f;
        _beginParticleLifespan = false;

    }

    public void StartEmitters()
    {
        foreach (EllipsoidParticleEmitter emitter in _epEmitters)
        {
            emitter.emit = true;
        }
    }
    void FixedUpdate()
    {
        if (_beginParticleLifespan)
        {
            _particleLifespan -= Time.deltaTime;
        }

        if (_particleLifespan <= 0)
        {
            // stops particle systems 
            foreach (EllipsoidParticleEmitter emitter in _epEmitters)
            {
                emitter.emit = false;
                Destroy(emitter, 3f);
            }
        }
    }
}
