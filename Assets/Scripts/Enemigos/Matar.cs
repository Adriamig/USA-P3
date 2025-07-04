﻿using UnityEngine;
using TelemetriaTL;
using TelemetriaTL.Events;

/* Script para Matar al jugador
 * Ira asociado a todos los prefabs de los enemigos para que el
   jugador pueda ser matado al colisionar con estos
 */
public class Matar : MonoBehaviour
{
    // Cuando colisiona
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Si el otro gameObject tiene el componente PlayerController (el jugador)
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            DeathType type = GetComponent<ReapareceEne>() != null ? DeathType.Enemy : DeathType.Hazard;
            TelemetryManager.Instance().AddEvent(new PlayerDeathEvent((int)transform.position.x, (int)transform.position.y, type));

            other.gameObject.GetComponent<CheckpointManager>().Reaparecer(); // LLama al GameManager para instanciarlo donde el checkpoint
            Debug.Log("HAS MUERTO");                                         // Envia un mensaje a consola

        }
        else if (other.gameObject.GetComponent<MoverEne>() || other.gameObject.GetComponent<SaltoRana>()) // Si choca contra otro enemigo
            other.gameObject.SetActive(false);                  // Se desactiva el GameObject
    }
}
