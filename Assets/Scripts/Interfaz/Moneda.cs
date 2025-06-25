using UnityEngine;

/* Script que se coloca a las monedas 
 * para que puedan ser recogidas.
 */

public class Moneda : MonoBehaviour
{    
    public SoundManager sonido;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {            
            GameManager.instance.AddMonedas(1);
            
            Destroy(this.gameObject);
            sonido.audMoneda.Play();

        }
    }
}
