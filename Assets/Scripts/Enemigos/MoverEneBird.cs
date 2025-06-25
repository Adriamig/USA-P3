using UnityEngine;

/* Script para el movimiento de los enemigos voladores
 * Ira asociado a los prefabs Bird y Bat
 */
public class MoverEneBird : MonoBehaviour
{
    public float dist, velocidad;
    public SoundManager sonido;

    private SpriteRenderer ene;
    private Rigidbody2D rb;
    private Vector2 velActual;

    private float pos;
    private bool cambio;
    private bool recuperaVel = false;
    private bool velAct = false;

    void Start()     // Inicializamos las variables con sus comopnentes
    {
        ene = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = transform.position.x;
        rb.velocity = new Vector2(velocidad, 0);
    }

    void Update()
    {
        // Si parar el tiempo se activa
        if (GameManager.instance.Tiempo())
        {
            if (!velAct) // Guardamos la velocidad actual del enemigo
            {
                velActual = rb.velocity;
                velAct = true;
            }
            rb.gravityScale = 0;    // Para que se quede parado en el aire
            rb.velocity = Vector2.zero; // Paramos al enemigo
            recuperaVel = true;     // Bool para activar la vel cuando el tiempo vuelva
        }
        else
        {
            velAct = false; // Bool para volver a almacenar la velocidad si se vuelve a parar
            if (recuperaVel) // Recuperamos la velocidad almacenada
            {
                rb.velocity = velActual;
                recuperaVel = false;
            }
        }


        if (transform.position.x > pos + dist)  //Controlar que no se pase de la distancia
        {
            cambio = true;
            ene.flipX = false;
        }

        else if (transform.position.x < pos - dist)
        {
            cambio = false;
            ene.flipX = true;
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.Tiempo())  // Si el tiempo no esta parado movemos al enemigo
        {
            if (cambio)
                rb.velocity = new Vector2(-velocidad * 3, 0);
            else
                rb.velocity = new Vector2(velocidad * 3, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)                         //  Sirve para reproducir el efecto de
    {                                                                       //  muerte del ave una vez lo matas.
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            sonido.deadAve();
        }
    }
}
