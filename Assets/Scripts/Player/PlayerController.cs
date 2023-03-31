using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    PlayerObjectInteraction _interaction;
    NavMeshAgent playerAgent;

    public LayerMask NpcMask;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        _interaction = GetComponent<PlayerObjectInteraction>();
    }

    void Update()
    {
        MouseMovement();
        MouseInteraction();
    }

    void MouseMovement()
    {
        if (Input.GetMouseButtonDown(0) && !BuildingSystem.inBuildingMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && EventSystem.current.currentSelectedGameObject == null)
            {
                playerAgent.SetDestination(hit.point);
                /*                    ArrowToPlace.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);*/
            }
        }
    }

    void MouseInteraction()
    {
        if (Input.GetMouseButtonDown(1) && EventSystem.current.currentSelectedGameObject == null && !CameraController.isCameraMoving)
        {
            if (_interaction.CheckIfClickedBuilding(true)) return;
        }
    }
}
