<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private ParticleSystem Particles;

    // Start is called before the first frame update
    void Start()
    {
        Particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Particles)
        {
            if (!Particles.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private ParticleSystem Particles;

    // Start is called before the first frame update
    void Start()
    {
        Particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Particles)
        {
            if (!Particles.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
>>>>>>> Stashed changes
