using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHolder : MonoBehaviour
{
    CubeHover m_cube;
    public float m_pickuprange;
    public float m_carryrange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_cube != null)
        {
            if((m_cube.gameObject.transform.position - transform.position).magnitude > m_carryrange)
            {
                m_cube.ToggleFloating();
                m_cube = null;
            }
        }
    }

    void OnAction()
    {
        if(m_cube == null)
        {
            CubeHover[] objects = FindObjectsOfType<CubeHover>();
            if (objects.Length == 0) return;
            foreach (var item in objects)
            {
                if (m_cube == null && (item.gameObject.transform.position - transform.position).magnitude <= m_pickuprange)
                {
                    m_cube = item;
                }
                else if (m_cube == null) continue;
                else if ((item.gameObject.transform.position - transform.position).magnitude < (m_cube.gameObject.transform.position - transform.position).magnitude)
                {
                    m_cube = item;
                }
            }
            if(m_cube != null)
            {
                m_cube.ToggleFloating();
            }
        }
        else
        {
            m_cube.ToggleFloating();
            m_cube = null;
        }
    }
}
