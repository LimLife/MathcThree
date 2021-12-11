using UnityEngine;

public class CameraPostion : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<Camera>();
        SetPostion();
    }
    private void SetPostion()
    {
        var postion = _camera.ScreenToWorldPoint(Input.mousePosition);
        var newPos = new Vector3(postion.x * 2, postion.y * 2, -10);
        _camera.transform.position = newPos;
    }

}
