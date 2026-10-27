using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
public class PCComponent : MonoBehaviour
{
    public enum ComponentType
    {
        GPU,
        CPU,
        SSD,
        HDD,
        USB,
        MOBO,
        Screw,
        Wifi,
        DVD,
        Glowstick,
        ROM,
        RAM,
        Power,
        Fan,
        Thermal,
        Cable
    }

    public ComponentType ComponentType_;
    public string ComponentName;
    public float moveSpeed = 100.0f;

    [SerializeField]
    private InputActionReference clickRef;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickRef.action.WasPerformedThisFrame())
        {
            Vector2 pos = Mouse.current.position.ReadValue();
            Vector2 direction = (pos - rb.position).normalized;
            MoveComponent(direction);
        }
    }


    public void MoveComponent(Vector2 direction)
    {
        Debug.Log(direction.x + direction.y);
        rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("BoardSlot") && collision.TryGetComponent(out BoardSlot _slot))
        {
            TryAttachComponent(_slot);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("BoardSlot") && collision.TryGetComponent(out BoardSlot _slot))
        {
            TryDetachComponent(_slot);
        }
    }

    public bool TryAttachComponent(BoardSlot _slot)
    {
        if (_slot.bAvailable)
        {
            _slot.AttachedComponent = this;
            _slot.bAvailable = false;
            Debug.Log($"Attached: {ComponentName} in slot {_slot.SlotID}");
            return true;
        }
        else
        {
            Debug.Log($"Could not attach. Slot {_slot.SlotID} already has {_slot.AttachedComponent.ComponentName} attached");
            return false;
        }
    }

    public bool TryDetachComponent(BoardSlot _slot)
    {
        if (!_slot.bAvailable)
        {
            _slot.AttachedComponent = null;
            _slot.bAvailable = true;
            Debug.Log($"Detached: {ComponentName} in slot {_slot.SlotID}");
            return true;
        }
        else
        {
            Debug.Log($"Error. Slot {_slot.SlotID} is already empty");
            return false;
        }
    }

}
