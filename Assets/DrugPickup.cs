using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Pickupable Object that causes the drug effect
/// </summary>
public class DrugPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Movement_Normal>().m_Controller.isGrounded)
            {
                other.GetComponent<Movement_Normal>().CollectDrug(this.transform);
                StartCoroutine(DestroyDrug(other.GetComponent<Movement_Normal>()));
            }
        }
    }


    IEnumerator DestroyDrug(Movement_Normal player)
    {
        yield return new WaitForSeconds(0.83f);
        //player.pickedUp = true;
        this.gameObject.SetActive(false);
    }
}
