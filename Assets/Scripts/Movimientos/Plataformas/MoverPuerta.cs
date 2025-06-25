using UnityEngine;

/* Script que permite abrir una puerta o 
 * barrera al pasar por encima de una flor.
 */

public class MoverPuerta : MonoBehaviour
{
    [SerializeField] private Sprite florRoja1;
    [SerializeField] private float distancia, velocidad;

    private bool inicio = false;
    private Vector2 posIni;
    private Rigidbody2D rb;
    private GameObject child;

    private void OnTriggerEnter2D(Collider2D Player)            //  Cuando el jugador entra en contacto 
    {                                                           //  con la flor, se cambia su sprite y se
                                                                //  inicia el movimiento de la plataforma.
        if (Player.GetComponent<PlayerController>() != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = florRoja1;
            inicio = true;            
        }
    }

    void Start()    //  Declara las variables que se usaran para acceder a la plataforma.
    {
        child = this.transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        posIni = new Vector2(child.transform.position.x, child.transform.position.y);
    }

    void Update()
    {
        if (inicio)     //  Se encarga de realizar el movimiento de la plataforma
                        //  una vez el jugador haya pasado por la flor.
        {
            if (child.transform.position.y < posIni.y + distancia)            
                rb.velocity = new Vector2(0, velocidad);
            
            if (child.transform.position.y > posIni.y + distancia)            
                inicio = false;
                        
            if (inicio == false)            
                rb.velocity = new Vector2(0, 0);            
        }
    }
}
