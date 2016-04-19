using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    /// <summary> Types of sound that can be called </summary>
    public enum SoundType { Music, SFX, System }
    [Header("Sound & Music Lists")]
    /// <summary> List of musics in the game </summary>
    [SerializeField]
    private List<AudioClip> m_Music_List = new List<AudioClip>();
    /// <summary> list of Sound FX in the game </summary>
    [SerializeField]
    private List<AudioClip> m_SFX_List = new List<AudioClip>();
    /// <summary> list of system sounds </summary>
    [SerializeField]
    private List<AudioClip> m_System_List = new List<AudioClip>();
    [Header("Audio Sources")]
    /// <summary> Music audio source </summary>
	public AudioSource Music_Source;
    /// <summary> SFX audio source </summary>
	public AudioSource SFX_Source;
    /// <summary> System audio source </summary>
	public AudioSource System_Source;
    /// <summary> Mute Boolean </summary>
    [HideInInspector]
	public bool Mute;
	/// <summary> Time it takes for music to fad in and out </summary>
	[SerializeField]
	private float m_FadeTime = 0.5f;
	/// <summary> The Timer used to account for the fade </summary>
	private float m_Timer;
    /// <summary> Speed at which the music speeds up </summary>
    [SerializeField]
    private float m_SpeedUpSpeed = 0.1f;

    /// <summary> Finds sound and plays it based on type and string identifier </summary>
    /// <param name="type"> Type of soundmixer to use </param>
    /// <param name="sound"> Name of sound </param>
    public void PlaySound (SoundType type, string sound)
    {
        switch (type)
        {
        case SoundType.Music:
            Music_Source.clip = FindSound(m_Music_List, sound);
			if (!Mute)
			{
				Music_Source.Play();
			}
			break;
        case SoundType.SFX:
			if (!Mute)
			{
				SFX_Source.PlayOneShot(FindSound(m_SFX_List, sound));
			}	
			break;
        case SoundType.System:
			if (!Mute)
			{
            	System_Source.PlayOneShot(FindSound(m_System_List, sound));
			}
            break;
        }
	}

	/// <summary> Finds sound and plays it based on type and give sound file </summary>
	/// <param name="type"> Type of soundmixer to use </param>
	/// <param name="sound"> an audio clip to play </param>
	public void PlaySound (SoundType type, AudioClip sound)
	{
		switch (type)
		{
		case SoundType.Music:
			Music_Source.clip = sound;
			if (!Mute)
			{
				Music_Source.Play();
			}
			break;
		case SoundType.SFX:
			if (!Mute)
			{
				SFX_Source.PlayOneShot(sound);
			}
			break;
		case SoundType.System:
			if (!Mute)
			{
				System_Source.PlayOneShot(sound);
			}
			break;
		}
	}

    /// <summary> Finds sound and plays it based on type and string identifier </summary>
    /// <param name="type"> Type of soundmixer to use </param>
    /// <param name="sound"> Name of sound </param>
    /// <param name="pos"> Position of sound </param>>
    public void PlaySound(SoundType type, string sound, float volume)
    {
        switch (type)
        {
            case SoundType.Music:
                Music_Source.clip = FindSound(m_Music_List, sound);
                if (!Mute)
                {
                    Music_Source.Play();
                }
                break;
            case SoundType.SFX:
                if (!Mute)
                {
                    SFX_Source.PlayOneShot(FindSound(m_SFX_List, sound), volume);
                }
                break;
            case SoundType.System:
                if (!Mute)
                {
                    System_Source.PlayOneShot(FindSound(m_System_List, sound));
                }
                break;
        }
    }

    /// <summary> Finds all sounds strting with the string and plays a random one </summary>
    /// <param name="type"> Type of sound we are playing </param>
    /// <param name="sound"> String that we will find all of </param>
    public float PlaySoundRandom (SoundType type, string sound)
	{
        float length = 0;
		if (!Mute)
		{
			List<AudioClip> clips = new List<AudioClip>();
			int rando;
			switch (type)
			{
			case SoundType.SFX:
				foreach(AudioClip clip in m_SFX_List)
				{
					bool match = true;
					if(!clip.name.Contains(sound))
					{
						match = false;
					}
					
					if(match)
					{
						clips.Add (clip);
					}
				}
				rando = Random.Range(0, clips.Count);
				SFX_Source.PlayOneShot(clips[rando]);
                    length = clips[rando].length;
                    break;
                case SoundType.System:
				foreach(AudioClip clip in m_System_List)
				{
					bool match = true;
					if(!clip.name.Contains(sound))
					{
						match = false;
					}

					if(match)
					{
						clips.Add (clip);
					}
				}
				rando = Random.Range(0, clips.Count);
				System_Source.PlayOneShot(clips[rando]);
                    length = clips[rando].length;
                    break;
			}
		}
        return length;
	}

    /// <summary> Searches the lsits for a specific sound </summary>
    /// <param name="list"> List we are searching </param>
    /// <param name="sound"> Sound we are searching for </param>
    /// <returns>  </returns>
    private AudioClip FindSound(List<AudioClip> list, string sound)
    {
        AudioClip clip = new AudioClip();
        foreach(AudioClip aClip in list)
        {
			if(aClip.name == sound)
            {
				clip = aClip;
                break;
            }
        }
        return clip;
    }

    /// <summary> Slows all audio sources </summary>
    public void SlowMoOn(float _Speed)
    {
        Music_Source.pitch = _Speed;
        SFX_Source.pitch = _Speed;
    }

    /// <summary> turns all audio sources back to normal </summary>
    public void SlowMoOff(float _Speed)
    {
        Music_Source.pitch = _Speed;
        SFX_Source.pitch = _Speed;
    }
}
