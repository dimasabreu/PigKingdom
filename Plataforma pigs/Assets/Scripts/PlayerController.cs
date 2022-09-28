using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Funções primarias")]
    private Rigidbody2D meuRB;
    private Animator minhaAnimacao;
    
    [SerializeField] private float VeloDeMov = 5f;
    [SerializeField] private float VeloDePulo = 7f;
    [SerializeField] private int totalPulos = 1;
    [SerializeField] private int qtdPulo = 1;

    [Header ("Raycast")]
    private BoxCollider2D boxCol;
    [SerializeField] private LayerMask layerLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        minhaAnimacao = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // melhor metodo para fisica
    private void FixedUpdate() 
    {
        minhaAnimacao.SetBool("NoChao", IsGrounded());
        // resetando o pulo ao tocar no chao
        if (IsGrounded())
        {
            qtdPulo = totalPulos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movendo();
    }

    private void Movendo()
    {
        // movendo para os lados e trocando de sprite
        var movimento = Input.GetAxis("Horizontal") * VeloDeMov;
        meuRB.velocity = new Vector2(movimento, meuRB.velocity.y);
        // fazendo a sprite virar com o player
        if (movimento != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movimento), 1f, 1f);
        }
        // input de movimentacao
        if(Input.GetKey(KeyCode.A))
        {
            minhaAnimacao.SetBool("Movendo",true);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            minhaAnimacao.SetBool("Movendo",true);
    
        }
        else
        {
            minhaAnimacao.SetBool("Movendo",false);
        }

        // pulo
        minhaAnimacao.SetFloat("VelocidadeVertical", meuRB.velocity.y);
        var pulo = Input.GetButtonDown("Jump");
        if (pulo && qtdPulo > 0)
        {
            meuRB.velocity = new Vector2(meuRB.velocity.x, VeloDePulo);
            qtdPulo--;
        }
    }

    // criando o raycast
    private bool IsGrounded()
    {
        bool chao = Physics2D.Raycast(boxCol.bounds.center, Vector2.down, .6f, layerLevel);
        return chao;
    }
}
