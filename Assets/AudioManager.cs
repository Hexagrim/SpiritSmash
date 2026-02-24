using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music, SFX;
    public AudioClip BG1, BG2, BG3;
    public bool B1, B2, B3;
    public float fadeDuration = 2f;
    bool MusicFinished = true;
    public static AudioManager instance;
    private bool isEscapedState = false;
    public AudioClip Dash, Death, Shake,Shake1;
    float normalVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // destroy duplicate
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            Music.volume = 0.1f;
        }
        else {

            bool escaped = FindFirstObjectByType<LevelManager>().escaped;
            bool dead = FindFirstObjectByType<DeathManager>().dead;
            bool shouldBeLow = escaped || dead;

            if (shouldBeLow && !isEscapedState)
            {
                Music.volume = 0.0101f;
                isEscapedState = true;
            }
            else if (!shouldBeLow && isEscapedState)
            {
                Music.volume = 0.045f;
                isEscapedState = false;
            }

        }

        //if (B2)
        //{
        //    Music.clip = BG2;
        //    Music.playOnAwake = true;
        //    Music.Play();
        //    B2 = false;
        //    Music.volume = 0.05f;
        //}
        //if (B1)
        //{
        //    Music.clip = BG1;
        //    Music.playOnAwake = true;
        //    Music.Play();
        //    B1 = false;
        //    Music.volume = 0.05f;
        //}
        //if (B3)
        //{
        //    Music.clip = BG3;
        //    Music.playOnAwake = true;
        //    Music.Play();
        //    B3 = false;
        //    Music.volume = 0.1f;
        //}

        if (MusicFinished)
        {
            StartCoroutine(RandMusic(120));
        }
    }
    IEnumerator RandMusic(float time)
    {
        int Value = Random.Range(0, 2);
        MusicFinished = false;  
        if(Value == 0)
        {
            StartCoroutine(SwitchMusic(BG1 , 0.05f));
        }
        else
        {
            StartCoroutine(SwitchMusic(BG2, 0.05f));
        }
        yield return new WaitForSecondsRealtime(time);
        MusicFinished = true;
    }
    IEnumerator SwitchMusic(AudioClip newClip, float Volume)
    {
        float startVolume = Music.volume;
        while (Music.volume > 0)
        {
            Music.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        Music.volume = 0f;
        Music.Stop();
        Music.clip = newClip;
        Music.Play();
        while (Music.volume < Volume)
        {
            Music.volume += Volume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        Music.volume = Volume;
    }
    public void PlaySFX(AudioClip clip)
    {
        SFX.pitch = Random.Range(0.9f, 1.1f);
        SFX.PlayOneShot(clip);
    }
}
