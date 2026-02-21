using UnityEngine;

public class Contenedor : MonoBehaviour
{
    public TipoResiduo tipoAceptado;
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Residuo r = other.GetComponent<Residuo>();

        if (r != null)
        {
            if (r.tipo == tipoAceptado)
            {
                gameManager.SumarPuntos(10);
                r.MarcarComoAceptado(); // importante
                Destroy(r.gameObject);
            }
            else
            {
                gameManager.PerderVida();
                Destroy(r.gameObject);
            }
        }
    }
}
