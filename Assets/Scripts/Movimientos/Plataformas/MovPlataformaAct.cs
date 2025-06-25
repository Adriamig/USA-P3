using UnityEngine;

/* Script que hace que la plataforma se
 * mueva una vez el jugador se sube encima
 * y va hacia un punto en concreto.
 */

public class MovPlataformaAct : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject child;

    [SerializeField] private float deltaX;
    [SerializeField] private bool inicio = true;
    [SerializeField] private Sprite florRoja, florAzul;


    void Start()                                                  //  Convierte en hija la flor
    {
        child = this.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D Player)               //  Con el trigger de la flor, se encarga
    {                                                             //  de activar los movimientos.
                                                                                
        if (Player.GetComponent<PlayerController>() && inicio) Mov1();
        else if (Player.GetComponent<PlayerController>() && !inicio) Mov2();
    }

    public void Movimiento()     //  Aplica movimiento a la plataforma.
    {
        deltaX = 3;
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(deltaX, 0);
    }

    public void Mov1()           //  Cambia el sprite de la flor y realiza 
    {                            //  el movimiento parándolo después.

        child.GetComponent<SpriteRenderer>().sprite = florRoja;
        Movimiento();
        Invoke("Res2", 3.0f);
        inicio = false;
    }

    public void Mov2()           //  Hace el mismo movimiento
    {                            //  en sentido contrario.
        if (!inicio)
        {
            child.GetComponent<SpriteRenderer>().sprite = florRoja;
            deltaX = -3;
            rb.velocity = new Vector2(deltaX, 0);
            Invoke("Res1", 3.0f);
        }
    }


    public void Res1()         //  Junto con Res2(), se encarga de detener la plataforma y cambiar 
    {                          //  el booleano para que funcione el movimiento y se repita.
    
        rb.velocity = new Vector2(0, 0);
        inicio = true;
        child.GetComponent<SpriteRenderer>().sprite = florAzul;
    }
    public void Res2()
    {
        rb.velocity = new Vector2(0, 0);
        inicio = false;
        child.GetComponent<SpriteRenderer>().sprite = florAzul;
    }
    
}
