using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        PlayerMove,
        PlayerDeath,
        ObjectGrab,
    }
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

    //[SerializeField] public SoundAudioClip[] SoundAudioClipArray;

    /*[System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }*/


   /* public static void PlaySound(Sound sound)
    {
        GameObject SoundGameObject = new GameObject("Sound");
        AudioSource AudioSource = SoundGameObject.AddComponent<AudioSource>();
        AudioSource.PlayOneShot(GetAudioClip(sound));
    }*/

    /*private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in SoundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }

        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }*/
    
    [SerializeField] GameObject[] SFX;
   public void PlayAudio(int Index,float time)
    {
        if (SFX[Index] != null)
        {
            //WaitAndPrint(Index,time);
            print("e");
            StartCoroutine(WaitAndPrint(Index,time));
           // SFX[Index].GetComponent<AudioSource>().Play();
        }else
        {
            Debug.LogError("could not find " + SFX[Index] + " in list");
        }
    }


    IEnumerator WaitAndPrint(int index,float time)
    {
        // suspend execution for 5 seconds
        print("e");
        yield return new WaitForSeconds(time);
        SFX[index].GetComponent<AudioSource>().Play();
        print("e");
        yield return new WaitForSeconds(time);
        //print("WaitAndPrint " + Time.time);

    }

}
