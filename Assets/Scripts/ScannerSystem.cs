using UnityEngine;

public class ScannerSystem : MonoBehaviour
{
    [SerializeField] private float ancho = 4f;
    [SerializeField] private float alto = 3f;

    [SerializeField] private Color colorNormal = Color.white;

    //[SerializeField]
    //private Color colorBorroso =
    //    new Color(0.4f, 0.4f, 0.4f, 0.6f);
    [SerializeField]
    private Color colorBorroso = Color.gray;
    void Update()
    {
        ActualizarResiduos();
        ActualizarItems();
    }

    void ActualizarResiduos()
    {
        Residuo[] residuos =
            FindObjectsByType<Residuo>(
                FindObjectsSortMode.None
            );

        foreach (Residuo r in residuos)
        {
            if (r != null)
            {
                ActualizarObjeto(r);
            }
        }
    }

    void ActualizarItems()
    {
        ItemEspecial[] items =
            FindObjectsByType<ItemEspecial>(
                FindObjectsSortMode.None
            );

        foreach (ItemEspecial item in items)
        {
            if (item != null)
            {
                ActualizarObjeto(item);
            }
        }
    }

    void ActualizarObjeto(IScaneable obj)
    {
        Vector3 posicion =
            obj.ObtenerTransform().position;

        SpriteRenderer sr =
            obj.ObtenerSpriteRenderer();

        if (sr == null) return;

        bool visible =
            EstaEnZonaVisible(posicion);

        obj.EstaEnZonaVisible = visible;

        sr.color =
            visible ? colorNormal : colorBorroso;

        sr.sortingOrder =
            visible ? 2 : 1;
    }

    bool EstaEnZonaVisible(Vector3 posicion)
    {
        Vector3 zonaPos = transform.position;

        bool dentroX =
            Mathf.Abs(posicion.x - zonaPos.x)
            <= ancho / 2f;

        bool dentroY =
            Mathf.Abs(posicion.y - zonaPos.y)
            <= alto / 2f;

        return dentroX && dentroY;
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        Gizmos.color = Color.green;

        Vector3 size =
            new Vector3(ancho, alto, 0);

        Gizmos.DrawWireCube(pos, size);

        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(pos, 0.2f);
    }
}