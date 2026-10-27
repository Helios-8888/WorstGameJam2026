using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    //public PCComponent SelectedComponent;
    public List<BoardSlot> Slots = new List<BoardSlot>();

    public bool CheckSlots()
    {
        if (Slots.Count == 0)
            return false;
        foreach (var slot in Slots)
        {
            if (!slot.bCorrectComponent)
                return false;
        }
        return true;
    }

}
