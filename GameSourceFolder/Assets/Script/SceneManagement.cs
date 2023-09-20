//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description:
 * Scenemanagement to control the scenes
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
   

   
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void LoadOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }

    public void LoadRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
