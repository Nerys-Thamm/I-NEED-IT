using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHover : MonoBehaviour
{
    public float m_height = 5.0f;
    public float m_floatforce = 10.0f;

    public float m_playerseekforce = 5.0f;

    public bool m_isFloating = false;

    Rigidbody m_body;
    GameObject m_player;
    GameObject m_maincamera;
    ParticleSystem m_floatparticles;
    // Start is called before the first frame update
    void Start()
    {
        m_body = GetComponent<Rigidbody>();
        m_player = FindObjectOfType<Movement_Normal>().gameObject;
        m_maincamera = FindObjectOfType<Camera>().gameObject;
        m_floatparticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(transform.position, -transform.up,out hit, m_height) && m_isFloating)
        {
            m_body.AddForce(transform.up * (m_floatforce / (hit.distance/m_height)));
        }
        if(m_isFloating)
        {
            m_body.AddForce(((m_player.transform.position + (m_maincamera.transform.forward * 5)) - this.transform.position) * m_playerseekforce );
        }
    }

    public void ToggleFloating()
    {
        if (m_isFloating)
        {
            m_isFloating = false;
            m_floatparticles.Stop();
        }
        else
        {
            m_isFloating = true;
            m_floatparticles.Play();
        }
    }
}
