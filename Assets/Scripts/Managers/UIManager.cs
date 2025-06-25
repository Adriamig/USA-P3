using UnityEngine;
using UnityEngine.UI;

/* Script que controla las mejoras de
 * las habilidades y su interfaz.
 */

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Image[] capsulasLlenas;
    [SerializeField] private Image[] partesIngrdientes;
    [SerializeField] private Image barraTiempo;

    void Start()
    {
        GameManager.instance.SetUIManager(this);
        UpdateMonedas(GameManager.instance.GetMonedas());
        for (int i = 6; i<= 9; i++) capsulasLlenas[i].enabled = false;    // Funciona para las cápsulas vacías
    }

    public void UpdateMonedas(int monedas) // Actualizar total de monedas.
    {
        coinsText.text = monedas.ToString();
    }

    public void UpdateGravedad(int capsulasG)  // Actualiza el número de cápsulas de la gravedad.
    {
        int c = capsulasG - 1;

        if (capsulasG < GameManager.instance.GetCapsulasG())
            capsulasLlenas[capsulasG].enabled = false;
        else if (capsulasG == GameManager.instance.GetCapsulasG())
        {
            for (int i = 0; i <= c; i++)
                capsulasLlenas[c - i].enabled = true;
        }
    }

    public void TiendaGravedad()
    {
        for (int i = 0; i <= 9; i++)        // Hacer 10 vueltas por las dos nuevas que están vacías.
            capsulasLlenas[i].enabled = true;
    }

    public void UpdateTiempo(int seg, bool tiendaT)  // Actualiza los segundos de la habilidad del tiempo.
    {
        if (tiendaT)
        {
            if (barraTiempo != null)
                barraTiempo.fillAmount = seg * 0.14f;
            Debug.Log(barraTiempo.fillAmount);
        }

        else
        {
            if(barraTiempo != null)
                barraTiempo.fillAmount = seg * 0.2f;    // 1/seg
            Debug.Log(barraTiempo.fillAmount);
        }            
    }

    public void RellenaBarraTiempo()        //  Rellena la barra del tiempo.
    {
        barraTiempo.fillAmount = 1;
    }

    public void UpdateIngredientes(int numero)      // Actualiza el número de ingredientes.
    {
        partesIngrdientes[numero].enabled = false;        
    }
}
