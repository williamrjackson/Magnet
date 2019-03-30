using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particles = null;
    // Start is called before the first frame update

    private static BlackHole instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    public static void RegisterSink()
    {
        if (instance != null)
        {
            instance.particles.Play();
        }
    }
}
