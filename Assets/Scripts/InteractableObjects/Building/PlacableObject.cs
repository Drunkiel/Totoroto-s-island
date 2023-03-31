using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    public bool isPlaced;
    public Vector3Int size;
    public Vector3[] vertices;
    public GameObject border;

    public string[] objectsTag;

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();
    }

    public virtual void Place()
    {
        Destroy(GetComponent<ObjectDrag>());
        transform.GetComponentInChildren<AutoSize>().AutoDestroy();
        BuildingSystem.instance._objectToPlace = null;

        isPlaced = true;
    }

    public virtual void Move()
    {
        if (BuildingSystem.inBuildingMode) return;
        BuildingSystem.inBuildingMode = true;

        isPlaced = false;
        BuildingSystem.instance._objectToPlace = this;
        BuildingSystem.instance.OpenUI(false);

        gameObject.AddComponent<ObjectDrag>();
        Instantiate(border, transform);
    }

    private void GetColliderVertexPositionLocal()
    {
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        vertices = new Vector3[4];
        vertices[0] = collider.center + new Vector3(-collider.size.x, -collider.size.y, -collider.size.z) * 0.5f;
        vertices[1] = collider.center + new Vector3(collider.size.x, -collider.size.y, -collider.size.z) * 0.5f;
        vertices[2] = collider.center + new Vector3(collider.size.x, -collider.size.y, collider.size.z) * 0.5f;
        vertices[3] = collider.center + new Vector3(-collider.size.x, -collider.size.y, collider.size.z) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        size = new Vector3Int(Mathf.CeilToInt(collider.size.x), Mathf.CeilToInt(collider.size.y), Mathf.CeilToInt(collider.size.z));
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }
}
