using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera Camera;
    public Color GizmoColor;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Camera.MoveToTopOfPrioritySubqueue();
            
        }
    }

    private void OnDrawGizmos()
    {
        GizmoColor.a = 0.2f;
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, this.gameObject.GetComponent<BoxCollider>().bounds.extents*2);
        GizmoColor.a = 0.8f;
        Gizmos.color = GizmoColor;
        Gizmos.DrawSphere(Camera.transform.position, 1);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}
