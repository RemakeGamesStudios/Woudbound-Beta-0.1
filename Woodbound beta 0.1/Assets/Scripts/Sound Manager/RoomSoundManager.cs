using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSoundManager : MonoBehaviour
{
    [Header("Audio Clips & RoomNames")]
    public AudioClip[] audioClips;
    public string[] roomNames;
    private AudioSource currentAudioSource;
    [Header("Time values")]
    public float fadeWaitTime;
    
    // Start is called before the first frame update
    public void Start()
    {
        currentAudioSource = this.GetComponent<AudioSource>();
    }
    //updates audio
    public void updateAudioClip(string currentRoom)
    {
        int r = 0;
        for (int i = 0; i < roomNames.Length; i++)
        {
            r = i;
            if (roomNames[i].Equals(currentRoom)) break;
        }
        if (audioClips[r] != null && currentAudioSource.clip != audioClips[r])
        {
            StartCoroutine(fadeClipOut(r));    
        }
        
    }
    public IEnumerator fadeClipOut(int r)
    {
       
        for (int i = 0; i < 5; i++)
        {
            currentAudioSource.volume -= .2f;
            yield return new WaitForSeconds(fadeWaitTime);
        }
        currentAudioSource.Stop();
        StartCoroutine(fadeClipIn(r));

    }
    public IEnumerator fadeClipIn(int r)
    {
        currentAudioSource.clip = audioClips[r];
        currentAudioSource.Play();
        for (int i = 0; i < 5; i++)
        {
            currentAudioSource.volume += .2f;
            yield return new WaitForSeconds(fadeWaitTime);
        }

    }
}
