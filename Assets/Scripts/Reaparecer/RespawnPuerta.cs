using UnityEngine;

/* Script para que las puertas vuelvan a su posición inicial.
 * Este es llamado por el script ReaparecePlatPuerta.
 */
public class RespawnPuerta : MonoBehaviour
{
    Vector2 posIni;
    void Start()
    {
        posIni = new Vector2(transform.position.x, transform.position.y);
    }

    public void Reaparece()
    {
        transform.position = posIni;
    }
}
