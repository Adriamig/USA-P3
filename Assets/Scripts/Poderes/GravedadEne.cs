using UnityEngine;

/* Script que activa el poder de la gravedad en los enemigos.
 * Irá asociado a todos los enemigos que no sean voladores
   ya que estos no son afectados por la gravedad.
 * Invierte la gravedad de los enemigos.
 */
public class GravedadEne : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool miGravedad = false;

    void Start()    //  Declara una variable que recoge el componente Rigidbody2D
    {               //  del GameObject al que esté asociado el script.

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(GameManager.instance.GetGravedad() == false && miGravedad != GameManager.instance.GetGravedad())      //  Accede al rigidbody y cambia su gravedad.
        {
            rb.gravityScale *= -1;
            transform.Rotate(180.0f, 0.0f, 0.0f, Space.Self);
            miGravedad = !miGravedad;
        }
    }

    public void CambiarGravedad() // Método para ser llamado mas tarde cuando se encuentre en la zona que le afecte la gravedad
    {
        if(GameManager.instance.GetGravedad() != miGravedad && GameManager.instance.GetGravedad() == true)        //  Vuelve a cambiar su gravedad.
        {
            rb.gravityScale *= -1;
            transform.Rotate(180.0f, 0.0f, 0.0f, Space.Self);
            miGravedad = !miGravedad;
        }
    }
}
