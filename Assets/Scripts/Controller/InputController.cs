using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class InputController : MonoBehaviour
    {
        private Camera cam;
        private PlayerInputActions input;

        private Vector2 pointerPosition;

        private void Awake()
        {
            cam = Camera.main;
            input = new PlayerInputActions();

            input.Player.PointerPosition.performed += ctx =>
                pointerPosition = ctx.ReadValue<Vector2>();

            input.Player.Click.performed += ctx => HandleClick();
        }

        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
        }

        private void HandleClick()
        {
            Ray ray = cam.ScreenPointToRay(pointerPosition);

            if (!Physics.Raycast(ray, out RaycastHit hitData))
                return;

            HandleClickable(hitData);
        }

        private void HandleClickable(RaycastHit hitData)
        {
            if (hitData.collider.TryGetComponent(out Interfaces.IClickable clickable))
            {
                clickable.OnClick();
            }
        }
    }
}