using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Config/Data", order = 64)]
public class Data : ScriptableObject
{ 
    public int Width => _width;
    public int Height => _height;
    public Sprite [] Tail => _tail;
    public Sprite DefaultSprite => _defultSprite;
    [SerializeField] private Sprite [] _tail;
    [SerializeField] private Sprite _defultSprite;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
}
