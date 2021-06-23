using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TripleButtonDoor : MonoBehaviour
{
    public bool m_buttonone;
    public bool m_buttontwo;
    public bool m_buttonthree;

    public bool m_opened;
    public PathMovementAgent m_leftdoor, m_rightdoor;

    public UnityEvent OnDoorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_opened && m_buttonone && m_buttontwo && m_buttonthree)
        {
            m_opened = true;
            m_leftdoor.SetMoving(true);
            m_rightdoor.SetMoving(true);
            OnDoorOpen.Invoke();
        }
        else if(m_opened && !(m_buttonone && m_buttontwo && m_buttonthree))
        {
            m_opened = false;
            m_leftdoor.SetMoving(true);
            m_rightdoor.SetMoving(true);
            OnDoorOpen.Invoke();
        }
    }

    public void ToggleButton(int _index)
    {
        switch (_index)
        {
            case 1:
                m_buttonone = !m_buttonone;
                break;
            case 2:
                m_buttontwo = !m_buttontwo;
                break;
            case 3:
                m_buttonthree = !m_buttonthree;
                break;
            default:
                break;
        }
    }
}
