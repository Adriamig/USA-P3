using UnityEngine;

/* Script encargado del movimiento del jugador.
 */

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer jug;
    public ParticleSystem part;
    public ParticleSystem part1;

    AudioSource jumpSound;


    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float jumpForce = 12.0f;
    [SerializeField] private float movimientoInput;
    [SerializeField] private float gravedad;

    private bool isGrounded;
    private bool isWalking;

    void Start()
    {
        jug = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravedad = rb.gravityScale;
        jumpSound = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Escalar>() != null)  //  Al estar en la escalera tener g = 0 y tener input vertical.
        {
            GameManager.instance.SetEscalera(true);
            rb.gravityScale = 0;
            if (Input.GetKeyDown(KeyCode.W))
            {
                movimientoInput = Input.GetAxisRaw("Vertical");

                rb.velocity = new Vector2(rb.velocity.x, movimientoInput * speed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)  //  Al salir de la escalera volver a tener la g anterior.
    {
        if (other.GetComponent<Escalar>() != null)
        {
            GameManager.instance.SetEscalera(false);
            if (GameManager.instance.GetGravedad())
                rb.gravityScale = -gravedad;
            else
                rb.gravityScale = gravedad;
        }
    }

    void Update()              //  Llama a todos los métodos que
                               //  funcionan para el movimiento.
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        isGrounded = GameManager.instance.GetSuelo();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckMovementDirection()
    {
        if (movimientoInput < 0)        //  Rotar el sprite según la dirección.
            jug.flipX = true;


        else if (movimientoInput > 0)
            jug.flipX = false;



        if (rb.velocity.x != 0)         //  Poner la animación correspondiente dependiendo
            isWalking = true;           //  de si el jugador se está moviendo o no.

        else
            isWalking = false;

    }

    private void UpdateAnimations()         //  Actualiza las animaciones según los booleanos.
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void CheckInput()       //  Traduce el input para convertirlo en movimiento.
    {

        Particula1();
        movimientoInput = Input.GetAxisRaw("Horizontal");

        if (GameManager.instance.GetParedL() && movimientoInput < 0)  //  Si está chocando con la paredL no funcione el input L
            movimientoInput = 0;

        else if (GameManager.instance.GetParedR() && movimientoInput > 0)
            movimientoInput = 0;

        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Jump()
    {
        SonidoSalto();
        if (isGrounded && !GameManager.instance.GetGravedad() && !GameManager.instance.GetEscalera())      //Salto con gravedad desactivada
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        else if (isGrounded && GameManager.instance.GetGravedad() && !GameManager.instance.GetEscalera())  //Salto con gravedad activada
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);

        Particula();
        SonidoSalto();
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(speed * movimientoInput, rb.velocity.y);

    }
    private void Particula()        //  Se encarga de mostrar las partículas  
    {                               //  cuando está en el suelo.
        if (isGrounded)
            part.Play();

        if (GameManager.instance.GetGravedad() && isGrounded)
            part.transform.position = new Vector3(part.transform.position.x, transform.position.y + 0.6f, part.transform.position.z);

        if (!GameManager.instance.GetGravedad() && isGrounded)
            part.transform.position = new Vector3(part.transform.position.x, transform.position.y - 0.8f, part.transform.position.z);
    }

    private void Particula1()       //  Se encarga de mostrar las 
    {                               //  partículas cuando camina.
        if (isWalking)
            part1.Play();

        if (GameManager.instance.GetGravedad() && isWalking)
            part1.transform.position = new Vector3(part1.transform.position.x, transform.position.y + 0.8f, part1.transform.position.z);

        if (!GameManager.instance.GetGravedad() && isWalking)
            part1.transform.position = new Vector3(part1.transform.position.x, transform.position.y - 0.8f, part1.transform.position.z);

        else if (GameManager.instance.GetParedL())
            part1.Stop();

        if (GameManager.instance.GetParedR())
            part1.Stop();

        else if (!isGrounded)
            part1.Stop();

    }
    private void SonidoSalto()      //  Se encarga de reproducir el 
    {                               //  efecto sonoro del salto.
        if (isGrounded)        
            jumpSound.Play();
        
        else if (isGrounded)        
            jumpSound.Stop();        
    }
}
