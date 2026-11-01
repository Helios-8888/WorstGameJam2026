using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    //public PCComponent SelectedComponent;
    public List<BoardSlot> Slots = new List<BoardSlot>();
    public List<PCComponent.ComponentType> MissingComponents = new List<PCComponent.ComponentType>();
    private void Start()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            MissingComponents.Add(Slots[i].DesiredComponent);
        }

    }

    public bool CheckSlots()
    {
        if (MissingComponents.Count == 0 && Slots.Count>0) 
            return true;
        else return false;

    }

}
