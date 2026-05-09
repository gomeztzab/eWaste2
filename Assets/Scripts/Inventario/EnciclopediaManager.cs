using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnciclopediaManager : MonoBehaviour
{
    [Header("PANEL")]
    public GameObject panelInformacion;

    [Header("TEXTOS")]
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDescripcion;
    public TextMeshProUGUI textoUtilidad;
    public TextMeshProUGUI textoRareza;

    [Header("IMAGEN")]
    public Image imagen;

    public void MostrarInformacion(ResiduoData data)
    {
        panelInformacion.SetActive(true);

        textoNombre.text = data.nombre;
        textoDescripcion.text = data.descripcion;
        textoUtilidad.text = "Degrado: " + data.utilidadAmbiental;
        textoRareza.text = "Contenedor: " + data.rareza;

        if (imagen != null)
            imagen.sprite = data.imagen;
    }

    public void CerrarPanel()
    {
        panelInformacion.SetActive(false);
    }
}