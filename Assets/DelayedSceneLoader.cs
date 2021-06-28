using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedSceneLoader : MonoBehaviour
{

    public float m_delay = 0;
    public string m_levelname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_delay -= Time.deltaTime;
        if (m_delay < 0)
        {
            SceneManager.LoadScene(m_levelname);
        }
    }
}
