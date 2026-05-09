using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemEnciclopediaUI : MonoBehaviour
{
    public ResiduoData data;

    public Image icono;
    public TextMeshProUGUI textoNombre;

    private EnciclopediaManager manager;

    public void Inicializar(ResiduoData nuevoData, EnciclopediaManager m)
    {
        data = nuevoData;
        manager = m;

        if (data != null)
        {
            if (icono != null)
                icono.sprite = data.imagen;

            if (textoNombre != null)
                textoNombre.text = data.nombre;
        }

        // 🔥 Conectar botón automáticamente
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (manager != null && data != null)
        {
            manager.MostrarInformacion(data);
        }
    }
}