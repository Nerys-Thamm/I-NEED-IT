using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementPath : MonoBehaviour
{

    public Color m_PathColor;
    public PathType m_Mode;
    bool m_returning = false;
    int m_index = 0;
    public enum PathType
    {
        CIRCUIT,
        PING_PONG,
        SINGLE,
    }

    [System.Serializable]
    public class PathNode
    {
        public Vector3 Position;
        public AnimationCurve SpeedCurve;
        public PathNode(Vector3 _prevpos)
        {
            Position = _prevpos;
            SpeedCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    [HideInInspector]
    public List<PathNode> m_Path;

    private void OnDrawGizmos()
    {
        Gizmos.color = m_PathColor;
        foreach (var node in m_Path)
        {
            Gizmos.DrawCube(node.Position, Vector3.one *0.5f);
        }
    }

    public PathNode GetNode()
    {
        return m_Path[m_index];
    }
    public PathNode GetNextNode()
    {
        switch (m_Mode)
        {
            case PathType.CIRCUIT:
                if (m_index == m_Path.Count - 1)
                {
                    m_index = 0;
                    return m_Path[0];
                }
                else
                {
                    return m_Path[++m_index];
                }
                
            case PathType.PING_PONG:
                if (m_index == m_Path.Count - 1)
                {
                    m_returning = true;
                    return m_Path[--m_index];
                }
                if (m_index == 0)
                {
                    m_returning = false;
                    return m_Path[++m_index];
                }
                if(m_returning)
                {
                    return m_Path[--m_index];
                }
                else
                {
                    return m_Path[++m_index];
                }
                
            case PathType.SINGLE:
                if (m_index == m_Path.Count-1)
                {
                    return m_Path[m_index];
                }
                else
                {
                    return m_Path[++m_index];
                }
            default:
                //This should never happen
                break;
        }
        //This should also never happen
        return m_Path[m_index];
    }
}
