using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioTrigger : MonoBehaviour
{
    public UnityEvent m_OnTrigger;

    public float m_waittime;

    float m_currentwaittime;

    bool m_CanBeTriggered = true;

    bool m_played = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_currentwaittime -= Time.deltaTime;
        if(m_currentwaittime <= 0 && !m_played)
        {
            m_OnTrigger.Invoke();
            m_played = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_CanBeTriggered)
        {
            m_currentwaittime = m_waittime;
            m_CanBeTriggered = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, GetComponent<BoxCollider>().extents);
    }
}
