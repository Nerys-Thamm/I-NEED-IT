using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string NextSceneName;
    public BoxCollider TriggerBox;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(NextSceneName);
    }

    private void OnDrawGizmos()
    {
        Color test = Color.black;
        test.a = 0.5f;
        Gizmos.color = test;
        Gizmos.DrawCube(transform.position + TriggerBox.center, TriggerBox.size);
    }
}
