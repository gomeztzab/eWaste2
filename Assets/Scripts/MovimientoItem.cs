using UnityEngine;

public class MovimientoItem : MonoBehaviour
{
    public float velocidad = 2f;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.enabled = false;
    }

    void Update()
    {
        if (GameManager.juegoPausado) return;

        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        if (transform.position.x > 8f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scanner"))
        {
            if (sr != null)
                sr.enabled = true;
        }
    }
}