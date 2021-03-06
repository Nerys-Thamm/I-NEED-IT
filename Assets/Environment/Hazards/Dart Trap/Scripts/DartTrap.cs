using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    public GameObject m_ProjectilePrefab;
    public float m_ProjectileSpeed;
    public float m_AutoFireDelay;
    public float m_AutoFireStartDelay;
    public bool m_AutoFireEnabled;

    public AudioSource audioSource;
    public AudioClip ac_Fire;

    float m_AutoFireTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_AutoFireTimer = m_AutoFireDelay + m_AutoFireStartDelay; ;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AutoFireEnabled)
        {
            m_AutoFireTimer -= Time.deltaTime;
            if(m_AutoFireTimer < 0)
            {
                m_AutoFireTimer = m_AutoFireDelay;
                FireProjectile();
            }
        }

    }

    public void FireProjectile()
    {
        audioSource.PlayOneShot(ac_Fire);
        GameObject newProjectile = GameObject.Instantiate(m_ProjectilePrefab, this.transform.position, this.transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(this.transform.up * m_ProjectileSpeed, ForceMode.Impulse);
    }

    public void SetAutoFire(bool _val)
    {
        m_AutoFireEnabled = _val;
    }
}
