using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public BoxCollider TriggerBox;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Movement_Normal CharMotor = other.GetComponent<Movement_Normal>();

            CharMotor.Die();
        }
    }

    private void OnDrawGizmos()
    {
        Color test = Color.red;
        test.a = 0.5f;
        Gizmos.color = test;
        //Gizmos.DrawWireCube(transform.position + TriggerBox.center, TriggerBox.size);
        Gizmos.DrawCube(transform.position + TriggerBox.center, TriggerBox.size);
    }
}
