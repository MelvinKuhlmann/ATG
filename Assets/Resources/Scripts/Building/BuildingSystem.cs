using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    private static Camera mainCamera;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    public GameObject prefab1;
    public GameObject prefab2;

    private PlaceableObject objectToPlace;
    
    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //TODO temp hack to start building blocks
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InitializeWithObject(prefab1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            InitializeWithObject(prefab2);
        }

        if (!objectToPlace)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();
                Debug.Log(objectToPlace.Size);
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
            }
            else
            {
                Destroy(objectToPlace.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlace.gameObject);
        }
    }

    #endregion
    
    #region Utils

    
    public static Vector3 GetMouseWorldPosition()
    {
        // Multiply with -1 otherwise the values of x and y are inverted
        return mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z)) * -1;
    }
    
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return new Vector3(position.x, position.y, 0f);
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        Debug.Log(area);
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        Debug.Log(array);
        return array;
    }
    
    #endregion
    
    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        bool canPlaced = true;
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;

        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray)
        {
            Debug.Log(b);
            Debug.Log(b == whiteTile);
            if (b == whiteTile)
            {
                //return false;
                canPlaced = false;
            }
        }

        Debug.Log(canPlaced);
        //return true;
        return canPlaced;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, 
            start.x, start.y, 
            start.x + size.x, start.y + size.y);
    }
    
    #endregion
}
