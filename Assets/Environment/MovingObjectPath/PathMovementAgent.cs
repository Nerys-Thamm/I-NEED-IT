using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathMovementAgent : MonoBehaviour
{
    public MovementPath m_Path;
    public bool m_AlwaysMoving = true;
    bool m_IsMoving;
    public UnityEvent m_OnNodeReach;
    public float m_Speed;
    public float m_distancetonextnode;
    Vector3 m_direction;
    MovementPath.PathNode m_currentnode;
    MovementPath.PathNode m_nextnode;
    // Start is called before the first frame update
    void Start()
    {
        m_currentnode = m_Path.GetNode();
        this.gameObject.transform.position = m_currentnode.Position;
        m_nextnode = m_Path.GetNextNode();
        m_direction = (m_nextnode.Position - this.gameObject.transform.position);
        m_distancetonextnode = m_direction.magnitude;
        m_IsMoving = m_AlwaysMoving;
    }

    // Update is called once per frame
    void Update()
    {
        m_direction = (m_nextnode.Position - this.gameObject.transform.position);
        m_distancetonextnode = m_direction.magnitude;
        if (m_distancetonextnode <= 0.05)
        {
            m_currentnode = m_nextnode;
            m_nextnode = m_Path.GetNextNode();
            m_direction = (m_nextnode.Position - this.gameObject.transform.position);
            m_distancetonextnode = m_direction.magnitude;
            m_OnNodeReach.Invoke();
        }
        if (m_IsMoving)
        {
            this.gameObject.transform.position += m_direction.normalized * (m_Speed * m_nextnode.SpeedCurve.Evaluate(1 / m_distancetonextnode)) * Time.deltaTime;
        }
    }

    public void SetMoving(bool _IsMoving)
    {
        m_IsMoving = _IsMoving;
    }

}
