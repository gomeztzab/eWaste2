using UnityEngine;
using System.Collections;

public enum TipoResiduo
{
    Bateria,
    Celular,
    MP3,
    Mouse,
    USB,
    Television,    // ← NUEVO
    Pilas,          // ← NUEVO
    Consola,        // ← NUEVO
    Placa,
    Control,
    Memoria,
    Audifonos,
    Disco,
    Camara,
    Lavadora,
    Licuadora,
    Microondas,
    Refrigerador
}

public enum NivelRiesgo
{
    Bajo,
    Medio,
    Alto
}

public class Residuo : MonoBehaviour, IScaneable
{
    [Header("TIPO Y RIESGO")]
    public TipoResiduo tipo;
    public NivelRiesgo riesgo;
    public bool requiereEscaneo = false;

    [Header("PUNTUACIÓN")]
    public int valorPuntos = 10; // ← EDITABLE POR PREFAB

    [Header("MOVIMIENTO")]
    public float velocidad = 2f;
    public float velocidadRegreso = 8f;

    [Header("LIMITES")]
    public float limiteDerecho = 8f;

    [Header("ESTADO")]
    public bool escaneado = false;
    public bool EstaEnZonaVisible { get; set; } = false;  // ← AGRÉGALO AQUÍ

    // PRIVADAS
    private bool fueAceptado = false;
    private bool regresando = false;
    private bool arrastrando = false;
    private bool destruido = false;
    private bool procesado = false;
    private Vector3 posicionInicial;
    private SpriteRenderer spriteRenderer;  // ← PARA EL SCANNER

    void Start()
    {
        posicionInicial = transform.position;

        // Obtener el SpriteRenderer (puede estar en el objeto o en hijos)
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (spriteRenderer == null)
        {
            // Crear uno si no existe (por si acaso)
            Debug.LogWarning($"⚠️ {gameObject.name} sin SpriteRenderer, creando...");
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }

    
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
        if (GameManager.juegoPausado) return;

        if (destruido) return;

        // Movimiento automático
        if (!arrastrando && !regresando)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }

        // Residuo escapado
        if (!arrastrando && !fueAceptado && transform.position.x > 10.7f)
        {
            destruido = true;

            GameManager.instancia.RegistrarErrorGlobal();

            Debug.Log("💀 Residuo escapado: " + tipo);

            Destroy(gameObject);
        }
    }

    IEnumerator RegresarSuave()
    {
        regresando = true;

        float lineaY = posicionInicial.y; // Volver a Y inicial
        float objetivoY = posicionInicial.y;

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
        destruido = true;
    }

    public bool YaProcesado()
    {
        return procesado;
    }

    public void MarcarProcesado()
    {
        procesado = true;
    }

    public Transform ObtenerTransform()
    {
        return transform;
    }

    public SpriteRenderer ObtenerSpriteRenderer()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        return spriteRenderer;
    }

    // GETTERS
    public int GetValorPuntos() => valorPuntos;
    public NivelRiesgo GetRiesgo() => riesgo;
}