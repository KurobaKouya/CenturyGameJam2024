using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Pool;




[Serializable]
public class AudioClipInstance
{
    public string name;
    public AudioClip audioClip;
    public bool bypassLimit = false;

    [Header("Volume")]
    public float volumeMinRange = 1;
    public float volumeMaxRange = 1;

    [Header("Pitch")]
    public float pitchMinRange = 1;
    public float pitchMaxRange = 1;

    [Header("StartTime")]
    public float startTime = 0;

    public float RandomVolume()
    {
        return UnityEngine.Random.Range(volumeMinRange, volumeMaxRange);
    }

    public float RandomPitch()
    {
        return UnityEngine.Random.Range(pitchMinRange, pitchMaxRange);
    }


}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource bgmSource;
    
    
    int poolAmount = 20;
    ObjectPool<AudioSource> pool;

    


    [SerializeField] int maxSameAudioPlaying = 10;
    public AudioClipInstance testClip;


    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
         
    }


    private void Start()
    {
        pool = new ObjectPool<AudioSource>(CreatePool, OnGet, OnRelease, null, true, poolAmount, 100);
        //LoadBGMVolume();
        //LoadSoundVolume();
    }


                //}

                //public void PlayOneShot(AudioClip audioClip, float volume, AudioType audioType, float pitch = 1f)
                //{

                //    AudioClip clip = audioClip;

                //    GetType(audioType).PlayOneShot(audioClip, volume);
                //}


                //public void PlayOneShot(List<AudioClip> audioClips, float volume, AudioType audioType)
                //{
                //    AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];
                //    PlayOneShot(clip, volume, audioType);
                //}

                //public void PlayOneShot(AudioClipInstance clipInstance)
                //{
                //    float vol = Random.Range(clipInstance.volumeMinRange, clipInstance.volumeMaxRange);
                //    //float pitch = Random.Range(clipInstance.pitchMinRange, clipInstance.pitchMaxRange);
                //    PlayOneShot(clipInstance.audioClip, vol, clipInstance.type);
                //}

                //public void PlayRandomOneShot(List<AudioClipInstance> clipInstances)
                //{
                //    AudioClipInstance clip = clipInstances[Random.Range(0, clipInstances.Count)];
                //    PlayOneShot(clip);

                //}


                //void LoadBGMVolume()
                //{

                //    audioMixer.SetFloat(bGMVolume, Mathf.Log10(SettingManager.musicIntensity) * 20);

                //}

                //void LoadSoundVolume()
                //{
                //    audioMixer.SetFloat(soundVolume, Mathf.Log10(SettingManager.soundIntensity) * 20);
                //}

                AudioSource CreatePool()
    {
        AudioSource audioSource = Instantiate(sfxSource, transform);
        return audioSource;
    }

    public void SetBGM(AudioClipInstance bgm)
    {
        bgmSource.clip = bgm.audioClip;
        bgmSource.volume = bgm.RandomVolume();
        bgmSource.pitch = bgm.RandomPitch();
    }

    /// <summary>
    /// Use AudioClipInstance as a variable and assign audioClip in inspector. 
    /// <para> Alternatively you can use PlayAudioComponent and use PlayAudio() to use it for events.</para> 
    ///
    /// </summary>
    /// <param name="clipInstance"></param>
    /// <returns></returns>
    public void PlaySourceAudio(AudioClipInstance clipInstance, Vector3 pos = new Vector3())
    {

        AudioSource aud;
        //AudioClipInstance clipInstance = /*GetClip(nameOfClip);*/
        if (clipInstance == null)
            Debug.LogError("Cannot find audio clip!");
        //stop oldest to prevent too many clips playing at once
        (AudioSource, int) oldestAudioSource = GetOldestIfTooManySimilar(clipInstance);
        if (!clipInstance.bypassLimit)
            oldestAudioSource.Item1?.gameObject.SetActive(false);

        aud = pool.Get();
        aud.clip = clipInstance.audioClip;
        //get number of same clip audiosources playing and divide to reduce loudness
        aud.volume = clipInstance.RandomVolume() /** GameManager.Instance.gameData.soundIntensity /*/;
        aud.pitch = clipInstance.RandomPitch();
        aud.transform.position = pos;
        aud.time = clipInstance.startTime;
        aud.Play();
        StartCoroutine(DisableSource(aud, aud.clip.length / aud.pitch + 0.1f));
        
    }

    //AudioClipInstance GetClip(string name)
    //{
    //    for (int i = 0; i < allClips.Count; i++)
    //    {
    //        if (allClips[i].name == name) return allClips[i];
    //    }
    //    return null;
    //}
    (AudioSource, int) GetOldestIfTooManySimilar(AudioClipInstance clip)
    {
        int count = 0;
        AudioSource[] audios = transform.GetComponentsInChildren<AudioSource>();
        AudioSource oldest = null;
        foreach (AudioSource item in audios)
        {
            if (item.gameObject.activeInHierarchy)
            {
                if (item.clip == clip.audioClip)
                {
                    if (oldest == null || oldest.time < item.time)
                        oldest = item;
                    count++;
                }
            }

        }
        if (count <= maxSameAudioPlaying)
            return (null, count);
        else
            return (oldest, count);
    }

    void OnGet(AudioSource source)
    {
        source.gameObject.SetActive(true);
    }

    void OnRelease(AudioSource source)
    {
        source.gameObject.SetActive(false);
    }

    IEnumerator DisableSource(AudioSource source, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pool.Release(source);

    }









}


