using UnityEngine;

public class DragTouch : MonoBehaviour
{
    private bool arrastrando = false;
    private Vector3 offset;

    [Header("ESCALA")]
    public float escalaAlArrastrar = 1.15f;

    [Header("LÍMITES")]
    public float minX = -9f;
    public float maxX = 9f;
    public float minY = -4f;
    public float maxY = 5f;

    private Vector3 escalaOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        if (GameManager.juegoPausado)
            return;

#if UNITY_ANDROID || UNITY_IOS

        ManejarTouch();

#else

        ManejarMouse();

#endif
    }

    // =========================
    // TOUCH
    // =========================

    void ManejarTouch()
    {
        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        Vector3 touchPos =
            Camera.main.ScreenToWorldPoint(
                touch.position
            );

        touchPos.z = 0;

        switch (touch.phase)
        {
            case TouchPhase.Began:

                Collider2D hit =
                    Physics2D.OverlapPoint(touchPos);

                if (hit != null &&
                    hit.gameObject == gameObject)
                {
                    arrastrando = true;

                    offset =
                        transform.position - touchPos;

                    transform.localScale =
                        escalaOriginal *
                        escalaAlArrastrar;
                }

                break;

            case TouchPhase.Moved:

                if (arrastrando)
                {
                    Vector3 nuevaPos =
                        touchPos + offset;

                    nuevaPos.x =
                        Mathf.Clamp(
                            nuevaPos.x,
                            minX,
                            maxX
                        );

                    nuevaPos.y =
                        Mathf.Clamp(
                            nuevaPos.y,
                            minY,
                            maxY
                        );

                    transform.position = nuevaPos;
                }

                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:

                if (arrastrando)
                {
                    arrastrando = false;

                    transform.localScale =
                        escalaOriginal;

                    SnapAlContenedor();
                }

                break;
        }
    }

    // =========================
    // MOUSE PC
    // =========================

    void ManejarMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(
                    Input.mousePosition
                );

            mousePos.z = 0;

            Collider2D hit =
                Physics2D.OverlapPoint(mousePos);

            if (hit != null &&
                hit.gameObject == gameObject)
            {
                arrastrando = true;

                offset =
                    transform.position - mousePos;

                transform.localScale =
                    escalaOriginal *
                    escalaAlArrastrar;
            }
        }

        if (Input.GetMouseButton(0) && arrastrando)
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(
                    Input.mousePosition
                );

            mousePos.z = 0;

            Vector3 nuevaPos =
                mousePos + offset;

            nuevaPos.x =
                Mathf.Clamp(
                    nuevaPos.x,
                    minX,
                    maxX
                );

            nuevaPos.y =
                Mathf.Clamp(
                    nuevaPos.y,
                    minY,
                    maxY
                );

            transform.position = nuevaPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (arrastrando)
            {
                arrastrando = false;

                transform.localScale =
                    escalaOriginal;

                SnapAlContenedor();
            }
        }
    }

    // =========================
    // SNAP
    // =========================

    void SnapAlContenedor()
    {
        Contenedor[] contenedores =
            FindObjectsByType<Contenedor>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

        Contenedor masCercano = null;

        float distanciaMin =
            Mathf.Infinity;

        foreach (Contenedor c in contenedores)
        {
            float distancia =
                Vector2.Distance(
                    transform.position,
                    c.transform.position
                );

            if (distancia < distanciaMin)
            {
                distanciaMin = distancia;
                masCercano = c;
            }
        }

        if (masCercano != null &&
            distanciaMin < 1.5f)
        {
            transform.position =
                masCercano.transform.position;
        }
    }
}