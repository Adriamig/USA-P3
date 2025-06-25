using UnityEngine;

/* Script de la muerte de los enemigos
 * Este script ira en el GameObject de la cabeza donde es hijo del enemigo
 */
public class MuerteEne : MonoBehaviour
{
    private Object DeathParticleRef;

    
    private void Start()   //  Definimos todas las variables que utilizaremos en este script
    {
        DeathParticleRef = Resources.Load("DeathParticle");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si colisionamos contra el jugador
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            this.transform.parent.gameObject.SetActive(false);                    // Ponemos que el padre se desactive
            GameObject DeathParticle = (GameObject)Instantiate(DeathParticleRef); //Instanciamos la particula de muerte del enemigo
            DeathParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Destroy(DeathParticle, 0.1f);
        }
        
    }    
}
