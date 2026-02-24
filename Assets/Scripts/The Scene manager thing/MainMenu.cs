using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator Anim;
    public Animator Anim1;
    public GameObject btn1, btn2, btn3;
    public GameObject NewText1, NewText2, OldText1, OldText2,NewButton,OldButton1,OldButton2;
    // Start is called before the first frame update
    void Start()
    {
        Anim.SetTrigger("fadeIn");
        FindFirstObjectByType<AudioManager>().Music.Play();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        if (PlayerPrefs.GetString("FirstTime", "Yes") == "Yes")
        {
            NewText1.SetActive(true);
            NewText2.SetActive(true);
            NewButton.SetActive(true);
        }
        else
        {
            OldButton1.SetActive(true);
            OldButton2.SetActive(true);
            OldText1.SetActive(true);
            OldText2.SetActive(true);
        }
            
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Play()
    {
        StartCoroutine(PlayFade(PlayerPrefs.GetString("Level", "Level1")));
    }
    IEnumerator PlayFade(string LvlName)
    {
        Anim.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(LvlName);
    }
    public void NewGameClick()
    {
        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);
        Anim1.SetTrigger("new");
    }
    public void NewGameNO()
    {
        Anim1.SetTrigger("no");
        btn1.SetActive(true);
        btn2.SetActive(true);
        btn3.SetActive(true);
    }
    public void NewGame()
    {
        PlayerPrefs.SetString("FirstTime", "No");
        PlayerPrefs.SetString("Tutorial", "yes");
        StartCoroutine(PlayFade("Cutscene"));
        btn1.SetActive(true);
        btn2.SetActive(true);
        btn3.SetActive(true);
    }
    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();

    }
}
