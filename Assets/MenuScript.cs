using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuScript : MonoBehaviour
{
    public GameObject MenuStuff;
    public Animator PlayerAnimator;
    public GameObject AudioTrigger;
    public GameObject canvas;
    public PlayerInput Input;

    private void Awake()
    { 
        canvas.SetActive(true);
        PlayerAnimator.speed = 0.0f;
        Input.SwitchCurrentActionMap("MainMenu");
    }

    public void OnPlay()
    {
        Debug.Log("AAA");
        PlayerAnimator.speed = 1.0f;
        AudioTrigger.SetActive(true);
        MenuStuff.SetActive(false);
        Input.SwitchCurrentActionMap("Movement");
    }

    private void OnQuit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
