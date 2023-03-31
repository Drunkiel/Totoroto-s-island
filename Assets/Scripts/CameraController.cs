using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public static bool isCameraMoving;

    public float scrollSensitivity;
    public float moveSpeed;

    public float maxScroll;
    public float minScroll;

    public int[] clampXPosition = new int[2];
    public int[] clampZPosition = new int[2];

    PlayerObjectInteraction _interaction;

    void Start()
    {
        _interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObjectInteraction>();
    }

    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        if (EventSystem.current.IsPointerOverGameObject() || (_interaction.CheckIfClickedBuilding(false) && !isCameraMoving))
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            MouseMovement();
            isCameraMoving = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            ShowAndUnlockCursor();
            isCameraMoving = false;
        }

        if (Input.GetMouseButtonUp(2))
        {
            ShowAndUnlockCursor();
        }
        else
        {
            MouseWheeling();
        }
    }

    void ShowAndUnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void MouseMovement()
    {
        HideAndLockCursor();
        Vector3 NewPosition = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        Vector3 actualPosition = transform.position;

        if (NewPosition.x > 0.0f) actualPosition -= transform.right * moveSpeed;

        if (NewPosition.x < 0.0f) actualPosition += transform.right * moveSpeed;

        if (NewPosition.z > 0.0f) actualPosition -= transform.forward * moveSpeed;

        if (NewPosition.z < 0.0f) actualPosition += transform.forward * moveSpeed;

        actualPosition.y = transform.position.y;
        transform.position = ClampCamera(actualPosition);
    }

    private Vector3 ClampCamera(Vector3 target)
    {
        float maxX = Mathf.Clamp(target.x, clampXPosition[0], clampXPosition[1]);
        float maxZ = Mathf.Clamp(target.z, clampZPosition[0], clampZPosition[1]);

        return new Vector3(maxX, target.y, maxZ);
    }

    void MouseWheeling()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.fieldOfView > minScroll)
        {
            Camera.main.fieldOfView -= scrollSensitivity;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.fieldOfView < maxScroll)
        {
            Camera.main.fieldOfView += scrollSensitivity;
        }
    }
}
