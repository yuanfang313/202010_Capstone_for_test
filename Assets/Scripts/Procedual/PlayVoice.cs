using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayVoice : MonoBehaviour
{
    public static AudioSource voiceAudioSource;

    public List<AudioClip> voiceClips_1 = new List<AudioClip>();
    public List<AudioClip> voiceClips_2 = new List<AudioClip>();
    public List<AudioClip> voiceClips_3 = new List<AudioClip>();

    private void Start()
    {
        voiceAudioSource = GetComponent<AudioSource>();
    }

    public void playVoice_1 (int index)
    {
        voiceAudioSource.PlayOneShot(voiceClips_1[index]);
    }

    public void playVoice_2(int index)
    {
        voiceAudioSource.PlayOneShot(voiceClips_2[index]);
    }

    public void playVoice_3(int index)
    {
        voiceAudioSource.PlayOneShot(voiceClips_3[index]);
    }

}
