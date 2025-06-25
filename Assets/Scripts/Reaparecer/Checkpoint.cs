using UnityEngine;

/* Script para guardar el ultimo checkpoint por donde el jugador pasa.
 * Irá asociado al prefab Checkpoint.
 * Si este pasa llama al GameManager para actualizarlo.
   y activa la animación de este.
 */
public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    private bool pasado = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && !pasado)         // Si el jugador pasa por este
        {
            pasado = true;
            other.GetComponent<CheckpointManager>().Pasapor(this.transform);   // LLama al CheckpointManager para actualizar la posicion
            anim.SetBool("Check", true);
            GameManager.instance.SetCapsulasRest(GameManager.instance.GetCapsulasG()); // Llenamos las capsulas del jugador
        } // Solo puede activarse una vez
    }
}
