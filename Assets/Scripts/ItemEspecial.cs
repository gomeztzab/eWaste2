using UnityEngine;

public enum TipoItemEspecial
{
    VidaPositiva,
    VidaNegativa,
    MultiplicadorX2,
    VelocidadSpawn
}

public class ItemEspecial : MonoBehaviour, IScaneable
{
    [Header("TIPO ITEM")]
    public TipoItemEspecial tipoItem;

    [Header("MOVIMIENTO")]
    public float velocidad = 2f;

    [Header("LIMITES")]
    public float limiteDerecho = 8f;

    // 🔥 Scanner
    public bool EstaEnZonaVisible { get; set; }

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);

        if (spriteRenderer == null)
        {
            Debug.LogError("❌ ITEM SIN SPRITERENDERER: " + gameObject.name);
        }
        else
        {
            Debug.Log("✅ Sprite encontrado en: " + gameObject.name);
        }
    }

    void Update()
    {
        if (GameManager.juegoPausado) return;

        // Movimiento igual que residuos
        transform.Translate(
            Vector2.right *
            velocidad *
            Time.deltaTime
        );

        // Destruir si sale de pantalla
        if (transform.position.x > 10.7f)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        AplicarEfecto();
    }

    public void AplicarEfecto()
    {


        if (GameManager.instancia == null)
            return;

        GameMode modo =
            GameManager.instancia.ObtenerModoActual();

        ArcadeMode arcade =
    modo as ArcadeMode;

        if (arcade != null)
        {
            switch (tipoItem)
            {
                case TipoItemEspecial.MultiplicadorX2:

                    arcade.ActivarX2();

                    Debug.Log("🔥 X2");

                    break;

                case TipoItemEspecial.VelocidadSpawn:

                    arcade.ActivarVelocidad();

                    Debug.Log("⚡ Velocidad");

                    break;
            }

            Destroy(gameObject);

            return;
        }


        SurvivalMode survival =
            modo as SurvivalMode;

        // =========================
        // SURVIVAL MODE
        // =========================

        if (survival != null)
        {
            CronometroJuego cronometro =
                Object.FindFirstObjectByType<CronometroJuego>();

            if (cronometro != null)
            {
                switch (tipoItem)
                {
                    case TipoItemEspecial.VidaPositiva:

                        cronometro.SumarTiempo(10f);

                        Debug.Log("⏱️ +10 segundos");

                        break;

                    case TipoItemEspecial.VidaNegativa:

                        cronometro.RestarTiempo(10f);

                        Debug.Log("💀 -10 segundos");

                        break;
                }
            }

            Destroy(gameObject);

            return;
        }

        // =========================
        // MODOS NORMALES
        // =========================

        switch (tipoItem)
        {
            case TipoItemEspecial.VidaPositiva:

                GameManager.instancia.GanarVida(1);

                Debug.Log("❤️ Vida ganada");

                break;

            case TipoItemEspecial.VidaNegativa:

                GameManager.instancia.PerderVida();

                Debug.Log("💀 Vida perdida");

                break;
        }

        Destroy(gameObject);
    }

    // =========================
    // IScaneable
    // =========================

    public Transform ObtenerTransform()
    {
        return transform;
    }

    public SpriteRenderer ObtenerSpriteRenderer()
    {
        return spriteRenderer;
    }
}