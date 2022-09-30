using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateMelee : MonoBehaviour
{
   private float timeBtwAttack;
   public float startTimeBtwAttack;

   public Transform attackPos;
   public LayerMask whatIsEnemies;
   public float attackRange;
   public int dano;

   public Animator anim;

   void Update() 
   {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("Bati");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponentInParent<PorquinhoController>().perdeVida(dano);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
   }

   private void OnDrawGizmosSelected() 
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
   }
}
