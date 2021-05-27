using UnityEngine;

public interface IPlayable
{
    void PlaySound(SoundClip clipName, AudioType audioSourceType);
    bool IsAudioSlotFree(ref AudioSource audioSource, SoundClip clipType);
}

