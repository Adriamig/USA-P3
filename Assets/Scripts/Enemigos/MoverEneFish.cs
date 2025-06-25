using UnityEngine;

/* Script para el movimiento de los peces
 * Irá asociado al prefab del gaeObject Pez
 */
public class MoverEneFish : MonoBehaviour
{
    [SerializeField] private float dist, velocidad;

    private SpriteRenderer ene;
    private Rigidbody2D rb;
    private Vector2 velActual;

    private float pos;
    private float gravedad;
    private bool cambio;
    private bool recuperaVel = false;
    private bool velAct = false;

    void Start()          // Inicializamos las variables con sus comopnentes
    {
        ene = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = transform.position.y;
        rb.velocity = new Vector2(velocidad, 0);
        gravedad = rb.gravityScale;
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
                if (GameManager.instance.GetGravedad()) //Devolverle la gravedad en función de si esta invertida o no
                    rb.gravityScale = -gravedad;
                else
                    rb.gravityScale = gravedad;
            }
        }

        // Si la gravedad se activa invertimos el sprite
        if (GameManager.instance.GetGravedad())
            ene.flipX = true;
        else
        {
            ene.flipX = false;
        }
        
        if (transform.position.y > pos + dist)  //Controlar que no se pase de la distancia
        {
            cambio = true;
            ene.flipY = true;
        }
        else if (transform.position.y < pos)
        {
            cambio = false;
            ene.flipY = false;
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.Tiempo())  // Si el tiempo no esta parado movemos al enemigo
        {
            if (cambio)
                rb.velocity = new Vector2(0, -velocidad);
            else
                rb.velocity = new Vector2(0, +velocidad);
        }
    }
}
