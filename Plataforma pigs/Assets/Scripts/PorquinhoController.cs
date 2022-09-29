using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorquinhoController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D meuRB;
    private Animator minhaAnimacao;
    private BoxCollider2D boxCol;
    [SerializeField] private LayerMask layerLevel;
    [SerializeField] private float speedh = 3f;
    [SerializeField] private float espera = 2f;
   
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        minhaAnimacao = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() 
    {
        Movendo();
    }

    private void Movendo()
    {
        // mudando de direcao se bater na parede
        if (Parede())
        {
            meuRB.velocity = new Vector2(meuRB.velocity.x * -1f, meuRB.velocity.y);
        }
        // olhando para onde está indo
        if (meuRB.velocity.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
            }
        //movendo se o tempo acabar
        if (espera <= 0f)
        {
            //movendo
            int dir = Random.Range(-1, 2);
            // multiplicando a velocidade pela dir
            meuRB.velocity = new Vector2(speedh * dir, meuRB.velocity.y);
            
            
            //resetando a espera
            espera = Random.Range(2f, 4f);
            
        }
        // se a espera for maior do q zero eu diminuo ela
        else
        {
            espera -= Time.deltaTime;
        }

        // checando se estou me movendo
        minhaAnimacao.SetBool("Movendo", meuRB.velocity.x != 0);
    }

    private bool Parede()
    {
        var dir = new Vector2(Mathf.Sign(meuRB.velocity.x), 0f);
        bool parede = Physics2D.Raycast(boxCol.bounds.center, dir, 1f, layerLevel);
        Debug.DrawRay(boxCol.bounds.center, dir * 1f, Color.red);
        return parede;
    }
}
