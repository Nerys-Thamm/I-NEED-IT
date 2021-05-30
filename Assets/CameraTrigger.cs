using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera Camera;
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Camera.MoveToTopOfPrioritySubqueue();
            //input.SwitchCurrentActionMap(actionmapname);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, this.gameObject.GetComponent<BoxCollider>().bounds.extents*2);
           
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}
