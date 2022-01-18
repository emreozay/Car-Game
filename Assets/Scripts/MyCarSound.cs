using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyCarSound : MonoBehaviour
{
    AudioSource audioSource;

    public float minPitch = 0.05f;
    public float maxPitch = 3f;
    private float pitchFromCar;

    public static UnityEvent soundOffEvent;
    public static UnityEvent soundOnEvent;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;

        if (soundOffEvent == null)
            soundOffEvent = new UnityEvent();

        soundOffEvent.AddListener(SoundOff);

        if (soundOnEvent == null)
            soundOnEvent = new UnityEvent();

        soundOnEvent.AddListener(SoundOn);
    }

    void Update()
    {
        pitchFromCar = CarController.carSpeed / 10;
        
        if (pitchFromCar < minPitch)
            audioSource.pitch = minPitch;
        else
            audioSource.pitch = pitchFromCar;
    }

    private void SoundOff()
    {
        audioSource.Stop();
    }

    private void SoundOn()
    {
        audioSource.Play();
    }
}
