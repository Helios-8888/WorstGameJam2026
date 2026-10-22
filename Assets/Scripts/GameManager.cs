using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PCComponent SelectedComponent;
    private Vector2 leftPos = Vector2.zero;
    private Vector2 rightPos = Vector2.zero;
    //OnSelect


    //OnDeSelect
    // Try Attach


    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            leftPos = Input.mousePosition;
            if (leftPos.magnitude > 0.001f && rightPos.magnitude > 0.001f)
            {
                Vector2 moveDir = (leftPos - rightPos).normalized;
                if (SelectedComponent != null)
                {
                    SelectedComponent.MoveComponent(moveDir);
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            rightPos = Input.mousePosition;
            if (leftPos.magnitude > 0.001f && rightPos.magnitude > 0.001f)
            {
                Vector2 moveDir = (leftPos - rightPos).normalized;
                if (SelectedComponent != null)
                {
                    SelectedComponent.MoveComponent(moveDir);
                }
            }
        }
    }
}
