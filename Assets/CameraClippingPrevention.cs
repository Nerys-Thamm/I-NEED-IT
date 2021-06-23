using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClippingPrevention : MonoBehaviour
{
    public Transform m_thirdpersoncam;
    public Transform m_closecam;

    public float m_thirdpersondistance;
    public float m_closedistance;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        RaycastHit hit;
        //if (Physics.Raycast(m_thirdpersoncam.position + (m_thirdpersoncam.forward * 10), -m_thirdpersoncam.forward, out hit, 10.0f))
        //{
        //    m_thirdpersoncam.localPosition = m_thirdpersoncam.localPosition.normalized * (m_thirdpersondistance - (10-hit.distance));
        //}
        //else
        //{
        //    m_thirdpersoncam.localPosition = m_thirdpersoncam.localPosition.normalized * m_thirdpersondistance;
        //}
        //if (Physics.Raycast(m_closecam.position, -m_closecam.forward, out hit, 5.0f))
        //{
        //    m_closecam.localPosition = m_closecam.localPosition.normalized * (m_closedistance - hit.distance);
        //}
        //else
        //{
        //    m_closecam.localPosition = m_closecam.localPosition.normalized * m_closedistance;
        //}

    }
}
