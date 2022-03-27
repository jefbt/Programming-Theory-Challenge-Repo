using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadVolume : MonoBehaviour
{
    public string volumeObjectName;

    private void Awake()
    {
        float volume = GameObject.Find(volumeObjectName).GetComponent<AudioSource>().volume;
        GetComponent<Slider>().value = volume;
    }
}
