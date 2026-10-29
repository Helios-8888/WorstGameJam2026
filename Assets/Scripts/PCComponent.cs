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

    public GameManager Gamemanager_;
    public ComponentType ComponentType_;
    public string ComponentName;
    public float moveSpeed = 10.0f;

    [SerializeField]
    private InputActionReference leftClickRef;
    [SerializeField]
    private InputActionReference rightClickRef;
    private Rigidbody2D rb;
    private bool bSelected = false;

    // Audio
    private AudioSource ComponentSource;
    public AudioClip[] AudioClips;
    public AudioClip selectionClip;
    public AudioClip attachmentClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bSelected = false;
        ComponentSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftClickRef.action.WasPerformedThisFrame())
        {
            Vector2 pos = Mouse.current.position.ReadValue();
            Vector2 direction = (pos - rb.position).normalized;
            if (bSelected)
            {
                MoveComponent(direction);
                ComponentSource.PlayOneShot(selectionClip);
                ComponentSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Length)]);
            }
        }
        if (rightClickRef.action.WasPerformedThisFrame())
        {
            ComponentSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Length)]);
            bSelected = false;
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


    public void OnSelectComponent()
    {
        bSelected = true;
    }
    public bool TryAttachComponent(BoardSlot _slot)
    {
        if (_slot.bAvailable)
        {
            _slot.AttachedComponent = this;
            _slot.bAvailable = false;
            Debug.Log($"Attached: {ComponentName} in slot {_slot.SlotID}");
            if (_slot.DesiredComponent == this.ComponentType_)
            {
                for (int i = 0; i< Gamemanager_.MissingComponents.Count; i++)
                {
                    if (_slot.DesiredComponent == Gamemanager_.MissingComponents[i])
                    {
                        _slot.bCorrectComponent = true;
                        Gamemanager_.MissingComponents.Remove(_slot.DesiredComponent);
                    }
                }
                ComponentSource.PlayOneShot(attachmentClip);
            }
            Gamemanager_.CheckSlots();
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
            _slot.bCorrectComponent = false;
            Gamemanager_.CheckSlots();
            return true;
        }
        else
        {
            Debug.Log($"Error. Slot {_slot.SlotID} is already empty");
            return false;
        }
    }

}
