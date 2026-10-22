using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameManager GameManager;
    public List<PCBit> BitsToAttach = new List<PCBit>();
    public ComponentType ComponentType_;
    public string ComponentName;
    private Rigidbody2D rb;
    private float moveSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.up * moveSpeed, ForceMode2D.Force);
    }


    public bool TryAttachComponent()
    {
        int numAttachedBits = 0;
        foreach (PCBit bit in BitsToAttach)
        {
            if (bit.bAttached)
            {
                numAttachedBits++;
            }

        }

        if (numAttachedBits < BitsToAttach.Count)
        {
            // Component not fully attached
            // Play error sound effect
            return false;
        }

        else
        {
            //Component fully attached
            //Play attached sound effect
            return true;
        }
    }

    public void OnComponentClick()
    {
        GameManager.SelectedComponent = this;

    }

    public void MoveComponent(Vector2 direction)
    {

    }
    


}
