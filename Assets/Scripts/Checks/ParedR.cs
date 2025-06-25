using UnityEngine;

/* Script para decir al gamemanager si el jugador se encuentra en pegado a la pared derecha
 * Este script ira en un GameObject donde solo puede colisionar con la pared derecha
 */
public class ParedR : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CompositeCollider2D>() != null)
            GameManager.instance.SetParedR(true);        // LLamamos al GameManager poniendo que esta tocando la pared derecha
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CompositeCollider2D>() != null)
            GameManager.instance.SetParedR(false);        // LLamamos al GameManager poniendo que no esta tocando la pared derecha
    }
}
