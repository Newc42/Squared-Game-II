using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip plains;
    [SerializeField] private AudioClip slimeKing;
    [SerializeField] private AudioClip electricity;
    [SerializeField] private AudioClip portals;
    [SerializeField] private AudioClip temple;

    private void Awake() {
        source = GetComponent<AudioSource>();
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 8:
                PlayNewSong(slimeKing);
                break;
            case 9:
                PlayNewSong(electricity);
                break;
            case 11:
                PlayNewSong(portals);
                break;
            case 12:
                PlayNewSong(temple);
                break;
            default:
                break;
        }
    }

    public void PlayNewSong(AudioClip currentClip){
        source.clip = currentClip;
        source.Play();
    }
}