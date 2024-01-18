using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GridXY<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x; public int y;
    }

    private int width, height;
    private float cellSize;
    private Vector3 originPos;
    private TGridObject[,] gridAr;

    public GridXY(int width, int height, float cellSize, Vector3 originPos, Func<GridXY<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridAr = new TGridObject[width, height];

        for (int x = 0; x < gridAr.GetLength(0); x++)
        {
            for (int y = 0; y < gridAr.GetLength(1); y++)
            {
                gridAr[x,y] = createGridObject(this,x, y);
            }
        }

        bool showDebug = true;
        if (showDebug)
        {
            TextMesh[,] debugTextAr = new TextMesh[width, height];

            for (int x = 0; x < gridAr.GetLength(0); x++)
            {
                for (int y = 0; y < gridAr.GetLength(1); y++) 
                {
                    Transform parent = null;
                    if (GameObject.FindWithTag("GridTarget"))
                    {
                        parent = GameObject.FindWithTag("GridTarget").GetComponent<Transform>();
                    }
                    
                    string text = gridAr[x, y].ToString();
                    Vector3 localPos = (new Vector3(x, y) * cellSize + originPos) + new Vector3(cellSize, cellSize) * .5f;
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
    }
}
