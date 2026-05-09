using UnityEngine;

public class AjustarCamara : MonoBehaviour
{
    public float tamañoBase = 5f;
    public float aspectoBase = 16f / 9f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        float aspectoActual =
            (float)Screen.width / Screen.height;

        if (aspectoActual < aspectoBase)
        {
            cam.orthographicSize =
                tamañoBase *
                (aspectoBase / aspectoActual);
        }
    }
}