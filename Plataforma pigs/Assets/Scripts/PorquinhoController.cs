using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorquinhoController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D meuRB;
    private Animator minhaAnimacao;
    private BoxCollider2D boxCol;
    private bool morto = false;
    [SerializeField] private LayerMask layerLevel;
    [SerializeField] private float speedh = 3f;
    [SerializeField] private float espera = 2f;
    [SerializeField] private BoxCollider2D colisor;
    [SerializeField] public int vida = 2;
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        minhaAnimacao = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        minhaAnimacao.SetInteger("Vida", vida);
    }
    private void FixedUpdate() 
    {
        if (!morto)
        {
            Movendo();
        }
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

    // vendo se estou batendo na parade e mudando a direcao
    private bool Parede()
    {
        var dir = new Vector2(Mathf.Sign(meuRB.velocity.x), 0f);
        bool parede = Physics2D.Raycast(boxCol.bounds.center, dir, 1f, layerLevel);
        Debug.DrawRay(boxCol.bounds.center, dir * 1f, Color.red);
        return parede;
    }


    public void perdeVida(int dano)
    {
        vida -= dano;
        meuRB.velocity = new Vector2(0f, meuRB.velocity.y + 4f);
        
        if (vida < 0)
        {
            minhaAnimacao.SetTrigger("DanoMorte");
            morrendo();
        }
        
    }

    // criando o metodo para morrer
    public void morrendo()
    {
        morto = true;
        // tirando a velocidade
        meuRB.velocity = Vector2.zero;
        // destruindo o gameobj
        Destroy(gameObject, 2f);
        // desabilitando o colisor
        colisor.enabled = false;
    }

}
