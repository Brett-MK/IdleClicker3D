using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RTS_Camera : MonoBehaviour
{
    [Header("Camera Bounds")]
    public float minX = -7f;
    public float maxX = 7f;
    public float minZ = -25f;
    public float maxZ = -10f;

    [Header("Camera Zoom")]
    public float minZoom = 1f;
    public float maxZoom = 5f;
    public float zoomSpeed = 20f;
    public float panSpeed = 1f;
    public float smoothTime = 0.05f;

    private Camera cam;
    private Vector3 velocity;
    private float zoomVelocity;

    private PlayerInputActions inputActions;
    private bool isDragging;
    private Vector3 dragOrigin;
    private float zoomDelta;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        inputActions = new PlayerInputActions();

        inputActions.Camera.PanEnable.performed += ctx =>
        {
            isDragging = true;
            dragOrigin = GetMouseWorldPosition();
        };
        inputActions.Camera.PanEnable.canceled += ctx => isDragging = false;

        inputActions.Camera.Zoom.performed += ctx => zoomDelta = ctx.ReadValue<float>();
        inputActions.Camera.Zoom.canceled += ctx => zoomDelta = 0f;
    }

    private void OnEnable() => inputActions.Camera.Enable();
    private void OnDisable() => inputActions.Camera.Disable();

    private void Update()
    {
        HandlePan();
        HandleZoom();
    }

    private void HandlePan()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        if (!isDragging) return;

        Vector3 currentPos = GetMouseWorldPosition();
        Vector3 move = dragOrigin - currentPos;
        move.y = 0; // keep camera height fixed
        Vector3 targetPos = transform.position + move;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.z = Mathf.Clamp(targetPos.z, minZ, maxZ);
        targetPos.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    private void HandleZoom()
    {
        if (Mathf.Abs(zoomDelta) > 0f)
        {
            float targetZoom = cam.orthographicSize - zoomDelta * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        ground.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}