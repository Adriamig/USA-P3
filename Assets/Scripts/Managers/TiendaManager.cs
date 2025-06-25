using UnityEngine;
using UnityEngine.UI;

/* Script que controla la implementación
 * de la tienda dentro del juego.
 */

public class TiendaManager : MonoBehaviour
{
    public static TiendaManager instance;

    [SerializeField] private int mejoraG = 0;
    [SerializeField] private int mejoraT = 0;
    [SerializeField] private int precio = 15;
    [SerializeField] private Text mejoraGrav;
    [SerializeField] private Text mejoraTiempo;
    [SerializeField] private Image[] capsulasLlenasG;
    [SerializeField] private Image[] capsulasLlenasT;

    private Canvas tiendaUI;

    void Awake()                                             //  Comprobar que solo hay un TiendaManager.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);
    }
    private void Start()
    {
        tiendaUI = GetComponent<Canvas>();
        tiendaUI.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.GetTiendaFisica()) // La tienda aparece si el jugador
        {                                                                          // está en contacto y pulsa "E".
            if (tiendaUI.enabled)
                tiendaUI.enabled = false;
            else
                tiendaUI.enabled = true;
        }
        else if (!GameManager.instance.GetTiendaFisica() && tiendaUI.enabled)
            tiendaUI.enabled = false;
    }
   
    public void TiendaGravedad()        //  Implementa la mejora de la gravedad.
    {
        if (GameManager.instance.GetMonedas() >= precio && mejoraG != 3)
        {
            mejoraG += 1;
            GameManager.instance.AddMonedas(-precio);
            CompraG();
        }               
        if (mejoraG == 3)
        {
            GameManager.instance.ActualizaTienda();
            GameManager.instance.SetCapsulasRest(8);        //poner la cte
            GameManager.instance.SetTiendaG(true);            
        }       
    }

    public void TiendaTiempo()          // Implementa la mejora del tiempo.
    {
        if (GameManager.instance.GetMonedas() >= precio && mejoraT != 3)
        {
            mejoraT += 1;
            GameManager.instance.AddMonedas(-precio);
            CompraT();
        }
        if (mejoraT == 3)
        {
            GameManager.instance.SetSegs(7);          //poner la cte
            GameManager.instance.SetTiendaT(true);
            GameManager.instance.UpdateTiempo();
        }
    }

    private void CompraG()      // Permite comprar la mejora de la gravedad.
    {
        int n = mejoraG;
        capsulasLlenasG[n - 1].enabled = true;
        mejoraGrav.text = n.ToString() + "/3";                
    }

    private void CompraT()      // Permite comprar la mejora del tiempo.
    {       
        int n = mejoraT;
        capsulasLlenasT[n - 1].enabled = true;
        mejoraTiempo.text = n.ToString() + "/3";
    }
}
