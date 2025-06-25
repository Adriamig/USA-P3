using UnityEngine;
using UnityEngine.SceneManagement;

/* Script que implementa el menú
 * principal al principio del juego.
 */

public class MainMenu : MonoBehaviour
{   
   public void PlayGame()  // Para cargar el nivel.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
    
    public void QuitGame() // Para salir.
    {
        Debug.Log("Saliendo del Juego!!!");
        Application.Quit();
    }
    public void Jugar(string nivel)   // Para jugar.
    {
        SceneManager.LoadScene(nivel);
    }
}
