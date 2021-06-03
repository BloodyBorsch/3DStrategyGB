using UnityEngine;
using Abstractions;
using UnityEngine.EventSystems;


namespace InputSystem
{
    public class InputController : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField] private SelectedItem _currentSelection;
        [SerializeField] private Vector3Value _currentGroundPosition;

        protected void Awake()
        {
            _mainCamera = Camera.main;
        }

        protected void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hitInfo;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    var selectableItem = hitInfo.collider.GetComponent<ISelectableItem>();
                    _currentSelection.SetValue(selectableItem);
                }
                else _currentSelection.SetValue(null);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    _currentGroundPosition.SetValue(hitInfo.point);
                }                
            }
        }
    }
}
