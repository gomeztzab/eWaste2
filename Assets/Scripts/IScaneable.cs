using UnityEngine;

public interface IScaneable
{
    bool EstaEnZonaVisible { get; set; }

    Transform ObtenerTransform();

    SpriteRenderer ObtenerSpriteRenderer();
}