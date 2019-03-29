using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float range = 3f;
    [SerializeField]
    float pull = 1f;

    public TouchAxisCtrl touch;
    private List<Attractor> attractors = new List<Attractor>();

    private static Magnet instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public static void IntroduceAttractor(Attractor attractor)
    {
        instance.attractors.Add(attractor);
    }
    public static void RemoveAttractor(Attractor attractor)
    {
        instance.attractors.Remove(attractor);
    }

    void Update()
    {
        Vector2 input = touch.GetAxis();
        Vector3 adjustedInput = new Vector3(input.x, 0, input.y) * speed;
        transform.position += adjustedInput;
        foreach (Attractor attractor in attractors)
        {
            float distance = Vector3.Distance(transform.position, attractor.transform.position);
            if (distance < range)
            {
                attractor.transform.position += (transform.position - attractor.transform.position).normalized * distance.Remap(0, range, pull, 0);
            }
        }
    }
}
