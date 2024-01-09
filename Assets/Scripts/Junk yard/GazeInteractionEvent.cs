using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeInteractionEvent : MonoBehaviour
{
    public UnityEvent onSelected;
    public UnityEvent onDeselected;

    public void Selected()
    {
        onSelected.Invoke();
    }

    public void Deselected()
    {
        onDeselected.Invoke();
    }


    /*
    Interaction flow:
    - 
     


    What is the application about: (Ideas)
    - Spell casting game
    - Escape Room / Puzzle game
    - DJ game
    - Train conductor
    - Sapce ship pilot.
        - Control speed with left hand
        - Control Orientation with right hand
        - Shoot the cannon with left hand button
        - With voice commands you can:
            - Reload cannon
            - Select menus (maybe)
        - 
     */
}
