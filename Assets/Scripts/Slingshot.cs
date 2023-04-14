using UnityEngine;
using UnityEngine.InputSystem;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    private InputController inputActions;

    private void Awake()
    {
        inputActions = new InputController();

        inputActions.Player.Shoot.performed += contex => Shoot();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Shoot()
    {
        Vector3 touchPosition = Touchscreen.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Witch tempWitch = hit.collider.gameObject.GetComponent<Witch>();
            if (tempWitch != null)
            {
                tempWitch.Hit();
            }
        }
    }
}
