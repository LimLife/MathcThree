using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite Render =>_sprite;

    private Sprite _sprite;
   public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
    }

}
