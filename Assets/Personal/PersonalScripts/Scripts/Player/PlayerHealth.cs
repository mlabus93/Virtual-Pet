using UnityEngine;
using UnityEngine.UI;
using System.Collections;


using PersonalScripts;
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if (damageImage != null)
        {
            if (damaged)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        Text healthText = GameObject.Find("healthText").GetComponent<Text>();

        if (currentHealth < 0)
        {
            
            healthText.text = "0";
            healthSlider.value = 0;
        }
        else
        {
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

        //playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        // play dead
        anim.SetFloat("Tired", .8f);
        anim.SetTrigger("Sleep");
        GetComponent<Player1StickMovement>().enabled = false;
        // perform gameover stuff
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().OnGameOver();
        //playerShooting.DisableEffects ();

        //anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
