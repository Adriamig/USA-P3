using UnityEngine;
using TelemetriaTL;
using TelemetriaTL.Events;

/* Script para cambiar la gravedad del mapa.
 * Poder: invierte la gravedad del personaje.
 * Irá asociado al prefab de PlayerController.
 */
public class Gravedad : MonoBehaviour
{
    private Rigidbody2D rb;
    private float gravedad;
    private SpriteRenderer jug;
    public SoundManager sonido;
    public Animator anim;
    private bool sonidoUltCap = true;

    void Start()                             //  Definimos las variables que usaremos en este script.
    {
        jug = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gravedad = rb.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && GameManager.instance.GetCapsulasRest() > 0 && !GameManager.instance.GetEscalera() 
            && !GameManager.instance.GetTiendaFisica() && !GameManager.instance.GetMenuPausa())                                        //  En caso de estar escalando, cambia la 
        {                                                                                                                              //  gravedad del jugador para que no caiga 
            rb.gravityScale *= -1;                                                                                                     //  y pueda subir por la escalera/liana.

            if (!GameManager.instance.GetGravedad())
                GameManager.instance.SetGravedad(true);
            else
                GameManager.instance.SetGravedad(false);

            GravityUp direction = rb.gravityScale > 0 ? GravityUp.Down : GravityUp.Up;
            TelemetryManager.Instance().AddEvent(new GravitySwitchEvent(direction, (int)transform.position.x, (int)transform.position.y));
        }

        if (!GameManager.instance.GetGravedad() && !GameManager.instance.GetEscalera())     //  En caso de no estar escalando.
            rb.gravityScale = gravedad;

        if (GameManager.instance.GetGravedad())      //  Rotar el sprite según la gravedad.
            jug.flipY = true;

        else
            jug.flipY = false;

        if (GameManager.instance.GetGravedad())       //  Se encarga de las animaciones que 
        {                                             //  forman parte de los efectos visuales
            anim.SetBool("Gravedad", true);           //  de la gravedad.
            anim.SetBool("Gravedad2", false);
        }
        else
        {
            anim.SetBool("Gravedad", false);
            anim.SetBool("Gravedad2", true);
        }

        // Se encarga de los efectos sonoros de la gravedad      
        if (Input.GetKeyDown(KeyCode.Mouse1) && GameManager.instance.GetCapsulasRest() >= 0 && sonidoUltCap && !GameManager.instance.GetEscalera() && !GameManager.instance.GetTiendaFisica() && !GameManager.instance.GetMenuPausa())
            sonido.audGravedad.Play();
        if (GameManager.instance.GetCapsulasRest() == 0)
            sonidoUltCap = false;
        else
            sonidoUltCap = true;
    }
}
  