using UnityEngine;
using System;
using System.Linq;

public class Contenedor : MonoBehaviour
{
    [Header("CONFIGURACIÓN")]
    public TipoResiduo[] tiposAceptados;

    public static event Action<bool> OnValidacion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Residuo r = other.GetComponent<Residuo>();

        if (r == null) return;

        // 🔥 Obtener modo directamente del GameManager
        GameMode modo = GameManager.instancia.ObtenerModoActual();

        bool esValido = tiposAceptados.Contains(r.tipo);

        if (esValido)
        {
            int puntos = r.GetValorPuntos();

            // BONUS VELOCIDAD
            if (r.velocidad > 3.5f)
            {
                puntos += 5;
                Debug.Log("⚡ Bonus velocidad!");
            }

            modo?.OnAcierto(puntos);

            OnValidacion?.Invoke(true);

            r.MarcarComoAceptado();

            Destroy(r.gameObject);
        }
        else
        {
            int penalizaciones = r.GetRiesgo() switch
            {
                NivelRiesgo.Bajo => 1,
                NivelRiesgo.Medio => 2,
                NivelRiesgo.Alto => 3,
                _ => 1
            };

            for (int i = 0; i < penalizaciones; i++)
            {
                modo?.OnError();
            }

            OnValidacion?.Invoke(false);

            Destroy(r.gameObject);
        }
    }
}