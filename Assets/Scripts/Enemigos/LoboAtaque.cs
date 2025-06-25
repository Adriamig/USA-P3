using UnityEngine;

/* Movimiento y ataque del lobo 
 * Ira asociado al prefab gameObject Lobo
 */

public class LoboAtaque : MonoBehaviour
{

    public float ataque, atacar, salto, temp, tiempoE;
    private float gravedad;
    private Vector2 velActual;

    public DetectorLobo scriptDet;    
    public SoundManager sonido;

    private Rigidbody2D rb;
    private Animator anim;

    private bool recuperaVel = false;
    private bool cambioSalto = false;
    private bool velAct = false;
    public bool inicio = true;
    public bool iniciado = false;


    public void Ataque()                    //  Método que realiza el salto hacia el jugador.
    {
        if (!GameManager.instance.Tiempo())
        {
            if (inicio && temp <= 0)
            {
                anim.Play("LoboAtaque");
                rb.AddForce(new Vector2(ataque, salto), ForceMode2D.Impulse);
                temp = tiempoE;
                Invoke("RestartAnim", 1.0f);
                inicio = false;
                sonido.audLobo.Play();
            }
        }
    }

    public void Vuelta()                  //  Método que devuelve al lobo a su posición inicial. 
    {
        if (!GameManager.instance.Tiempo())
        {
            if (inicio == false && temp <= 0)
            {
                anim.Play("LoboVuelta");
                rb.AddForce(new Vector2(atacar, salto), ForceMode2D.Impulse);
                //Debug.Log("Volviendo");
                temp = tiempoE;
                Invoke("RestartAnim", 1.0f);
                inicio = true;
            }
        }

        Invoke("RestartBool", 2f);
    }
    public void RestartBool()                   //  Junto con RestartAnim() y Espera(), 
    {                                           //  son métodos auxiliares para poder 
        scriptDet.iniciar = false;              //  realizar el movimiento del lobo.
    }
    public void RestartAnim()
    {
        anim.Play("Parado");
    }
    public void Espera()
    {
        inicio = true;
    }

    public void Repeticion()                    //  Comprime dos métodos en uno para 
    {                                           //  reducir código y que se llamen los
        Ataque();                               //  dos a la vez.
        Vuelta();
    }

    void Start()                                            //  Definimos las variables que utilizaremos en este script.
    {
        rb = this.GetComponent<Rigidbody2D>();
        gravedad = rb.gravityScale;
        anim = GetComponent<Animator>();
        temp = tiempoE;
    }

    void Update()
    {
        temp -= Time.deltaTime;

        if (GameManager.instance.Tiempo())              //  Se encarga del movimiento del lobo
        {                                               //  teniendo en cuenta el tiempo.
            if (!velAct)
            {
                velActual = rb.velocity;
                velAct = true;
            }
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            recuperaVel = true;

        }
        else if (!GameManager.instance.Tiempo())        //  Le devuelve la velocidad en caso de
        {                                               //  que el tiempo estuviera detenido con
            velAct = false;                             //  el lobo en medio del movimiento.
            if (recuperaVel)
            {
                rb.velocity = velActual;
                recuperaVel = false;
                if (GameManager.instance.GetGravedad())      // Devolverle la gravedad en función de si está invertida o no.
                    rb.gravityScale = -gravedad;
                else
                    rb.gravityScale = gravedad;
            }
        }
        if (GameManager.instance.GetGravedad())         //  Invierte la gravedad del lobo.
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

    }
    private void OnTriggerEnter2D(Collider2D other)                         //  Sirve para reproducir el efecto de
    {                                                                       //  muerte del lobo una vez lo matas.
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            sonido.deadLobo();
        }
    }

}