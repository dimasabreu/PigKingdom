using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaController : MonoBehaviour
{
   private Animator anim;

   void Start()
   {
    anim = GetComponent<Animator>();
   }

    public void Abrindo()
    {
        anim.SetTrigger("Abri");
    }
    public void Fechando()
    {
        anim.SetTrigger("Fechei");
    }
}
