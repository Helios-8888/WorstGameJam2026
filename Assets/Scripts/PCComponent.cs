using UnityEngine;
using System.Collections.Generic;
public class PCComponent : MonoBehaviour
{
    public List<PCBit> BitsToAttach = new List<PCBit>();
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
}
