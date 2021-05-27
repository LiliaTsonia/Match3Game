using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    SFX_Master,
    Music_Master
}

[CreateAssetMenu(fileName ="New AudioData", menuName ="ScriptableObject")]
public class AudioData : ScriptableObject
{
    public AudioType AudioType;
    public List<AudioClip> AudioClips;

    public AudioClip GetAudioClip(SoundClip clipType)
    {
        return AudioClips.Find(c => c.name == clipType.ToString());
    }
}
