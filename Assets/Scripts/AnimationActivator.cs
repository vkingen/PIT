using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationActivator : MonoBehaviour
{
    Animator animator;
    [SerializeField] private string animationName = "IsOpen";
    private bool toggle = false;

    public UnityEvent onActivation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    
    public void TogglePlayAnimation()
    {
        toggle = !toggle;
        PlayAnimation(toggle);
    }

    public void PlayAnimation(bool state)
    {
        animator.SetBool(animationName, state);
        onActivation.Invoke();
    }
}
