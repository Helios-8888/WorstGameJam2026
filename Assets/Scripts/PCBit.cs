using UnityEngine;

public class PCBit : MonoBehaviour
{
    public PCComponent Owner;
    public bool bAttached = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("BoardSlot") && collision.TryGetComponent(out BoardSlot _slot))
        {
            TryAttachBit(_slot);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("BoardSlot") && collision.TryGetComponent(out BoardSlot _slot))
        {
            TryDetachBit(_slot);
        }
    }

    public bool TryAttachBit(BoardSlot _slot)
    {
        if (_slot.bAvailable)
        {
            _slot.AttachedComponent = Owner;
            _slot.bAvailable = false;
            Debug.Log($"Attached: {Owner.ComponentName} in slot {_slot.SlotID}");
            return true;
        }
        else
        {
            Debug.Log($"Could not attach. Slot {_slot.SlotID} already has {_slot.AttachedComponent.ComponentName} attached");
            return false;
        }
    }

    public bool TryDetachBit(BoardSlot _slot)
    {
        if (!_slot.bAvailable)
        {
            _slot.AttachedComponent = null;
            _slot.bAvailable = true;
            Debug.Log($"Detached: {Owner.ComponentName} in slot {_slot.SlotID}");
            return true;
        }
        else
        {
            Debug.Log($"Error. Slot {_slot.SlotID} already is already empty");
            return false;
        }
    }
}
