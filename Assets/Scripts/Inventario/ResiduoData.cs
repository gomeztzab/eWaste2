using UnityEngine;

[CreateAssetMenu(fileName = "ResiduoData", menuName = "Enciclopedia/Residuo")]
public class ResiduoData : ScriptableObject
{
    public string nombre;
    public string descripcion;
    public string utilidadAmbiental;
    public string rareza;

    public Sprite imagen;
}