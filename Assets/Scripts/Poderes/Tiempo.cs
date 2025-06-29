using UnityEngine;
using TelemetriaTL;
using TelemetriaTL.Events;

/* Scrip para activar el poder del tiempo.
 * Poder: para el tiempo de todo el mapa.
 * Estará asociado en el`prefab PlayerController.
 */
public class Tiempo : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start()  
    {
        sprite = GetComponent<SpriteRenderer>();        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && !GameManager.instance.GetTiendaFisica() && !GameManager.instance.GetMenuPausa())      //  Cuando presiona Shift o Clic derecho,
        {
            if (GameManager.instance.CambioTiempo())     // si se ha activado el tiempo, reproduce los efectos 
            {                                            // visuales del tiempo por cuánto sea que dure.
                CambiarFondo();
                Invoke("CambiarFondo2", GameManager.instance.GetSegs() - 1);
            }
        }
        if (GameManager.instance.GetFondoTiempo())      //  Anula los efectos visuales cuando se acaba la habilidad.
        {
            CambiarFondo2();
            GameManager.instance.SetFondoTiempo(false);
        }
    }

    public void CambiarFondo()      //  Trae el fondo del tiempo adelante.
    {
        TelemetryManager.Instance().AddEvent(new FreezeStartEvent((int)transform.position.x, (int)transform.position.y));
        sprite.sortingOrder = 0;
    }

    public void CambiarFondo2()     //  Lleva el fondo del tiempo atrás.
    {
        TelemetryManager.Instance().AddEvent(new FreezeEndEvent((int)transform.position.x, (int)transform.position.y));
        sprite.sortingOrder = -10;
        CancelInvoke();
    }  
}
