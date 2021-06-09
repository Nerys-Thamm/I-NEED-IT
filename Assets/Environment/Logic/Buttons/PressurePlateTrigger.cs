using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateTrigger : MonoBehaviour
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPress.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnRelease.Invoke();
    }
}
