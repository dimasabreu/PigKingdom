using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorquinhoController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D meuRB;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float espera = 2f;
    [SerializeField] private GameObject maoDireita;
    [SerializeField] private GameObject maoEsquerda;
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();
    }

    private void Movendo()
    {
        //movendo se o tempo acabar
        if (espera <= 0f)
        {
            meuRB.velocity = new Vector2(speed, meuRB.velocity.y);
            // olhando para onde está indo
            transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
            espera = Random.Range(2f, 10f);
        }
        // se a espera for maior do q zero eu diminuo ela
        else
        {
            espera--;
        }
    }
}
