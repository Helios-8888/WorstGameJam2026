using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, WorstInput.IW9rstActions
{
    public GameManager GameManager_;
    public Vector2 ComponentOrigin;
    public Vector2 Destinatinon;

    public void OnSelectLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 tempPos = Mouse.current.position.ReadValue();
            Debug.Log(tempPos);
        }
        
    }

    public void OnSelectRight(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
