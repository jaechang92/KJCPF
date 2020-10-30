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

    public string nextSoundSourceName = null;

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
        StartCoroutine(CoSlowUpVolume());
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

    public void SlowMute()
    {
        StartCoroutine(CoSlowMute());
    }

    public IEnumerator CoSlowMute()
    {
        float originVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.Stop();
        audioSource.volume = originVolume;
        if (nextSoundSourceName != null)
        {
            BGMStart(nextSoundSourceName);
            nextSoundSourceName = null;
        }

    }

    public IEnumerator CoSlowUpVolume()
    {
        float originVolume = audioSource.volume;
        audioSource.volume = 0;
        while (audioSource.volume < originVolume)
        {
            audioSource.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }

        audioSource.volume = originVolume;

    }
    
}
