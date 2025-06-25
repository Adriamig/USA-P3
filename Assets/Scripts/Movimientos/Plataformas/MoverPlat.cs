using UnityEngine;

/* Script que provoca que una plataforma
 * se mueva sólo cuando el jugador esté 
 * encima de una flor.
 */

public class MoverPlat : MonoBehaviour
{
    public Sprite florRoja;
    [SerializeField] private float velocidad, distancia;
    [SerializeField] private bool horizontal;


    private Sprite normal;
    private Rigidbody2D rb;
    private Vector2 posIni, velAct, velTiempo;

    private bool inicio = true;
    private bool cambio = false;

    GameObject child;


    private void OnTriggerEnter2D(Collider2D Player)            //  Controla el movimiento de la plataforma,
    {                                                           //  el cual comienza una vez se entra en el Trigger.

        if (Player.GetComponent<PlayerController>() != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = florRoja;      //  Cambia el sprite de la flor 
            if (inicio)                                                 //  mientras esté activa.
            {
                if (!horizontal)
                    rb.velocity = new Vector2(0, velocidad);
                else
                    rb.velocity = new Vector2(velocidad, 0);
                inicio = false;
            }
            else
                rb.velocity = velAct;
        }
    }

    private void OnTriggerExit2D(Collider2D Player)             //  Cuando se sale del Trigger, este
    {                                                           //  movimiento se detiene.
        if (Player.GetComponent<PlayerController>() != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = normal;     //  Vuelve a cambiar el sprite porque se desactiva.
            velAct = rb.velocity;
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void Start()            //  Definimos las variables que utilizaremos en este script.
    {
        child = this.transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        posIni = new Vector2(child.transform.position.x, child.transform.position.y);
        normal = this.GetComponent<SpriteRenderer>().sprite;

    }
    void Update()       
    {
        if (cambio)
        {
            rb.velocity = velTiempo;
            cambio = false;
        }
        if (!horizontal)
        {
            if (child.transform.position.y > posIni.y + distancia)
                rb.velocity = new Vector2(0, -velocidad);
            else if (child.transform.position.y < posIni.y - distancia)
                rb.velocity = new Vector2(0, velocidad);
        }
        else
        {
            if (child.transform.position.x > posIni.x + distancia)
                rb.velocity = new Vector2(-velocidad, 0);
            else if (child.transform.position.x < posIni.x - distancia)
                rb.velocity = new Vector2(velocidad, 0);
        }
    }
}
