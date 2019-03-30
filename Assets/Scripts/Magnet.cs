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
    [SerializeField]
    float rotationSpeed = 1f;
    [SerializeField]
    ParticleSystem particles = null;

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
            lastPos = transform.position;
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

    Vector3 lastPos;
    void Update()
    {
        Vector2 input = touch.GetAxis();
        Vector3 adjustedInput = new Vector3(input.x, 0, input.y) * speed;
        transform.position += adjustedInput;
        Attractor nearestAttractor = null;
        float nearestDistance = 10f;
        foreach (Attractor attractor in attractors)
        {
            float distance = Vector3.Distance(transform.position, attractor.transform.position);
            if (distance < range)
            {
                attractor.transform.position += (transform.position - attractor.transform.position).normalized * distance.Remap(0, range, pull, pull * .5f) * Time.deltaTime;
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestAttractor = attractor;
                }
            }
        }
        if (nearestAttractor != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nearestAttractor.transform.position - transform.position, Vector3.up), Time.deltaTime * rotationSpeed);
            if (particles != null && !particles.isPlaying)
            {
                particles.Play();
            }
        }
        else if (Vector3.Distance(lastPos, transform.position) > .05f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - lastPos, Vector3.up), Time.deltaTime * rotationSpeed);
            if (particles != null && particles.isPlaying)
            {
                particles.Stop();
            }
        }
        lastPos = transform.position;
    }
}
