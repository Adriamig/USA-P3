using UnityEngine;

/* Script que llama al script ReaparecePlatPuerta para activar cuando el GameManager lo indique
 * Irá asociado al prefab SpawnerPlataformas donde sus hijos seran prefabs con el componente dicho
 */
public class SpawnerPlataforma : MonoBehaviour
{
    ReaparecePlatPuerta[] componentes;
    private void Start()
    {
        componentes = transform.GetComponentsInChildren<ReaparecePlatPuerta>();
    }

    void Update()
    {
        if (GameManager.instance.GetReaparecePuerta())
        {
            foreach (ReaparecePlatPuerta comp in componentes)
            {
                comp.Reaparece();
            }

            GameManager.instance.SetReaparecePuerta(false);
        }
    }
}
