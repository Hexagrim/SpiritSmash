using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MainMenu());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 3.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(28f);
        PlayerPrefs.SetString("Level", "Level1");
        PlayerPrefs.SetString("FirstTime", "yes");
        PlayerPrefs.SetString("Finished", "yes");
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 1.0f;
    }
}
