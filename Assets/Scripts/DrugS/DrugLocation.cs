using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugLocation : MonoBehaviour
{
    public DrugPickup Drug;

    public void RespawnDrug()
    {
        Drug.gameObject.SetActive(true);
    }
}
