using UnityEngine;
using System.Collections;

public enum TipoResiduo
{
    Bateria,
    Celular,
    Cable,
    Pantalla,
    USB
}

public class Residuo : MonoBehaviour
{
    public TipoResiduo tipo;
    public bool escaneado = false;
    public float velocidad = 2f;
    public float velocidadRegreso = 8f;

    private bool fueAceptado = false;
    private bool regresando = false;
    private bool arrastrando = false;

    void OnMouseDrag()
    {
        arrastrando = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    void OnMouseUp()
    {
        arrastrando = false;

        if (!fueAceptado)
        {
            StartCoroutine(RegresarSuave());
        }
    }

    void Update()
    {
        // Movimiento automático constante de la cinta
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    IEnumerator RegresarSuave()
    {
        regresando = true;

        // Queremos volver a la línea Y original (la de la cinta)
        float lineaY = transform.position.y;
        float objetivoY = 0f; // 🔥 PON AQUÍ la Y real de tu cinta

        while (Mathf.Abs(transform.position.y - objetivoY) > 0.05f)
        {
            float nuevaY = Mathf.Lerp(
                transform.position.y,
                objetivoY,
                velocidadRegreso * Time.deltaTime
            );

            transform.position = new Vector3(transform.position.x, nuevaY, 0);

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, objetivoY, 0);
        regresando = false;
    }

    public void MarcarComoAceptado()
    {
        fueAceptado = true;
    }
}
