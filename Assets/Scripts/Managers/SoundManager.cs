using UnityEngine;
using System.Collections.Generic;

public enum SoundClip
{
    Select,
    Swap,
    Clear,
    Hyperfun
};

/// <summary>
/// Try to move soundmanager (and gamemanager???) to DI project context
/// Try addressables for sound clips
/// </summary>

public class SoundManager : MonoBehaviour, IPlayable
{
    [SerializeField] private AudioSource[] _audioSources; //0 : SFX_Master, 1 : Music_Master
    [SerializeField] private AudioData[] _audioDataObjects;

    void Start()
    {
        TileController.OnSoundPlay += PlaySound;
    }

    public void PlaySound(SoundClip clipName, AudioType audioSourceType)
    {
        int index = (int)audioSourceType;
        AudioSource audioSource = _audioSources[index];

        if(IsAudioSlotFree(ref audioSource, clipName))
        {
            audioSource.clip = _audioDataObjects[index].GetAudioClip(clipName);
        }

        audioSource.Play();
    }

    public bool IsAudioSlotFree(ref AudioSource audioSource, SoundClip clipType)
    {
        return audioSource.clip == null || audioSource.clip.name != clipType.ToString();
    }
}
