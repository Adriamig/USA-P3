using UnityEngine;

/* Script para que los enemigos reaparezcan cuando el script Spawner lo llame.
 * Irá asociado a todos los prefabs de los enemigos.
 */
public class ReapareceEne : MonoBehaviour
{
    Vector2 posIni;
    private Rigidbody2D rb;
    private float gravedadIni;

    void Start()
    {
        posIni = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        gravedadIni = rb.gravityScale;
    }

    public void Reaparece()
    {
        transform.position = posIni;
        if (rb.isKinematic != true)            //Si no es plataformas
            rb.gravityScale = gravedadIni;
    }

}
