using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("ITEMS VIDA")]
    public GameObject[] itemsVida;

    [Header("ITEMS ARCADE")]
    public GameObject[] itemsArcade;

    [Range(0f, 1f)]
    public float probabilidadItem = 0.15f;

    [Header("RESIDUOS")]
    public GameObject[] residuos;

    [Header("CONFIGURACIÓN")]
    public float tiempoSpawn = 2f;

    [Header("POSICIÓN")]
    public Vector3 posicionSpawn =
        new Vector3(-9.40f, 0f, 0f);

    public float variacionY = 0.5f;

    [Header("CONTENEDOR")]
    public Transform contenedorResiduos;

    [Header("ESCALA GLOBAL")]
    public float escalaGlobal = 1.3f;
    // =====================================
    // BOLSA ALEATORIA
    // =====================================

    private List<GameObject> bolsaResiduos =
        new List<GameObject>();

    private bool pausado = false;

    void Start()
    {
        PrepararBolsa();

        InvokeRepeating(
            nameof(GenerarObjeto),
            1f,
            tiempoSpawn
        );
    }

    // =====================================
    // PREPARAR BOLSA
    // =====================================

    void PrepararBolsa()
    {
        bolsaResiduos.Clear();

        foreach (GameObject r in residuos)
        {
            if (r != null)
            {
                bolsaResiduos.Add(r);
            }
        }

        MezclarBolsa();
    }

    void MezclarBolsa()
    {
        for (int i = 0; i < bolsaResiduos.Count; i++)
        {
            int random =
                Random.Range(i, bolsaResiduos.Count);

            GameObject temp =
                bolsaResiduos[i];

            bolsaResiduos[i] =
                bolsaResiduos[random];

            bolsaResiduos[random] =
                temp;
        }
    }

    // =====================================
    // GENERAR OBJETO
    // =====================================

    void GenerarObjeto()
    {
        if (pausado) return;

        Vector3 spawnPos =
            posicionSpawn +
            new Vector3(
                0,
                Random.Range(-variacionY, variacionY),
                0
            );

        bool generarItem =
            Random.value < probabilidadItem;

        GameMode modo =
            GameManager.instancia.ObtenerModoActual();

        // =====================================
        // GENERAR ITEMS
        // =====================================

        if (generarItem)
        {
            // =========================
            // ARCADE MODE
            // =========================

            if (modo is ArcadeMode)
            {
                if (itemsArcade.Length > 0)
                {
                    int randomItem =
                        Random.Range(
                            0,
                            itemsArcade.Length
                        );

                    GameObject item =
                        itemsArcade[randomItem];

                    if (item != null)
                    {
                        GameObject nuevoItem =
                            Instantiate(
                                item,
                                spawnPos,
                                Quaternion.identity,
                                contenedorResiduos
                            );

                        // 🔥 ESCALA GLOBAL
                        nuevoItem.transform.localScale *= escalaGlobal;

                        Debug.Log(
                            "🔥 Item Arcade: "
                            + item.name
                        );

                        return;
                    }
                }
            }

            // =========================
            // OTROS MODOS
            // =========================

            else
            {
                if (itemsVida.Length > 0)
                {
                    int randomItem =
                        Random.Range(
                            0,
                            itemsVida.Length
                        );

                    GameObject item =
                        itemsVida[randomItem];

                    if (item != null)
                    {
                        GameObject nuevoItem =
                            Instantiate(
                                item,
                                spawnPos,
                                Quaternion.identity,
                                contenedorResiduos
                            );

                        // 🔥 ESCALA GLOBAL
                        nuevoItem.transform.localScale *= escalaGlobal;

                        Debug.Log(
                            "❤️ Item Vida: "
                            + item.name
                        );

                        return;
                    }
                }
            }
        }

        // =====================================
        // GENERAR RESIDUOS
        // =====================================

        if (bolsaResiduos.Count == 0)
        {
            PrepararBolsa();
        }

        GameObject residuo =
            bolsaResiduos[0];

        bolsaResiduos.RemoveAt(0);

        if (residuo == null)
        {
            Debug.LogWarning(
                "⚠️ Residuo vacío"
            );

            return;
        }

        GameObject nuevoResiduo =
            Instantiate(
                residuo,
                spawnPos,
                Quaternion.identity,
                contenedorResiduos
            );

        // 🔥 ESCALA GLOBAL
        nuevoResiduo.transform.localScale *= escalaGlobal;

        Debug.Log(
            "🗑️ Spawn: "
            + residuo.name
        );
    }

    // =====================================
    // PAUSA
    // =====================================

    public void PausarSpawn()
    {
        pausado = true;

        CancelInvoke(nameof(GenerarObjeto));
    }

    public void ReanudarSpawn()
    {
        pausado = false;

        CancelInvoke(nameof(GenerarObjeto));

        InvokeRepeating(
            nameof(GenerarObjeto),
            tiempoSpawn,
            tiempoSpawn
        );
    }

    // =====================================
    // CAMBIAR TIEMPO SPAWN
    // =====================================

    public void CambiarTiempoSpawn(float nuevoTiempo)
    {
        tiempoSpawn = nuevoTiempo;

        if (!pausado)
        {
            CancelInvoke(nameof(GenerarObjeto));

            InvokeRepeating(
                nameof(GenerarObjeto),
                tiempoSpawn,
                tiempoSpawn
            );
        }
    }
}