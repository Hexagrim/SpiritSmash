using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float time;
    public string sceneName;
    public Animator Transition;
    void Start()
    {
        StartCoroutine(Cut(time));
    }


    void Update()
    {
        
    }
    IEnumerator Cut(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        Transition.SetTrigger("fade");
        SceneManager.LoadSceneAsync(sceneName);

    }
}
