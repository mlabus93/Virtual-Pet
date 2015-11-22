using UnityEngine;
using System.Collections;

public class DollHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;

    Animator anim;
    ParticleSystem hitParticles;
    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        currentHealth = startingHealth;
    }

    public void Reset()
    {
        currentHealth = startingHealth;
        anim.SetTrigger("Idle");
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        anim.SetTrigger("Dead");
    }
}
