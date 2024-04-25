using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    private bool timerIsOn = false;
    private float timer = 3;
    public Text timerTxt;
    
    private void Update()
    {
        if (timerIsOn == true)
        {
            timer -= Time.deltaTime;
            timerTxt.text = Mathf.CeilToInt(timer).ToString();
        }
        if (timer <= 0)
        {
            timer = 3;
            timerIsOn = false;
            SceneManager.LoadScene(1);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        timerIsOn = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
