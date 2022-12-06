using UnityEngine.Audio;
using System;
using UnityEngine;

// WORK IN PROGRESS BY NIILO

public partial class Sound_Manager : MonoBehaviour
{
    // Declare variables/objects
    public Sound[] sounds;
    public static Sound_Manager Instance;

    void Awake()
    {
        Instance = this;

        // populate sounds array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // start playing sound <name of sound>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            // error message to avoid NullReference
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    // stop playing sound <name of sound>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }
}
