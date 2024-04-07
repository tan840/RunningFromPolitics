using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;


    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SoundManager");
                    instance = singletonObject.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.Loop;
        }
        //sounds = GetComponent<Sound>();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { print("failed"); return; }
        s.source.Play();
        //s.source.PlayOneShot(s.clip);
      //  print(name + " played");
    }

    private void Start()
    {
        Play("Theme");
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { print("failed"); return; }
        //s.source.Play();
        s.source.Stop();
       // print(name + " played");
    }

    public void PlayOnce(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { print("failed"); return; }
        //s.source.Play();
        s.source.PlayOneShot(s.clip);
        //print(name + " played");
    }
}
