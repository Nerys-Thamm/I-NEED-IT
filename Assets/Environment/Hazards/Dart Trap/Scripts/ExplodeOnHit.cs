using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHit : MonoBehaviour
{
    public GameObject m_ExplosionPrefab;

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
        if (other.isTrigger && !other.tag.Equals("PlayerHitBox")) return;  
        

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //TODO: Code for interaction with entities when hit
        //-------------------------------------------------
        if (other.tag.Equals("PlayerHitBox"))
        {
            Movement_Normal player = other.GetComponentInParent<Movement_Normal>();
            if (!player.HasDied)
            {
                player.Die();
            }
            else
            {
                return;
            }
        }

        //-------------------------------------------------
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        GameObject.Instantiate(m_ExplosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
