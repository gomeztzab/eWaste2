using UnityEngine;

public class AudioAgent : MonoBehaviour
{
    [Header("AUDIO CLIPS A ARRASTRAR")]
    public AudioClip sonidoExito; // ← DRAG HERE
    public AudioClip sonidoError; // ← DRAG HERE
    public AudioClip sonidoCombo; // ← DRAG HERE
    public AudioClip musica; // ← DRAG HERE

    [Header("AUDIO SOURCES A ARRASTRAR")]
    public AudioSource audioSourceSFX; // ← Para efectos
    public AudioSource audioSourceMusica; // ← Para música

    [Header("VOLUMEN")]
    [SerializeField] private float volumenSFX = 0.7f;
    [SerializeField] private float volumenMusica = 0.3f;

    void Start()
    {
        // Configurar Audio Sources
        if (audioSourceSFX == null)
            audioSourceSFX = gameObject.AddComponent<AudioSource>();
        if (audioSourceMusica == null)
            audioSourceMusica = gameObject.AddComponent<AudioSource>();

        // Configurar música
        audioSourceMusica.clip = musica;
        audioSourceMusica.loop = true;
        audioSourceMusica.volume = volumenMusica;
        audioSourceMusica.Play();

        // Suscribirse a eventos
        Contenedor.OnValidacion += ReproducirValidacion;
    }

    void ReproducirValidacion(bool esAcierto)
    {
        audioSourceSFX.clip = esAcierto ? sonidoExito : sonidoError;
        audioSourceSFX.pitch = esAcierto ? 1.2f : 0.8f;
        audioSourceSFX.volume = volumenSFX;
        audioSourceSFX.PlayOneShot(audioSourceSFX.clip);
    }

    void ReproducirCombo(int combo, int multiplicador)
    {
        if (combo % 5 == 0) // Cada 5 combos
        {
            audioSourceSFX.pitch = 1f + (combo * 0.05f);
            audioSourceSFX.volume = volumenSFX;
            audioSourceSFX.PlayOneShot(sonidoCombo);
        }
    }

    void OnDestroy()
    {
        Contenedor.OnValidacion -= ReproducirValidacion;
    }
}