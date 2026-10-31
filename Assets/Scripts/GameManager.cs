using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Stretch goals
    // speed running timer


    //public PCComponent SelectedComponent;
    public List<BoardSlot> Slots = new List<BoardSlot>();
    public List<PCComponent.ComponentType> MissingComponents = new List<PCComponent.ComponentType>();
    
    //Winning and Losing
    public AudioSource GameAudioSource;
    public AudioClip WinClip;
    public AudioClip LoseClip;

    public RectTransform WinTransform;
    public RectTransform LoseTransform;
    public Button BackButton;
    private void Start()
    {
        GameAudioSource = GetComponent<AudioSource>();

        WinTransform.gameObject.SetActive(false);
        LoseTransform.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);


        for (int i = 0; i < Slots.Count; i++)
        {
            MissingComponents.Add(Slots[i].DesiredComponent);
        }
    }

    public void TestPC()
    {
        if (MissingComponents.Count == 0 && Slots.Count > 0)
        {
            GameAudioSource.PlayOneShot(WinClip);
            WinTransform.gameObject.SetActive(true);
            // Play some dialogue
            // Enable a back to main menu button
            BackButton.gameObject.SetActive(false);
        }
        else
        {
            GameAudioSource.PlayOneShot(LoseClip);
            LoseTransform.gameObject.SetActive(true);
            // Play some dialogue
            // Enable a back to main menu button
            BackButton.gameObject.SetActive(false);

        }

    }

}
