using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNarration : MonoBehaviour
{

    public AudioSource m_source;

    public AudioClip m_drugclip, m_nodrugclip;

    public float m_delay;

    PersistentSceneData m_data;

    float m_timer = 0;

    bool m_playing = false;


    // Start is called before the first frame update
    void Start()
    {
        m_data = FindObjectOfType<PersistentSceneData>();
        if (m_data.GetDrugs() == 0)
        {
            m_source.clip = m_nodrugclip;
        }
        else
        {
            m_source.clip = m_drugclip;
        }

        m_timer = m_delay;
    }

    // Update is called once per frame
    void Update()
    {
        m_delay -= Time.deltaTime;
        if (m_delay < 0 && !m_playing)
        {
            m_playing = true;
            m_source.Play();
        }
    }
}
