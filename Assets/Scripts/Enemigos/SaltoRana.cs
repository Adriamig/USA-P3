using UnityEngine;

/* Script para el movimiento de la rana
 * Ira asociado al prefab del gameObject Rana
 */
public class SaltoRana : MonoBehaviour
{
    public float distRight, distLeft, velocidad, salto, tiempoEntreSalto;
    public SoundManager sonido;

    private SpriteRenderer ene;
    private Rigidbody2D rb;
    private Vector2 velActual;
    private Animator anim;

    private float pos, gravedad, temp;
    private bool cambio, suelo;
    private bool recuperaVel = false;
    private bool velAct = false;
    private bool cambioSalto = false;

    void Start()        //  Definimos todas las variables que utilizaremos en este script.
    {
        ene = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pos = transform.position.x;
        rb.velocity = new Vector2(velocidad, 0);
        gravedad = rb.gravityScale;
        temp = tiempoEntreSalto;
    }

    void Update()
    {
        temp = temp - Time.deltaTime;
        if (GameManager.instance.Tiempo())
        {
            if (!velAct)
            {
                velActual = rb.velocity;
                velAct = true;
            }
            rb.gravityScale = 0;      //  Para que se quede parado en el aire.
            rb.velocity = new Vector2(0, 0);
            recuperaVel = true;
        }
        else if (!GameManager.instance.Tiempo())            //  Recupera la velocidad de antes una vez 
        {                                                   //  el tiempo deje de estar detenido.
            velAct = false;
            if (recuperaVel)
            {
                rb.velocity = velActual;
                recuperaVel = false;
                if (GameManager.instance.GetGravedad())     //  Devolverle la gravedad en función 
                    rb.gravityScale = -gravedad;            //  de si está invertida o no.
                else
                    rb.gravityScale = gravedad;
            }
        }
        if (GameManager.instance.GetGravedad())             //  Cambia la gravedad de la rana.
        {
            if (!cambioSalto)
            {
                salto = -salto;
                cambioSalto = true;
            }
        }
        else
        {
            if (cambioSalto)
            {
                salto = -salto;
                cambioSalto = false;
            }
        }
        if (rb.velocity.y >= -0.01 && rb.velocity.y <= 0.01) suelo = true;
        else suelo = false;
        if (transform.position.x > pos + distRight && suelo)          //  Controlar que no se pase de la distancia
        {
            cambio = true;
            ene.flipX = true;
        }

        else if (transform.position.x < pos - distLeft && suelo)
        {
            cambio = false;
            ene.flipX = false;
        }
        if (suelo) anim.SetBool("Volar", false);
        else anim.SetBool("Volar", true);
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.Tiempo())             //  Siempre que el tiempo no esté detenido, la rana
        {                                               //  hará el mismo movimiento hacia los dos lados saltando.
            if (cambio && suelo)
            {
                if (temp <= 0)
                {
                    rb.AddForce(new Vector2(-velocidad, salto), ForceMode2D.Impulse);
                    if(sonido.audRana != null)
                        sonido.audRana.Play();
                    temp = tiempoEntreSalto;
                }
            }
            else if (suelo)
                if (temp <= 0)
                {
                    rb.AddForce(new Vector2(velocidad, salto), ForceMode2D.Impulse);
                    if (sonido.audRana != null)
                        sonido.audRana.Play();
                    temp = tiempoEntreSalto;
                }
        }

        if (GameManager.instance.Tiempo()) sonido.audRana.Stop();

    }
    private void OnTriggerEnter2D(Collider2D other)                         //  Sirve para reproducir el efecto de
    {                                                                       //  muerte de la rana una vez la matas.
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            sonido.deadRana();
            Debug.Log("Has matado");
        }
    }

}