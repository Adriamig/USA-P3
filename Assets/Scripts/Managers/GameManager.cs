using System.Text.RegularExpressions;
using TelemetriaTL;
using TelemetriaTL.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

/* El único GameManager del juego.
 */

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    UIManager theUIManager;
    SoundManager soundManager;

    const int SEGS = 5;
    const int SEGSMEJORADO = 7;
    const int CAPS = 6;
    const int CAPSMEJORADO = 8;

    public int partesIngrediente = 0;
    public int segs = 5;
    public int monedas = 0;
    public int capsulasG = 6;

    bool fondo = false;
    bool reaparecePuerta = false;
    public bool gravedad = false;
    private bool tiempo = false;
    private bool escalera = false;
    private bool reapareceEne = false;
    private bool menuPausa = false;
    public bool tiendaG = false;
    public bool tiendaT = false;
    public bool tiendaFisica = false;

    private bool suelo;
    private bool paredL;
    private bool paredR;

    // Telemetria
    private LevelWin win;

    [SerializeField] private string _fileName;

    void Awake()                                             //  Comprobar que solo hay un GameManager.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);
    }

    void Start()
    {
        win = LevelWin.Lose;
        TelemetryManager.Init("TimeLess", 5.0f, TelemetriaTL.SerializeType.JSON, TelemetriaTL.PersistanceType.File, _fileName);
    }

    void Update()
    {
        TelemetryManager.Instance().Update(Time.deltaTime);
    }

    void OnApplicationQuit()
    {
        Match matchActual = Regex.Match(SceneManager.GetActiveScene().name, @"^Nivel(\d+)$");
        if (matchActual.Success)
        {
            int nivelActual = int.Parse(matchActual.Groups[1].Value);
            TelemetryManager.Instance().EndLevel(nivelActual, win);
        }

        TelemetryManager.Release();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SetSuelo(bool isGrounded)                    //  Detecta el suelo. 
    {
        suelo = isGrounded;
    }

    public bool GetSuelo()                                   //  Devuelve un valor que indica
    {                                                        //  si hay suelo o no.
        return suelo;
    }

    public void SetParedL(bool paredL_)                      //  Detecta pared por la izquierda.
    {
        paredL = paredL_;
    }

    public bool GetParedL()                                  //  Devuelve un valor que indica
    {                                                        //  si hay pared por la izquierda o no.
        return paredL;
    }

    public void SetParedR(bool paredR_)                      //  Detecta pared por la derecha.
    {
        paredR = paredR_;
    }

    public bool GetParedR()                                  //  Devuelve un valor que indica
    {                                                        //  si hay pared por la derecha o no.
        return paredR;
    }

    public void AddMonedas(int n)                            //  Añade monedas a la cantidad actual.
    {
        monedas += n;
        if (theUIManager != null)
            theUIManager.UpdateMonedas(monedas);
        Debug.Log(monedas);
    }

    public void ReiniciaMonedas()                            //  Pone la cantidad de monedas a 0.
    {
        monedas = 0;
        if (theUIManager != null)
            theUIManager.UpdateMonedas(monedas);
    }

    public int GetMonedas()                                  //  Devuelve la cantidad de monedas.
    {
        return monedas;
    }

    public int GetSegs()                                     //  Se encarga de aplicar la mejora del tiempo.
    {
        if (!tiendaT)
            return SEGS;

        else
            return SEGSMEJORADO;
    }
    public void SetSegs(int s)                               //  Se encarga de aplicar la mejora del tiempo.
    {
        segs = s;
    }

    public bool CambioTiempo()
    {
        if (segs == GetSegs())
        {
            soundManager.audioTiempo();
            tiempo = !tiempo;                               //  Invierte tiempo y lo vuelve a invertir después de 5 segundos                       
            InvokeRepeating("Crono", 0f, 1f);
            return true;
        }
        else return false;
    }
    public void SetFondoTiempo(bool _fondo)                 //  Se encarga de los efectos visuales del tiempo.
    {
        fondo = _fondo;
    }
    public bool GetFondoTiempo()                            //  Se encarga de los efectos visuales del tiempo.
    {
        return fondo;
    }
    public void MuerteTiempo()                              //  Reinicia la barra del tiempo una vez muerto.
    {
        CancelInvoke();
        tiempo = false;
        segs = GetSegs();
        theUIManager.RellenaBarraTiempo();
        soundManager.ResetNivel();

    }
    public void Crono()                                     //  Sirve como contador.
    {
        segs -= 1;
        theUIManager.UpdateTiempo(segs, tiendaT);
        if (segs == 0)
        {
            tiempo = false;
            CancelInvoke();
            InvokeRepeating("RecuperarTiempo", 1f, 1.5f);
        }
    }

    public void RecuperarTiempo()                           //  Revisa si el tiempo está mejorado o no.
    {
        if (segs != GetSegs())
        {
            segs += 1;
            theUIManager.UpdateTiempo(segs, tiendaT);
        }
        if (segs == GetSegs())
            CancelInvoke();
    }

    public bool Tiempo()                                    //  Devuelve un valor que indica 
    {                                                       //  si el tiempo está detenido o no.
        return tiempo;
    }

    public void SetGravedad(bool active)                    //  Método que recoge booleano del script Gravedad para saber el estado.
    {
        gravedad = active;
        SetCapsulasRest(capsulasG - 1);
        theUIManager.UpdateGravedad(capsulasG);
        Debug.Log("CAPSULAS RESTANTES: " + capsulasG);
    }

    public bool GetGravedad()                               //  Método que devuelve el booleano pedido en el método de SetGravedad.
    {
        return gravedad;
    }

    public void SetCapsulasRest(int capsG)                  //  Da un valor a la cantidad de cápsulas. 
    {
        capsulasG = capsG;
        theUIManager.UpdateGravedad(capsulasG);
    }

    public int GetCapsulasRest()                            //  Devuelve el valor de las cápsulas.
    {
        return capsulasG;
    }

    public void SetEscalera(bool escalera1)                 //  Genera un booleano encargado de
    {                                                       //  detectar las escaleras.
        escalera = escalera1;
    }

    public bool GetEscalera()                               //  Devuelve un valor que indica 
    {                                                       //  si hay escalera o no.
        return escalera;
    }

    public bool GetReaparecePuerta()                        //  Devuelve el booleano encargado de
    {                                                       //  detectar las puertas.
        return reaparecePuerta;
    }

    public void SetReaparecePuerta(bool _reaparecePuerta)   //  Genera un booleano encargado de 
    {                                                       //  detectar las puertas.
        reaparecePuerta = _reaparecePuerta;
    }

    public void ChangeScene(string sceneName) // Cambia de la escena actual a otra.
    {
        if (!string.IsNullOrEmpty(sceneName)) // Para el botón reanudar.
        {
            Match matchActual = Regex.Match(SceneManager.GetActiveScene().name, @"^Nivel(\d+)$");
            if (matchActual.Success)
            {
                int nivelActual = int.Parse(matchActual.Groups[1].Value);
                TelemetryManager.Instance().EndLevel(nivelActual, win);
            }

            Match matchNueva = Regex.Match(sceneName, @"^Nivel(\d+)$");
            if (matchNueva.Success)
            {
                int nivelNuevo = int.Parse(matchNueva.Groups[1].Value);
                TelemetryManager.Instance().StartLevel(nivelNuevo);

            }

            win = LevelWin.Lose;

            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

            tiempo = false;
            gravedad = false;
            escalera = false;
        }

        Time.timeScale = 1;
    }

    public void RecogeIngrediente(int nIngr)                //  Añade 1 a la cantidad de 
    {                                                       //  ingredientes actuales.
        partesIngrediente++;
        theUIManager.UpdateIngredientes(nIngr);
    }

    public void SetIngredientes(int n)                      //  Da un valor a la cantidad de ingredientes.
    {
        partesIngrediente = n;
    }
    public bool ActivaPortal()                              //  Activa el portal al siguiente 
    {                                                       //  nivel en caso de que haya 4 ingredientes.
        return (partesIngrediente == 4);
    }

    public void Levelfinished()                             //  Se encarga de cambiar las escenas.
    {
        if (partesIngrediente == 4 && SceneManager.GetActiveScene().name == "Nivel1")
        {
            win = LevelWin.Win;
            ChangeScene("Nivel2");
        }
        else if (partesIngrediente == 4 && SceneManager.GetActiveScene().name == "Nivel2")
        {
            win = LevelWin.Win;
            ChangeScene("Nivel3");
        }
        else if (partesIngrediente == 4 && SceneManager.GetActiveScene().name == "Nivel3")
        {
            win = LevelWin.Win;
            ChangeScene("Nivel4");
        }
        else if (partesIngrediente == 4 && SceneManager.GetActiveScene().name == "Nivel4")
        {
            win = LevelWin.Win;
            ChangeScene("FinDemo");
        }

        partesIngrediente = 0;
    }
    public void SetUIManager(UIManager uim)                //  Comprobar solo un UI y actualizarlo.
    {
        theUIManager = uim;
        theUIManager.UpdateMonedas(monedas);
        theUIManager.UpdateGravedad(capsulasG);
        theUIManager.UpdateTiempo(GetSegs(), tiendaT);
    }

    public void SetSoundManager(SoundManager sm)           //  Crea un valor usado para el SoundManager.
    {
        soundManager = sm;
    }

    public void SetReapareceEnemigo(bool _reapareceEne)   //  Genera un booleano encargado de
    {                                                     //  la reaparición de los enemigos.
        reapareceEne = _reapareceEne;
    }

    public bool GetReapareceEnemigo()                     //  Devuelve el booleano encargado de
    {                                                     //  la reaparición de los enemigos.
        return reapareceEne;
    }
    public int GetCapsulasG()                             //  Se encarga de aplicar la mejora
    {                                                     //  a las cápsulas de gravedad.
        if (!tiendaG)
            return CAPS;
        else
            return CAPSMEJORADO;
    }
    public void SetTiendaFisica(bool tiendaF)             //  Genera un booleano para la tienda.
    {
        tiendaFisica = tiendaF;
    }

    public bool GetTiendaFisica()                         //  Devuelve el booleano de la tienda.
    {
        return tiendaFisica;
    }

    public void ActualizaTienda()                         //  Actualiza la tienda de acorde a
    {                                                     //  si se ha comprado o no.
        theUIManager.TiendaGravedad();
    }

    public void UpdateTiempo()                            //  Aplica la mejora al tiempo.
    {
        theUIManager.UpdateTiempo(segs, tiendaT);
    }

    public void SetTiendaG(bool tiendaG_)                 //  Tiene en cuenta las mejoras de la gravedad.
    {
        tiendaG = tiendaG_;
    }

    public void SetTiendaT(bool tiendaT_)                 //  Tiene en cuenta las mejoras del tiempo.
    {
        tiendaT = tiendaT_;
    }

    public void SetMenuPausa(bool pausa)
    {
        menuPausa = pausa;
    }

    public bool GetMenuPausa()
    {
        return menuPausa;
    }

    public void AnulaMejoras()                            //  Anula todas las mejoras y pone
    {                                                     //  la tienda a como estaba antes de comprar.
        tiendaG = false;
        tiendaT = false;
        segs = SEGS;
    }
}                                                                                                                                                                                                           