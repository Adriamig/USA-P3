using UnityEngine;

/* Script para encargarse de los procesos de reaparicionel jugador y de los enemigos
 * Irá asociado al prefab PlayerController
 */
public class CheckpointManager : MonoBehaviour
{
    private Transform ultCheckpoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !GameManager.instance.GetTiendaFisica() && !GameManager.instance.GetMenuPausa())
            Reaparecer();
    }

    public void Pasapor(Transform checkpoint) // Actualiza la posicion del ultimo checkpoint
    {
        ultCheckpoint = checkpoint;
    }

    public void Reaparecer() // Metodo para aparecer desde el ultimo checkpoint con los datos que se tenia
    {
        if (GameManager.instance.GetGravedad())
        {
            GameManager.instance.SetCapsulasRest(GameManager.instance.GetCapsulasG() + 1); // Poner una cápsula de más porque con la sig línea se resta una
            GameManager.instance.SetGravedad(false);
        }
        else
            GameManager.instance.SetCapsulasRest(GameManager.instance.GetCapsulasG());

        GameManager.instance.MuerteTiempo();
        GameManager.instance.SetFondoTiempo(true);
        GameManager.instance.GetSegs();
        GameManager.instance.SetReapareceEnemigo(true);
        GameManager.instance.SetReaparecePuerta(true);
        transform.position = ultCheckpoint.position;
    }

    public void ReinicioTotal() // Metodo que puede ser llamado cuando el jugador quiere reiniciar
    {
        GameManager.instance.SetCapsulasRest(GameManager.instance.GetCapsulasG() + 1); //Poner una cápsula de más poruq econ la sig línea se resta una
        GameManager.instance.SetGravedad(false);
        GameManager.instance.GetSegs();
        GameManager.instance.ReiniciaMonedas();
        GameManager.instance.SetReapareceEnemigo(true);
        GameManager.instance.SetReaparecePuerta(true);
        GameManager.instance.AnulaMejoras();
        GameManager.instance.MuerteTiempo();
        GameManager.instance.SetFondoTiempo(true);
        GameManager.instance.SetIngredientes(0);
    }
}
