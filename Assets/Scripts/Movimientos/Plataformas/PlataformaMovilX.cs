using UnityEngine;

/* Script que mueve el objeto asociado a él
 * en un movimiento oscilante sobre el eje X
 * en otra dirección.
 */

public class PlataformaMovilX : MonoBehaviour
{
    [SerializeField] private float dist, velocidad;

    private Rigidbody2D rb;
    private Vector2 velActual;

    private float pos;

    private bool cambio;
    private bool recuperaVel = false;
    private bool velAct = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos = transform.position.x;
    }

    void Update()
    {
        if (GameManager.instance.Tiempo())          //  Se encarga de detener la plataforma
        {                                           //  en caso de que se pare el tiempo.
            if (!velAct)
            {
                velActual = rb.velocity;
                velAct = true;
            }
            rb.velocity = new Vector2(0, 0);
            recuperaVel = true;
        }
        else if (!GameManager.instance.Tiempo())    //  Devuelve la velocidad que tenía antes
        {                                           //  una vez el tiempo deje de estar parado.
            velAct = false;
            if (recuperaVel)
            {
                rb.velocity = velActual;
                recuperaVel = false;
            }
        }

        if (transform.position.x > pos + dist)      //  Controlar que no se pase de la distancia
        {
            cambio = true;
        }

        else if (transform.position.x < pos - dist)
        {
            cambio = false;
        }
    }
    private void FixedUpdate()      
    {
        if (!GameManager.instance.Tiempo())         //  Aplica velocidad siempre que no se detenga el tiempo.
        {
            if (cambio)
                rb.velocity = new Vector2(-velocidad,0);
            else
                rb.velocity = new Vector2(velocidad,0);
        }
    }
}