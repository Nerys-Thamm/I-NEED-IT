using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(MovementPath))]
public class MovementPathEditor : Editor
{

    MovementPath path;
    SerializedProperty nodes;
    public ReorderableList list;


    private void OnEnable()
    {
        path = (MovementPath)target;
        nodes = serializedObject.FindProperty("m_Path");
        list = new ReorderableList(serializedObject, nodes, true, true, true, true);
        list.drawElementCallback += DrawElement;
        list.drawHeaderCallback = DrawHeader;
        list.index = path.m_Path.Count-1;
        list.onAddCallback = param =>
        {
            if(param.index < 0 || param.index >= path.m_Path.Count)
            {
                param.index = path.m_Path.Count - 1;
            }
            if (path.m_Path.Count == 0 || param.index == 0)
            {
                path.m_Path.Add(new MovementPath.PathNode(path.gameObject.transform.position));
            }
            else
            {
                path.m_Path.Add(new MovementPath.PathNode(path.m_Path[param.index].Position));
            }
        };
        list.onRemoveCallback = param =>
        {
            if(path.m_Path.Count != 0)
            {
                path.m_Path.RemoveAt(param.index);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }


    void DrawHeader(Rect rect)
    {
        string name = "Nodes";
        EditorGUI.LabelField(rect, name);
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.LabelField(new Rect(rect.x + 160, rect.y, 100, EditorGUIUtility.singleLineHeight), "Speed Curve");

        EditorGUI.PropertyField(
        new Rect(rect.x + 250, rect.y, 150, EditorGUIUtility.singleLineHeight),
        element.FindPropertyRelative("SpeedCurve"),
        GUIContent.none
        );
        EditorGUI.PropertyField(
        new Rect(rect.x, rect.y, 150, EditorGUIUtility.singleLineHeight),
        element.FindPropertyRelative("Position"),
        GUIContent.none
        );

    }

    [System.Obsolete]
    private void OnSceneGUI()
    {
        Handles.color = path.m_PathColor;
        if (path.m_Path.Count != 0)
        {
            for (int i = 0; i < path.m_Path.Count; i++)
            {
                path.m_Path[i].Position = Handles.PositionHandle(path.m_Path[i].Position, Quaternion.identity);
                if (i < path.m_Path.Count - 1)
                {
                    Vector3 pathvector = path.m_Path[i + 1].Position - path.m_Path[i].Position;
                    Handles.DrawLine(path.m_Path[i].Position, path.m_Path[i + 1].Position);
                    if (pathvector.magnitude > 0)
                    {
                        Handles.ArrowCap(i, path.m_Path[i].Position, Quaternion.LookRotation(pathvector, Vector3.up), pathvector.magnitude / 2);
                    }
                    


                }
                else if(i == path.m_Path.Count - 1 && path.m_Mode == MovementPath.PathType.CIRCUIT)
                {
                    Vector3 pathvector = path.m_Path[0].Position - path.m_Path[i].Position;
                    Handles.DrawLine(path.m_Path[i].Position, path.m_Path[0].Position);
                    if (pathvector.magnitude > 0)
                    {
                        Handles.ArrowCap(i, path.m_Path[i].Position, Quaternion.LookRotation(pathvector, Vector3.up), pathvector.magnitude / 2);
                    }
                }

            }
        }
        

    }
}
