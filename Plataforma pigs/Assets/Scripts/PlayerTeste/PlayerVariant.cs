using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariant : MonoBehaviour
{
    public ParticleSystem dust;
    [Header("Status")]
    [SerializeField] private int vida = 2;
    [SerializeField] private float esperaDano = 0f;

    [Header("Movimentação horizontal")]
    [SerializeField] public float moveSpeed = 5f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Movimentação vertical")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Componentes")]
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask Level;
    public GameObject characterHolder;

    [Header("Fisica")]
    public float maxSpeed = 5f;
    public float linearDrag = 15f;
    public float gravity = 1;
    public float fallMultiplier = 5f;
    

    [Header("Colisão")]
    public bool noChao = false;
    public float groundLenght = 0.1f;
    public Transform pedireito;
    public Transform peesquerdo;
    private bool morto = false;
    [SerializeField] private EdgeCollider2D  colisor;
    [SerializeField] private portaController portaAtual;
    [SerializeField] private bool DoorAction = false;
   
    

    // CODIGO!!!!!!!!!!!!!!!
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(!morto)
        {
            Invencibiliade();
            VendosedapraPula();
            AbrindoPorta();
            EntrandoPorta();
        }
    }

     private void FixedUpdate() 
    {
        if (vida >= 0)
        {
            moveCharacter(direction.x);
            if(jumpTimer > Time.time && noChao)
            {
                Jump();
            }
            modifyPhysics();
        }
        else
        {
            Morrendo();
        }
    }
    private void VendosedapraPula()
    {
        bool estavanoChao = noChao;
        noChao = Physics2D.Raycast(pedireito.position, Vector2.down, groundLenght, Level) || Physics2D.Raycast(peesquerdo.position, Vector2.down, groundLenght, Level);
        if (!estavanoChao && noChao)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);
        anim.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("vertical", rb.velocity.y);
        if((horizontal > 0 && !facingRight) | (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    public void Jump()
    {
        
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.8f, 1.2f, 0.1f));
    }

    public void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (noChao)
        {
            if(Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
        
    }

    public void Flip()
    {
        CreateDust();
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pedireito.position , transform.position);
        Gizmos.DrawLine(peesquerdo.position , transform.position);
    }

    private void  Invencibiliade()
    {
        if (esperaDano > 0f)
        {
            esperaDano -= Time.deltaTime;
        }
    }

    // checando se o player bateu no inimigo
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                collision.GetComponentInParent<Animator>().SetTrigger("DanoMorte");
            }
            else
            {
                if (!morto)
                {
                    if (esperaDano <= 0f)
                    {
                        vida--;
                        esperaDano = 2f;
                        anim.SetTrigger("Dano");

                        //informando a vida para o anim
                        anim.SetInteger("Vida", vida);
                    }
                } 
            }
            
        }
        if (collision.gameObject.CompareTag("Porta"))
        {
            portaAtual = collision.GetComponent<portaController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Porta"))
        {
            portaAtual = null;
        }
    }
    public void Morrendo()
    {
        morto = true;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        colisor.enabled = false;
    }
    public void CreateDust()
    {
        dust.Play();
    }

    private void AbrindoPorta()
    {
        if (portaAtual != null && !morto)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(!DoorAction)
                {
                    portaAtual.Abrindo();
                    DoorAction = true;
                }
                else
                {
                    portaAtual.Fechando();
                    DoorAction = false;
                }
                
            }  
        }
    }

    private void EntrandoPorta()
    {
        if(DoorAction && Input.GetKeyDown(KeyCode.W) && !morto)
        {
            Invoke("Entrei", 0.15f);
        } 
    }

    private void Entrei()
    {
        anim.SetTrigger("EntrandoPorta");
        morto = true;
    }
}
