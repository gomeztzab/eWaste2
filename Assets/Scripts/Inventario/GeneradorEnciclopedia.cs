using UnityEngine;

public class GeneradorEnciclopedia : MonoBehaviour
{
    [Header("DATOS")]
    public ResiduoData[] todosLosResiduos;

    [Header("REFERENCIAS")]
    public GameObject prefabBoton;
    public Transform contenedor; // Grid o panel
    public EnciclopediaManager manager;

    void Start()
    {
        GenerarItems();
    }

    void GenerarItems()
    {
        foreach (ResiduoData data in todosLosResiduos)
        {
            GameObject nuevo = Instantiate(prefabBoton, contenedor);

            ItemEnciclopediaUI item = nuevo.GetComponent<ItemEnciclopediaUI>();

            if (item != null)
            {
                item.Inicializar(data, manager);
            }
        }
    }
}