using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using TelemetriaTL;

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
        GameManager.instance.ChangeScene(nivel);
    }
}
