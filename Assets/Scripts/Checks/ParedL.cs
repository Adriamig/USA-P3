using UnityEngine;

/* Script para decir al gamemanager si el jugador se encuentra en pegado a la pared izquierda
 * Este script ira en un GameObject donde solo puede colisionar con la pared izquierda
 */
public class ParedL : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CompositeCollider2D>() != null)        
            GameManager.instance.SetParedL(true);        // LLamamos al GameManager poniendo que esta tocando la pared izquierda
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CompositeCollider2D>() != null)        
            GameManager.instance.SetParedL(false);        // LLamamos al GameManager poniendo que no esta tocando la pared izquierda
    }
}
