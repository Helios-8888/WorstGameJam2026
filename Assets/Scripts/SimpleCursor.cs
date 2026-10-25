using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SimpleCursor : MonoBehaviour
{
    [SerializeField]
    private InputActionReference pointerRef;
    private RectTransform _cursorTransform;
    private Canvas _canvas;
    private RectTransform _canvasTransform;
    private Camera _canvasCam;

    private void Awake()
    {
        //Cursor.visible = false;
        _cursorTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        if (_canvas != null)
        {
            _canvasTransform = _canvas.GetComponent<RectTransform>();
            _canvasCam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay 
                ? null 
                : _canvas.worldCamera;
        }
    }

    private void OnEnable()
    {
        pointerRef.action.performed += OnPointerPositionChanged;
    }
    private void OnDisable()
    {
        pointerRef.action.performed -= OnPointerPositionChanged;
    }
    // Update is called once per frame
    private void OnPointerPositionChanged(InputAction.CallbackContext context)
    {
        if (_cursorTransform != null && _canvas != null)
        {
            var mousePos = context.ReadValue<Vector2>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasTransform, mousePos, _canvasCam, out var  localPoint)) {
                _cursorTransform.anchoredPosition = localPoint;
            }
        }
    }
}
