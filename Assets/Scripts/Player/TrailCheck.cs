using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCheck : MonoBehaviour
{
    public float angle = 0.15f;
    public Transform car;
    public AudioSource audioSource;
    public ParticleSystem[] trails = new ParticleSystem[4];

    bool playing = false;

    void FixedUpdate()
    {
        if (Mathf.Abs(car.rotation.z) > angle && !playing)
        {
            foreach (ParticleSystem ps in trails)
            {
                var main = ps.velocityOverLifetime;
                main.y = -500 * RoadMovement.Speed * Time.deltaTime;
                ps.Play();
            }
            playing = true;
            audioSource.Play();
        }
        else if(Mathf.Abs(car.rotation.z) < angle)
        {
            foreach (ParticleSystem ps in trails)
            {
                ps.Stop();
            }
            audioSource.Stop();
            playing = false;
        }
    }
}
