using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] MusicClips;

    private AudioSource aud;
    private float currVolume;
    private int currSong;

    private static BackgroundMusic bgAudioObject;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (bgAudioObject == null)
        {
            bgAudioObject = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currSong = Random.Range(0, MusicClips.Length);
        aud = GetComponent<AudioSource>();

        if (!aud.isPlaying)
        {
            aud.clip = MusicClips[currSong];
            aud.playOnAwake = true;
            aud.loop = true;
            aud.Play();
            aud.volume = GameData.instance.musicVolume;
            currVolume = GameData.instance.musicVolume;

            StartCoroutine(PlayBackgroundMusic());
        }
    }

    IEnumerator PlayBackgroundMusic()
    {
        yield return new WaitForSeconds(aud.clip.length);
        currSong = Random.Range(0, MusicClips.Length);
        aud.clip = MusicClips[currSong];
        aud.Play();
        PlayBackgroundMusic();
    }

    public void Update()
    {
        if (currVolume != GameData.instance.musicVolume)
        {
            aud.volume = GameData.instance.musicVolume;
            currVolume = GameData.instance.musicVolume;
        }
    }
}