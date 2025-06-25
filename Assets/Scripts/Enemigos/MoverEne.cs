using UnityEngine;

/* Script del movimiento del enemigo en tierra
 * Irá asociado al prefab Dino
 */
public class MoverEne : MonoBehaviour
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
  
    void Start()    // Inicializamos las variables con sus comopnentes
    {
        ene = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = transform.position.x;
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

        if (transform.position.x > pos + dist)  //Controlar que no se pase de la distancia
        {
            cambio = true;
            ene.flipX = true;      
        }

        else if (transform.position.x < pos - dist)
        {
            cambio = false;
            ene.flipX = false;
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.Tiempo()) // Si el tiempo no esta parado movemos al enemigo
        {
            if (cambio)
                rb.velocity = new Vector2(-velocidad * 3, -rb.gravityScale);
            else
                rb.velocity = new Vector2(velocidad * 3, -rb.gravityScale);
        }        
    }
}
