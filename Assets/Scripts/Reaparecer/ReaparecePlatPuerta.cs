using UnityEngine;

/* Script para ser llamado desde el script SpawnerPlataforma.
 * Sirve para activar el script RespawnPuerta cuando es llamado.
 */
public class ReaparecePlatPuerta : MonoBehaviour
{
    // Start is called before the first frame update
    RespawnPuerta componentes;
    Sprite normal;
    private void Start()
    {
        normal = GetComponent<SpriteRenderer>().sprite;
        componentes = transform.GetChild(0).GetComponent<RespawnPuerta>();
    }
    public void Reaparece()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = normal;
        componentes.Reaparece();
    }
}
