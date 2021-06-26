using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateTrigger : MonoBehaviour
{
    public enum DetectedObj
    {
        PLAYER,
        CUBE,
    };
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    public DetectedObj m_ObjectToDetect;
    public bool m_SingleUse = false;

    public Color m_UnpressedColor;
    public Color m_PressedColor;
    public SpriteRenderer m_Sprite;
    int m_numofobj = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Sprite != null)
        {
            if (m_numofobj > 0)
            {
                m_Sprite.color = m_PressedColor;
            }
            else
            {
                m_Sprite.color = m_UnpressedColor;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        if (other.tag != "Player" && m_ObjectToDetect == DetectedObj.PLAYER) return;
        if (other.tag != "PuzzleCube" && m_ObjectToDetect == DetectedObj.CUBE) return;
        
        if (m_numofobj == 0)
        {
            OnPress.Invoke();
        }
        m_numofobj++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) return;
        if (other.tag != "Player" && m_ObjectToDetect == DetectedObj.PLAYER) return;
        if (other.tag != "PuzzleCube" && m_ObjectToDetect == DetectedObj.CUBE) return;
        m_numofobj--;
        if (m_numofobj == 0 && !m_SingleUse)
        {
            OnRelease.Invoke();
        }
    }
}
