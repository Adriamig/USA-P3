using UnityEngine;

/* Script que implementa el 
 * menú de pausa dentro del juego.
 */

public class MenuPausa : MonoBehaviour
{
    private Canvas MenuPausaUI;

    private void Start()
    {
        MenuPausaUI = GetComponent<Canvas>();
        MenuPausaUI.enabled = false;
    }
        
    void Update()       // Abre el menú si se pulsa Escape.
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.GetEscalera())
        {
            if (MenuPausaUI.enabled)
            {
                GameManager.instance.SetMenuPausa(true);
                Reanudar();
            }
            else    // Si se vuelve a pulsar, lo cierra.
            {
                GameManager.instance.SetMenuPausa(false);
                Pausa();
            }         
        }
    }

    public void Reanudar()  // Para reanudar el juego cuando se cierra el menú.
    {
        MenuPausaUI.enabled = false;
        GameManager.instance.SetMenuPausa(false);
        Time.timeScale = 1.0f;     
    }

    private void Pausa()    // Para parar el juego mientras el menú está en pantalla.
    {      
        MenuPausaUI.enabled = true;
        GameManager.instance.SetMenuPausa(true);
        Time.timeScale = 0f;       
    }
}
