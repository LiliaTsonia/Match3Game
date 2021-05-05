using UnityEngine;

public enum Clip 
{ 
	Select, 
	Swap, 
	Clear 
};

public class SFXManager : MonoBehaviour {
	private AudioSource[] _sfxSounds;

	void Start () 
	{
		_sfxSounds = GetComponents<AudioSource>();
		Tile.OnSoundPlay += PlaySFX;
    }

	public void PlaySFX(Clip audioClip) {
		_sfxSounds[(int)audioClip].Play();
	}
}
