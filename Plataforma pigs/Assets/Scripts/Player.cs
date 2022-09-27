using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Funções primarias")]
    private Rigidbody2D meuRB;
    private SpriteRenderer SpritePlayer;
    [SerializeField] private float VeloDeMov = 5f;
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        SpritePlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();
    }

    private void Movendo()
    {
        var movimento = Input.GetAxis("Horizontal") * VeloDeMov;
        meuRB.velocity = new Vector2(movimento, meuRB.velocity.y);
        if(Input.GetKeyDown(KeyCode.A))
        {
            // fazendo a sprite virar para a esquerda
            SpritePlayer.flipX = true;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            //fazendo a sprite virar para a direta
            SpritePlayer.flipX = false;
        }
        
    }
}
