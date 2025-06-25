using UnityEngine;

/* Script que permite recoger los 
 * ingredientes que lo tengan asociado.
 */

public class ParteIngr : MonoBehaviour
{
    public int nIngr;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.RecogeIngrediente(nIngr);  // Recoge el ingrediente y
            Destroy(this.gameObject);                       // destruye su gameObject.
        }
    }
}
