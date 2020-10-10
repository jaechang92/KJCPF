using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public string key;
    public AudioClip value;
}


public class SoundManager : MonoBehaviour
{
    public List<Sound> sounds;

    static public SoundManager instance;


    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BGMStart("NormalBGM");
    }

    
    void Update()
    {
        
    }

    public void PlayOneShotSound(string name)
    {
        audioSource.PlayOneShot(mySoundKeyValue(name),1);
    }

    public void BGMStart(string name)
    {
        audioSource.clip = mySoundKeyValue(name);
        audioSource.Play();
    }

    public AudioClip mySoundKeyValue(string key)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].key == key)
            {
                return sounds[i].value;
            }
        }
        return null;
    }

    
}
