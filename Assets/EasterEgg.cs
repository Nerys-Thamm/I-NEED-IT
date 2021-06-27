using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public AudioSource m_player;
    public AudioClip m_clip;
    public GameObject m_model;
    public bool m_isenabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEasterEgg()
    {
        if (m_isenabled)
        {
            m_player.Stop();
            m_player.volume = 1.0f;
            m_model.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            m_isenabled = false;
        }
        else
        {
            m_player.clip = m_clip;
            m_player.volume = 0.1f;
            m_player.Play();
            m_model.transform.localScale = new Vector3(2.0f, 0.7f, 0.7f);
            m_isenabled = true;
        }
    }

}
