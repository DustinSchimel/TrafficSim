using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase redTile;

    public GameObject straight;
    public GameObject intersection;
    public GameObject turn;
    public GameObject house;
    public GameObject work;

    private PlaceableObject objectToPlace;


    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            InitializeWithObject(straight);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            InitializeWithObject(intersection);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            InitializeWithObject(turn);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            InitializeWithObject(house);
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            InitializeWithObject(work);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }
}
