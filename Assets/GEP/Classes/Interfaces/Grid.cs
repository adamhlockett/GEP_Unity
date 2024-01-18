using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridAr;
    private Vector3 originPosition;
    private TextMesh[,] debugTextAr;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridAr = new TGridObject[width, height];
        debugTextAr = new TextMesh[width, height];

        for (int x = 0; x < gridAr.GetLength(0); x++)
        {
            for (int y = 0; y < gridAr.GetLength(1); y++)
            {
                gridAr[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = true;
        if (showDebug)
        {
            for (int x = 0; x < gridAr.GetLength(0); x++)
            {
                for (int y = 0; y < gridAr.GetLength(1); y++)
                {
                    Transform parent = GameObject.FindWithTag("GridTarget").GetComponent<Transform>();
                    string text = gridAr[x, y].ToString();
                    Vector3 localPos = (new Vector3(x, y) * cellSize + originPosition) + new Vector3(cellSize, cellSize) * .5f;
                    localPos.x -= 60;
                    localPos.y -= 32;
                    int fontSize = 20;
                    Color color = Color.white;
                    TextAnchor textAnchor = TextAnchor.MiddleCenter;
                    TextAlignment textAlignment = TextAlignment.Left;
                    int sortingOrder = 5000;

                    GameObject gO = new GameObject("World_Text", typeof(TextMesh));
                    Transform tr = gO.transform;
                    tr.SetParent(parent, false);
                    tr.localPosition = localPos;
                    TextMesh textMesh = gO.GetComponent<TextMesh>();
                    textMesh.anchor = textAnchor;
                    textMesh.alignment = textAlignment;
                    textMesh.text = text;
                    textMesh.fontSize = fontSize;
                    textMesh.color = color;
                    textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

                    debugTextAr[x, y] = textMesh;

                    Debug.DrawLine(new Vector3(x, y) * cellSize, new Vector3(x, y + 1) * cellSize, Color.white, 100f);
                    Debug.DrawLine(new Vector3(x, y) * cellSize, new Vector3(x + 1, y) * cellSize, Color.white, 100f);
                }
            }
            Debug.DrawLine(new Vector3(0, height) * cellSize, new Vector3(width, height) * cellSize, Color.white, 100f);
            Debug.DrawLine(new Vector3(width, 0) * cellSize, new Vector3(width, height) * cellSize, Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
            {
                debugTextAr[eventArgs.x, eventArgs.y].text = gridAr[eventArgs.x, eventArgs.y]?.ToString();
            };
        }

        //SetValue(2, 1, 56);
    }

    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos  - originPosition).x/ cellSize);
        y = Mathf.FloorToInt((worldPos  - originPosition).y/ cellSize);
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height) { 
            gridAr[x, y] = value;
            debugTextAr[x,y].text = gridAr[x,y].ToString();
        }
    }

    public void SetValue(Vector3 worldPos, TGridObject value)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetValue(x, y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height) return gridAr[x, y];
        else return default(TGridObject);
    }
    
    public TGridObject GetValue(Vector3 worldPosit)
    {
        int x, y;
        GetXY(worldPosit, out x, out y);
        return GetValue(x, y);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
}
