using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string LevelName;
    public float time;
    public Animator Anim,Anim1;
    public bool TEMP_trigger;

    public bool escaped;
    void Start()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        if (PlayerPrefs.GetString("Tutorial","yes") == "yes")
        {
            if (GameObject.FindWithTag("tut"))
            {
                GameObject.FindWithTag("tut").GetComponent<Animator>().SetTrigger("show");
                PlayerPrefs.SetString("Tutorial", "no");
            }
        }

        PlayerPrefs.Save();

    }
    void Awake()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }


    void Update()
    {
        if(TEMP_trigger)
        {
            StartCoroutine(transition());
            TEMP_trigger = false;
        }

        if (!escaped && Input.GetKeyDown(KeyCode.Escape) && FindFirstObjectByType<PlayerHealthManager>().CurrentHealth != 0)
        {
            GameObject.FindWithTag("LowhpVig").GetComponent<SpriteRenderer>().enabled = false;

            FindFirstObjectByType<HoldToHealCamera>().enabled = false;
            FindFirstObjectByType<PlayerMovement>().enabled = false;
            FindFirstObjectByType<BlobFireMech>().enabled = false;
            FindFirstObjectByType<MouseDash>().enabled = false;

            StartCoroutine(SmoothSlowMo(1, 0, 0.2f));
            GameObject.FindWithTag("esc").GetComponent<Animator>().SetTrigger("fade");

            escaped = true;

        }
    }
    void NextLevel()
    {
        SceneManager.LoadSceneAsync(LevelName);
    }

    public IEnumerator transition()
    {
        escaped = false;
        GameObject.FindWithTag("LowhpVig").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindWithTag("esc").SetActive(false);
        FindFirstObjectByType<HoldToHealCamera>().enabled = false;
        FindFirstObjectByType<PlayerMovement>().enabled = false;
        FindFirstObjectByType<BlobFireMech>().enabled = false;
        FindFirstObjectByType<MouseDash>().enabled = false;
        Anim.SetTrigger("fade");
        StartCoroutine(SmoothSlowMo(1, 0, 1));
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadSceneAsync(LevelName);
    }

    public IEnumerator SmoothSlowMo(float from, float to, float duration)
    {
        float t = 0f;
        Time.timeScale = from;
        Time.fixedDeltaTime = 0.02f * from;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float scale = Mathf.Lerp(from, to, t / duration);
            Time.timeScale = scale;
            Time.fixedDeltaTime = 0.02f * scale;
            yield return null;
        }

        Time.timeScale = to;
        Time.fixedDeltaTime = 0.02f * to;
    }
    public void Continue()
    {
        
        escaped = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        GameObject.FindWithTag("LowhpVig").GetComponent<SpriteRenderer>().enabled = true;

        FindFirstObjectByType<HoldToHealCamera>().enabled = true;
        FindFirstObjectByType<PlayerMovement>().enabled = true;
        FindFirstObjectByType<BlobFireMech>().enabled = true;
        FindFirstObjectByType<MouseDash>().enabled = true;
        GameObject.FindWithTag("esc").GetComponent<Animator>().SetTrigger("fadeOut");
    }
    public void MainMenu()
    {
        StartCoroutine(MainFade());
    }
    IEnumerator MainFade()
    {
        Anim1.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
