using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthFill : MonoBehaviour
{
    public Image FillImg;
    // Start is called before the first frame update
    void Start()
    {
        FillImg.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            FillImg.fillAmount = FindFirstObjectByType<BossOne>().CurrentHealth / FindFirstObjectByType<BossOne>().Health;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            FillImg.fillAmount = FindFirstObjectByType<Boss2>().CurrentHealth / FindFirstObjectByType<Boss2>().Health;
        }
        else if(SceneManager.GetActiveScene().name == "Level3")
        {
            if (FindFirstObjectByType<LevelThreeManager>().second_stage && FillImg.fillAmount >0)
            {
                FillImg.fillAmount -= Time.deltaTime / 35;
            }
        }
    }
}
