using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridHolder : IInitializable
{
    private int rowCount;
    private int columnCount;
    public GameObject[,] Grid { get; set;}
    
    public int RowCount
    {
        get => rowCount;
        set => rowCount = value;
    }
    public int ColumnCount
    {
        get => columnCount;
        set => columnCount = value;
    }
    

    public void Initialize()
    {
        Debug.Log("GridHolder Initialize");
    }

    public void ConstructGrid(int rowCount, int columnCount)
    {
        RowCount = rowCount;
        ColumnCount = columnCount;
        Grid = new GameObject[rowCount,columnCount];
    }
}
