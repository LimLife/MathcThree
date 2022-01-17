using System;
using System.Collections;
using UnityEngine;

public class Borad : MonoBehaviour
{
    [SerializeField] private Data _config;
    [SerializeField] private Tail _prefab;

    private Camera _camera;



    private Sprite[] _tail => _config.Tail;
    private Sprite _default => _config.DefaultSprite;

    private int _height => _config.Height;//9
    private int _width => _config.Width;//16   

    private Tail[,] _board;
    private Tail _selectedOne = null;
    private Tail _selectedTwo = null;



    private void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
        _board = new Tail[_width, _height];
        CrateBoard();
    }
    private void CrateBoard()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int r = UnityEngine.Random.Range(0, _tail.Length - 1);
                var item = Instantiate(_prefab, new Vector3(x, y, 0), Quaternion.identity);
                item.gameObject.name = ($"{x},{y}");
                item.SetSprite(_tail[r]);
                _board[x, y] = item;
            }
        }
    }
    private void Swap()
    {
        Sprite temp = _selectedTwo.Render;
        _selectedTwo.SetSprite(_selectedOne.Render);

        _selectedOne.SetSprite(temp);

        Deselect();
    }
    private Vector2Int GetItemPostion()
    {
        Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2Int((int)Math.Round(pos.x), (int)Math.Round(pos.y));
    }
    private void SelectedTwoElement()
    {
        var index = GetItemPostion();
        if (_selectedOne == null)
        {
            if (index.x > _width || index.y > _height || index.x < 0 || index.y < 0)
            {
                return;
            }
            _selectedOne = _board[index.x, index.y];
        }
        else
        {
            _selectedTwo = _board[index.x, index.y];
            Swap();
        }
    }


    private void AddNewTail()
    {
        for (int x = 0; x < _width; x++)
        {
            if (_board[x, _height - 1].Render == _default)
            {
                _board[x, _height - 1].SetSprite(_tail[UnityEngine.Random.Range(0, _tail.Length)]);

                BoardFaced.Falling(_board, _default);
                AddNewTail();
            }
        }
    }

    private IEnumerator AddNewTails()
    {
        int count = 0;
        while (count < _height)
        {           
            count++;
            AddNewTail();
            yield return new WaitForSeconds(0.2f);

        }
        yield break;
    }
    private void Deselect() //2 лишних метода в нем
    {
        CheckAllRow();
        CheckAllColumn();
        StartCoroutine(Falling(_height, 0.3f));
        _selectedOne = null;
        _selectedTwo = null;
    }


    private IEnumerator Falling(int count, float timeDilay)
    {
        int time = 0;
        while (time <= count)
        {
            time++;
            yield return new WaitForSeconds(timeDilay);

            if (time == count)
            {
                AddNewTail();
                yield break;
            }
            BoardFaced.Falling(_board, _default);
        }
    }
    private void CheckAllRow()
    {
        for (int i = 0; i < _width; i++)
        {
            BoardFaced.CheckClumn(_board, i, _default);
        }
    }
    private void CheckAllColumn()
    {
        for (int i = 0; i < _height; i++)
        {
            BoardFaced.CheckRow(_board, i, _default);
        }
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0)))
        {
            SelectedTwoElement();

        }
    }
}







