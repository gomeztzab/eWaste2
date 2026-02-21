using UnityEngine;

public class DragTouch : MonoBehaviour
{
    private Vector3 offset;
    private bool arrastrando = false;

    void OnMouseDown()
    {
        arrastrando = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        arrastrando = false;
    }

    void Update()
    {
        if (arrastrando)
        {
            Vector3 posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            posicion.z = 0;
            transform.position = posicion;
        }
    }
}
