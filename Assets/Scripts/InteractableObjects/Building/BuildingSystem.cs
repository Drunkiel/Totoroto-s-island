using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;
    public static bool inBuildingMode;

    public GridLayout gridLayout;
    private Grid grid;
    public Vector2 gridSize;

    public GameObject[] buildingsPrefabs;
    public GameObject buildingMaterial;
    public Material[] materials;

    public GameObject UI;
    public BuildingUI _buildingUI;
    public PlacableObject _objectToPlace;

    void Awake()
    {
        instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        if (!_objectToPlace) return;

        ChangeMaterial(CanBePlaced());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) Destroy(_objectToPlace.gameObject);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 50))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(position);

        if (position.x > gridSize.x || position.x < -gridSize.x || position.z > gridSize.y || position.z < -gridSize.y) return SnapCoordinateToGrid(Vector3.zero);

        position = grid.GetCellCenterWorld(cellPosition);
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        if (inBuildingMode) return;

        inBuildingMode = true;
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
        _objectToPlace = newObject.GetComponent<PlacableObject>();
        Instantiate(buildingMaterial, newObject.transform);
        newObject.AddComponent<ObjectDrag>();

        OpenUI(true);
    }

    public void OpenUI(bool destroy)
    {
        //UI
        UI.SetActive(true);

        //Removing listeners
        UI.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        UI.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();

        //Adding new listeners
        UI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => PlaceButton());

        if (destroy) UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { Destroy(_objectToPlace.gameObject); UI.SetActive(false); inBuildingMode = false; });
        else
        {
            Vector3 oldPosition = _objectToPlace.transform.position;
            UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { _objectToPlace.transform.position = oldPosition; PlaceButton(); });
        }
    }

    public void PlaceButton()
    {
        if (CanBePlaced()) _objectToPlace.Place();
        else Destroy(_objectToPlace.gameObject);
        UI.SetActive(false);
        inBuildingMode = false;
    }

    private bool CanBePlaced()
    {
        if (_objectToPlace == null) return false;
        return !_objectToPlace.transform.GetChild(1).GetComponent<MultiTriggerController>().isTriggered;
    }

    private void ChangeMaterial(bool itCanBePlaced)
    {
        if (itCanBePlaced) _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<MeshRenderer>().material = materials[0];
        else _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<MeshRenderer>().material = materials[1];
    }
}
