using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridBuildSystem : MonoBehaviour
{
    public static GridBuildSystem Instance { get; private set; }

    //public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;

    
    private Grid<GridObject> grid;
    //[SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList
    public Canvas canvas;
    [SerializeField] private Transform testIcon;
    private RectTransform itemContainer;

    private void Awake()
    {
        Instance = this;
        int gWidth = 10;
        int gHeight = 10;
        float cellSize = 10f;
        grid = new Grid<GridObject>(gWidth, gHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int y;
        //public PlacedObject placedObject;

        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            //placedObject = null;
        }
        public override string ToString()
        {
            return x + ", " + y;
        }
    }

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue() / canvas.scaleFactor);
            LayerMask mouseColliderLayerMask = LayerMask.GetMask("NEWUI");
            Vector3 raycastPoint;
            Quaternion rotation = Quaternion.Euler(0,0, 0);
            //Quaternion rotation = (0,0,0);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            {
                raycastPoint = raycastHit.point;
            }
            else
            {
                raycastPoint = Vector3.zero;
            }
            //raycastPoint.x -= 0.5f;
            //raycastPoint.y -= 120;
            //raycastPoint.z = 0;
            grid.GetXY(raycastPoint, out int x, out int y);
            Instantiate(testIcon, grid.GetWorldPosition(x,y), rotation, canvas.GetComponent<Transform>());
            //testIcon.SetPositionAndRotation(raycastPoint, rotation);
        }
    }
    //public void OnClick(InputValue value)
    //{
    //    Instantiate(testIcon, Mouse.current.position.ReadValue() / canvas.scaleFactor, Quaternion.identity, canvas.GetComponent<Transform>());
    //}
}
