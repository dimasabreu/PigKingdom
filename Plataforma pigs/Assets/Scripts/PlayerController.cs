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
    private CapsuleCollider2D capCol;
    [SerializeField] private LayerMask layerLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        minhaAnimacao = GetComponent<Animator>();
        capCol = GetComponent<CapsuleCollider2D>();
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
        pulo();
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
        
        minhaAnimacao.SetBool("Movendo", movimento != 0);
    }

    private void pulo()
    {
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
        bool chao = Physics2D.Raycast(capCol.bounds.center, Vector2.down, .6f, layerLevel);
        return chao;
    }

    // checando se o player bateu no inimigo
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                minhaAnimacao.SetFloat("VelocidadeVertical", meuRB.velocity.y);
                meuRB.velocity = new Vector2(meuRB.velocity.x, VeloDePulo);
            }
            else
            {
                Debug.Log("tomei de frente");
            }
        }
    }

}
