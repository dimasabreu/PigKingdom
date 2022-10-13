using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaController : MonoBehaviour
{
    [SerializeField] public string destino;
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

    public void EnviandoDestino()
    {
        FindObjectOfType<TrocaDeLevel>().IndoDestino(destino);
    }
}
