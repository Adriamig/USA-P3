using UnityEngine;

/* Script para avisar al Animator que pare el movimiento de las animaciones
 * Irá asociado a todo GameObject que tenga animación
 */
public class PararAnimacion : MonoBehaviour
{    
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()       //  Detiene las animaciones dependiendo
                                //  de si se ha parado el tiempo o no.
    {
        if (GameManager.instance.Tiempo())        
            anim.SetBool("Parado", true);
        
        else
            anim.SetBool("Parado", false);
    }
}
