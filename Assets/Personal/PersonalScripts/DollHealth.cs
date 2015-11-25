using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class DollHealth : MonoBehaviour, IDamageable
    {
        public int startingHealth = 10;
        public int currentHealth;
        Animator anim;
        bool isDown;

        void Awake()
        {
            anim = GetComponent<Animator>();
            currentHealth = startingHealth;
        }

        public void Reset()
        {
            currentHealth = startingHealth;
            //anim.SetTrigger("Idle");
            isDown = false;
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (isDown)
            {
                return;
            }
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Death();
            }
        }


        public void Death()
        {
            isDown = true;
            anim.SetTrigger("Dead");
        }

        public void StartSinking()
        {
            anim.SetTrigger("NotPlaying");
        }

    }
}

