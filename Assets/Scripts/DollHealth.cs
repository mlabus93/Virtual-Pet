using UnityEngine;
using System.Collections;

public class DollHealth : EnemyHealth
{
    new public int startingHealth = 10;
    Animator anim;
    ParticleSystem hitParticles;
    bool isDown;

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

    public new void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDown)
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
        isDown = true;
        anim.SetTrigger("Dead");
    }

    new public void StartSinking()
    {
    }
}
