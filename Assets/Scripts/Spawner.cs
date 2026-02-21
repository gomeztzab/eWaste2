using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] residuos;
    public float tiempoSpawn = 2f;

    void Start()
    {
        InvokeRepeating("GenerarResiduo", 1f, tiempoSpawn);
    }

    void GenerarResiduo()
    {
        int random = Random.Range(0, residuos.Length);

        Vector3 spawnPos = new Vector3(-9.40f, 0f, 0f);
        // cambia 0f por la Y que tú quieras

        Instantiate(residuos[random], spawnPos, Quaternion.identity);
    }
}
