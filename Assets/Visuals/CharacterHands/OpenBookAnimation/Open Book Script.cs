using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenBookScript : MonoBehaviour
{
    public Animator animator;              
    public GameObject animatedObject;     

    private string animationName = "Open Book";
    private bool isActive = false;
    private Keyboard keyboard;

    void Start()
    {
        keyboard = Keyboard.current;
    }

    void Update()
    {
        if (keyboard != null && keyboard.leftShiftKey.wasPressedThisFrame)
        {
            if (!isActive)
            {
                StartCoroutine(ActivateAndPlay());
            }
            else
            {
                if (animatedObject != null)
                    animatedObject.SetActive(false);

                isActive = false;
            }
        }
    }

    IEnumerator ActivateAndPlay()
    {
        if (animatedObject != null)
            animatedObject.SetActive(true);

        
        yield return null;

        if (animator != null)
            animator.Play(animationName, 0, 0f);

        isActive = true;
    }
}
