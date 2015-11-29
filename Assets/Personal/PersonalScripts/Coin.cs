using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour, IInteractable, IGivePoints
    {
        Collider _collider;
        IMinigameLevelManager _levelManager;
        public int _coinValue;
        TimedEmitterDestruction _timedDestruction;
        public bool _pointsCollected { get; set; }

        void Awake()
        {
            _collider = transform.GetComponent<Collider>();
            _levelManager = FindObjectOfType<LevelManager>();

            // Check if levelManager present
            if (_levelManager == null)
            {
                Debug.LogError("Level Manager Not Found!!");
            }
            _coinValue = 13;
            // Gets a list of all particle emitters
            _timedDestruction = transform.GetComponentInChildren<TimedEmitterDestruction>();
            _pointsCollected = false;

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Interact();
            }
        }
        public void Interact()
        {
            // Prevents player from accruing extra points on same coin
            if (!_pointsCollected)
            {
                GivePoints(_coinValue);
                _pointsCollected = true;
            }

            GetComponent<AudioSource>().Play();
            // starts particle systems 
            _timedDestruction.StartEmitters();
            _timedDestruction._beginParticleLifespan = true;
            // Allows coin to be destroyed without particle systems
            transform.DetachChildren();
            Destroy(gameObject, 3f);
        }
        public void GivePoints(int amount)
        {
            _levelManager.AddPoints(amount);
        }
        
    }
}

