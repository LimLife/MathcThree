using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardFaced
{
    public static void CheckRow(Tail[,] tail, int row, Sprite _default)
    {
        List<Tail> cels = new List<Tail>();
        Tail temp = null;
        for (int i = 0; i < tail.GetLength(0); i++)
        {
            if (temp == null)
            {
                temp = tail[i, row];
            }
            if (tail[i, row].Render == temp.Render)
            {
                cels.Add(tail[i, row]);
                if (cels.Count > 2)
                {
                    foreach (var item in cels)
                    {
                        item.SetSprite(_default);
                    }
                }
            }
            else
            {
                cels.Clear();
                temp = tail[i, row];
                cels.Add(tail[i, row]);
            }
        }
    }
    public static void CheckClumn(Tail[,] tail, int column, Sprite _default)
    {
        List<Tail> cels = new List<Tail>();
        Tail temp = null;
        for (int i = 0; i < tail.GetLength(1); i++)
        {
            if (temp == null)
            {
                temp = tail[column, i];
            }
            if (tail[column, i].Render == temp.Render)
            {
                cels.Add(tail[column, i]);
                if (cels.Count > 2)
                {
                    foreach (var item in cels)
                    {
                        item.SetSprite(_default);
                    }
                }
            }
            else
            {
                cels.Clear();
                temp = tail[column, i];
                cels.Add(tail[column, i]);
            }
        }
    }

    public static void Falling(Tail [,] tail,Sprite _default)
    {
        for (int y = 0; y < tail.GetLength(1) - 1; y++)
        {
            for (int x = 0; x < tail.GetLength(0); x++)
            {
                Tail next = tail[x, y + 1];
                if (tail[x, y].Render == _default)
                {
                    Tail current = next;
                    tail[x, y].SetSprite(current.Render); // x:0 y:1 >> x:0 y:0
                    next.SetSprite(_default);             
                }
            }
        }
    }
}


