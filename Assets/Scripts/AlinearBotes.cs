using UnityEngine;

public class AlinearBotes : MonoBehaviour
{
    public float espacio =240f; // distancia entre botes
    public float alturaDesdeAbajo = 150f; // distancia desde el borde inferior

    void Start()
    {
        // 🔹 Colocar grupo abajo automáticamente
        float bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        transform.position = new Vector3(0, bottom + alturaDesdeAbajo, 0);

        // 🔹 Alinear hijos horizontalmente
        int cantidad = transform.childCount;
        float anchoTotal = (cantidad - 1) * espacio;
        float inicio = -anchoTotal / 2f;

        for (int i = 0; i < cantidad; i++)
        {
            Transform bote = transform.GetChild(i);
            bote.localPosition = new Vector3(inicio + (i * espacio), 0, 0);
        }
    }
}
