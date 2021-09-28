using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rain: MonoBehaviour
{

    [SerializeField] AnimationCurve curve;
    
    [SerializeField] AudioSource audios;
    [SerializeField] float radius;
    [SerializeField] float rateovertime;
    [SerializeField] float maxvolume;
    float t = 0;
    float tiempo;
    [SerializeField] float duration;
    ParticleSystem ps;
    ParticleSystem.MainModule main;
    ParticleSystem.EmissionModule emission;
    ParticleSystem.ShapeModule shape;
    ParticleSystem.ColorOverLifetimeModule gradient;
    [SerializeField]Gradient grad;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
        shape = ps.shape;
        emission = ps.emission;
        gradient = ps.colorOverLifetime;

    }
    void Update()
    {

        tiempo += Time.deltaTime;
        gradient.color = grad;
        emission.rateOverTime = rateovertime * curve.Evaluate(tiempo / duration);
        audios.volume= maxvolume *curve.Evaluate(tiempo / duration);

    }
}

