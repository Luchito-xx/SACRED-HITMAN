using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControladorVolumen : MonoBehaviour
{
    [SerializeField] private AudioMixer globalAudioMixer;

    public void CambiarVolumen(float global_volumen)
    {
        globalAudioMixer.SetFloat("Volumen", global_volumen);
    }
}
