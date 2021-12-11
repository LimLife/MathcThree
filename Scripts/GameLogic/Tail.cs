using UnityEngine;

public class Tail : MonoBehaviour
{
    public Sprite Render => _sprite.sprite;

    [SerializeField] private SpriteRenderer _sprite;
    public void SetSprite(Sprite render)
    {
        _sprite.sprite = render;
    }
}

