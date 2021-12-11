using System;
using System.Collections.Generic;
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
    private Vector2Int RandomItemSet()
    {
        Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2Int((int)Math.Round(pos.x), (int)Math.Round(pos.y));
    }
    private void SelectedTwoElement()
    {
        var index = RandomItemSet();
        if (_selectedOne == null)
        {
            _selectedOne = _board[index.x, index.y];
        }
        else
        {
            _selectedTwo = _board[index.x, index.y];
            Swap();
        }
    }

    private void CheckCoincidence()
    {
        List<Tail> _tail = new List<Tail>();
        int count = 0;
        int y = 0;
        for (int x = 0, x1 = 1; x < _board.GetLength(0) && x1 < _board.GetLength(0) - 1; x++, x1++)
        {
            if (_board[x, y].Render == _board[x1, y].Render && _board[x, y].Render == _board[x1 - 1, y].Render)
            {
                _tail.Add(_board[x, y]);
                _tail.Add(_board[x1, y]);
                count++;
                if (count > 1)
                {
                    foreach (var item in _tail)
                    {
                        item.SetSprite(_default);
                    }
                }
            }
            else
            {
                _tail.Clear();
                count = 0;
            }
        }
    }
    private void CheckAllColumn()
    {
        int row = _board.GetLength(0);
        for (int i = 0; i < row; i++)
        {
            ChekcColumn(i);
        }
    }
    private void CheckAllRow()
    {
        int clomn = _board.GetLength(1);
        for (int i = 0; i < clomn; i++)
        {
          CheckRow(i);          
        }
    }
    private void CheckRow(int column) 
    {
        List<Tail> tail = new List<Tail>();
        Tail temp = null;
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if (temp == null)
            {
                temp = _board[i, column];
            }
            if (_board[i,column].Render == temp.Render)
            {
                tail.Add(_board[i, column]);             
                if (tail.Count >2)
                {
                    foreach (var item in tail)
                    {
                        item.SetSprite(_default);
                    }
                }
            }
            else
            {
                tail.Clear();
                temp = _board[i, column];
                tail.Add(_board[i, column]);
            }
        }
     
    }
    private void ChekcColumn(int column)
    {
        List<Tail> tail = new List<Tail>();
        Tail temp = null;
        for (int i = 0; i < _board.GetLength(1); i++)
        {
            if (temp == null)
            {
                temp = _board[ column,i];
            }
            if (_board[column, i].Render == temp.Render)
            {
                tail.Add(_board[column, i]);
                if (tail.Count > 2)
                {
                    foreach (var item in tail)
                    {
                        item.SetSprite(_default);
                    }
                }
            }
            else
            {
                tail.Clear();
                temp = _board[column, i];
                tail.Add(_board[column, i]);
            }
        }
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0)))
        {           
            SelectedTwoElement();
        }
    }
    private void Deselect()
    {
        CheckAllRow();
        CheckAllColumn();
        _selectedOne = null;
        _selectedTwo = null;
    }

}







