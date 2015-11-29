using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PersonalScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        public int _currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


        public int currentHealth { get { return _currentHealth; } set {  _currentHealth = ClampAtFull(value);}}
        

       private int ClampAtFull(int val)
        {
            if (val > 100)
                return 100;
            if (val < 0)
                return 0;
            return val;
        }
        Animator anim;                                              // Reference to the Animator component.
        public AudioSource playerAudio;                                    // Reference to the AudioSource component.
        Player1StickMovement playerMovement;                              // Reference to the player's movement.
        //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
        public bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.

        public bool GetIsDead()
        {
            return isDead;
        }
        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<Player1StickMovement>();
            //playerShooting = GetComponentInChildren<PlayerShooting>();

            // Set the initial health of the player.
            currentHealth = startingHealth;
        }


        void Update()
        {
            // If the player has just been damaged...
            if (damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                if (damageImage != null)
                    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
        }


        public int GetCurrentHealth()
        {
            return currentHealth;
        }


        public void TakeDamage(int amount, Vector3 hitPoint )
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            Text healthText = GameObject.Find("healthText").GetComponent<Text>();

            if (currentHealth < 0)
            {

                healthText.text = "0";
                healthSlider.value = 0;
            }
            else
            {
                UpdateHealthSlider();
            }

            playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }

        public void UpdateHealthSlider()
        {
            Text healthText = GameObject.Find("healthText").GetComponent<Text>();

            healthText.text = currentHealth.ToString();
            if (healthSlider == null)
            {
                Slider[] sliders = GameObject.FindObjectsOfType<Slider>();
                // finds and sets the correct slider
                for (int i = 0; i < sliders.Length; i++)
                {
                    if (sliders[i].name == "HealthBar")
                        healthSlider = sliders[i].GetComponent<Slider>();
                }
            }
            healthSlider.value = currentHealth;
        }


        public void Death()
        {
            isDead = true;

            // play dead
            anim.SetFloat("Tired", .8f);
            anim.SetTrigger("Sleep");
            GetComponent<Player1StickMovement>().enabled = false;
            // perform gameover stuff
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().OnGameOver();
            GameObject.FindObjectOfType<LevelManager>().OnGameOver();
            //playerShooting.DisableEffects ();

            //anim.SetTrigger ("Die");

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            //playerShooting.enabled = false;
        }

        public void RestartLevel()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
