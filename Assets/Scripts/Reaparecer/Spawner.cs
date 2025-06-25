using UnityEngine;

/* Script que se utiliza para que los enemigos y las vuelvan a su posicion inicial
 * Este llama al script ReapareceEne cuando el GameManager lo pida
 */
public class Spawner : MonoBehaviour
{
    ReapareceEne[] componentes;
    private void Start()
    {
        componentes = transform.GetComponentsInChildren<ReapareceEne>();
    }
    void Update()
    {
        if (GameManager.instance.GetReapareceEnemigo())
        {
            // Reiniciamos a los enemigos / plataformas
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(true);
            }

            foreach (ReapareceEne comp in componentes) comp.Reaparece();

            GameManager.instance.SetReapareceEnemigo(false);
        }
    }
}
