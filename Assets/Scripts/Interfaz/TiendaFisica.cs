using UnityEngine;

/* Permite el uso de la tienda
 * dentro del juego.
 */

public class TiendaFisica : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)         // La tienda aparece sólo si el jugador
    {                                                       // está en contacto con el sprite de la tienda
        if (other.GetComponent<PlayerController>() != null) // y presiona "E".
        {   
            GameManager.instance.SetTiendaFisica(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.SetTiendaFisica(false);
        }
    }
}
