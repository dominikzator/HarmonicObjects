using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    private int rowIndex;
    private int columnIndex;

    private bool initialized = false;

    public int RowIndex => rowIndex;
    public int ColumnIndex => columnIndex;

    public void SetData(int rowIndex, int columnIndex)
    {
        if (initialized)
        {
            return;
        }
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
        initialized = true;
    }
}
