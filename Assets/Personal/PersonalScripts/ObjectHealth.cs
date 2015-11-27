using UnityEngine;

namespace PersonalScripts
{
    public class ObjectHealth : MonoBehaviour, IDamageable
    {
        public int startingHealth = 100;            // The amount of health the Object starts the game with.
        public int currentHealth;                   // The current health the Object has.
        public float destroySpeed = 2.5f;              // The speed at which the Object sinks through the floor when dead.
        public int scoreValue = 10;                 // The amount added to the player's score when the Object dies.
        public AudioClip deathClip;                 // The sound to play when the Object dies.


        Animator anim;                              // Reference to the animator.
        AudioSource objectAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the Object is damaged.
        Collider Collider;                          // Reference to the capsule collider.
        bool isDead;                                // Whether the Object is dead.
        bool isSinking;                             // Whether the Object has started sinking through the floor.


        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            objectAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            Collider = GetComponent<Collider>();

            // Setting the current health when the Object first spawns.
            currentHealth = startingHealth;
        }


        void Update()
        {
            // If the Object should be sinking...
            if (isSinking)
            {
                // ... move the Object down by the sinkSpeed per second.
                transform.Translate(-Vector3.up * destroySpeed * Time.deltaTime);
            }
        }


        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            // If the Object is dead...
            if (isDead)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect.
            objectAudio.Play();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;

            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();

            // If the current health is less than or equal to zero...
            if (currentHealth <= 0)
            {
                // ... the Object is dead.
                Death();
            }
        }

        public void Death()
        {
            // The Object is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            Collider.isTrigger = true;

            // Tell the animator that the Object is dead.
            anim.SetTrigger("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            objectAudio.clip = deathClip;
            objectAudio.Play();
        }


        public void StartSinking()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent<NavMeshAgent>().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the Object).
            GetComponent<Rigidbody>().isKinematic = true;

            // The Object should no sink.
            isSinking = true;

            // Increase the score by the Object's score value.
            LevelManager._score += scoreValue;

            // After 2 seconds destory the Object.
            Destroy(gameObject, 2f);
        }
    }

}
