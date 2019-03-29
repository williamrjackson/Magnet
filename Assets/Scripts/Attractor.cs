using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    void Start()
    {
        Magnet.IntroduceAttractor(this);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Magnet>())
        {
            Destroy(col.gameObject);
            Debug.Log("GameOver");
        }
        else
        {
            Magnet.RemoveAttractor(this);
            Destroy(gameObject);
        }
    }

}
