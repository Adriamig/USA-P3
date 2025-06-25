using UnityEngine;

/* Script para cambiar la zona que el jugador ve.
 * Estara asociado en un gameObject vacio con colisionador hijo de la cámara.
 * Sirve solo para llamar al intercambio de la gravedad de los enemigos al que la afecta.
 */
public class GravedadCamara : MonoBehaviour
{
    private Collider2D miCollider;

    private void Start()
    {
        miCollider = GetComponent<Collider2D>();
        miCollider.enabled = true;
    }

    private void Update()
    {
        if(GameManager.instance.GetGravedad() == false)     //  Activa el collider del radio de visión
        miCollider.enabled = true;                          //  hasta que la gravedad se utilice.
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GravedadEne gravedadEne = other.gameObject.GetComponent<GravedadEne>();

        if (gravedadEne && GameManager.instance.GetGravedad() == true)      //  Cambia la gravedad de aquellos GameObjects
        {                                                                   //  que estén en el radio de visión del jugador.
            gravedadEne.CambiarGravedad();
            Invoke("DesactivarCollider", 0.2f);
        }
    }

    private void DesactivarCollider()        //  Desactiva el collider del radio de visión.
    {
        miCollider.enabled = false;
    }
}
