using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Funções primarias")]
    private Rigidbody2D meuRB;
    private SpriteRenderer SpritePlayer;
    private Animator minhaAnimacao;
    
    [SerializeField] private float VeloDeMov = 5f;
    [SerializeField] private float VeloDePulo = 7f;
    [SerializeField] private int totalPulos = 2;
    [SerializeField] private int qtdPulo = 2;

    [Header ("Raycast")]
    private BoxCollider2D boxCol;
    [SerializeField] private LayerMask layerLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        SpritePlayer = GetComponent<SpriteRenderer>();
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
        if(Input.GetKey(KeyCode.A))
        {
            minhaAnimacao.SetBool("Movendo",true);
            // fazendo a sprite virar para a esquerda
            SpritePlayer.flipX = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            minhaAnimacao.SetBool("Movendo",true);
            //fazendo a sprite virar para a direta
            SpritePlayer.flipX = false;
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
            //minhaAnimacao.SetBool("NoChao", false);
        }
    }

    // criando o raycast
    private bool IsGrounded()
    {
        bool chao = Physics2D.Raycast(boxCol.bounds.center, Vector2.down, .5f, layerLevel);
        return chao;
    }
}
