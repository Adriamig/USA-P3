using UnityEngine;

/* El SoundManager de este juego.
 */

public class SoundManager : MonoBehaviour
{

    public bool aNivel1, aTiempo;       //  Booleanos que sirven para controlar el audio que suena cuando paras el tiempo.
    public AudioSource audNivel1, audTiempo, audGravedad, audLobo, audLoboM, audRana, audRanaM, audAve, audAveM, audMenu, audMoneda;   //  Lista de los audios 
    public GameObject Rana, Lobo, Ave;  //  GameObjects que emiten algunos de los sonidos.


    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetSoundManager(this); //  Comprobar que solo hay un SoundManager.
            audioNivel();                               //  Iniciar la música del nivel 1.
        }

        //  Convierto los audioSources en hijos desde el script para tener ordenados los audios en la jerarquía.
        if (Rana != null)
            audRana.transform.parent = Rana.transform;  //  Convierte el audioSource de la rana en hijo del GameObject Rana.
        if (Lobo != null)
            audLobo.transform.parent = Lobo.transform;  //  Convierte el audioSource del lobo en hijo del GameObject Lobo.
        if (Ave != null)
            audAve.transform.parent = Ave.transform;    //  Convierte el audioSource del ave en hijo del GameObject Ave.
    }

    public void deadRana()          //  Método que detiene el croak de la rana  
    {                               //  para reproducir un efecto sonoro de su muerte.
        audRana.Stop();
        audRanaM.Play();
    }

    public void deadLobo()          //  Método que detiene el rugido del lobo
    {                               //  para reproducir un efecto sonoro de su muerte.                        
        audLobo.Stop();
        audLoboM.Play();
    }

    public void deadAve()           //  Método que detiene el aleteo del ave
    {                               //  para reproducir un efecto sonoro de su muerte.
        audAve.Stop();
        audAveM.Play();
    }


    public void audioNivel()        //  Método, junto a audioTiempo(), que sirve para
    {                               //  reproducir de manera correcta la música de cuando
        aNivel1 = true;             //  se para el tiempo.
        aTiempo = false;
        audNivel1.Play();
    }

    public void audioTiempo()
    {
        if (audNivel1.isPlaying)
            aNivel1 = false;
        {
            audNivel1.Stop();
        }
        if (!audTiempo.isPlaying && aTiempo == false)
        {
            audTiempo.Play();
            aTiempo = true;
        }
        Invoke("audioNivel", GameManager.instance.GetSegs());

    }

    public void ResetNivel()    //  Vuelve a activar el audio del nivel 1.
    {
        CancelInvoke();
        audTiempo.Stop();
        audioNivel();

    }
    public void audioMenu()     //  Reproduce los sonidos del menú.
    {
        audMenu.Play();
    }
    public void audioMoneda()   //  Reproduce los sonidos de la moneda.
    {
        audMoneda.Play();
    }

}

