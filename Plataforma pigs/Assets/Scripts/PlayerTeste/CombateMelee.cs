using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateMelee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Inimigo"))
        {
            collision.gameObject.GetComponent<PorquinhoController>().perdeVida(1);
            collision.GetComponentInParent<Animator>().SetTrigger("DanoMorte");
        }
    }
}
