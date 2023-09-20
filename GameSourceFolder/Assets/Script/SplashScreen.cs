using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu"; // Name of your main menu scene
    public float splashScreenDuration = 3.0f; // Duration of the splash screen in seconds

    private void Start()
    {
        StartCoroutine(TransitionToMainMenu());
    }

    private IEnumerator TransitionToMainMenu()
    {
        yield return new WaitForSeconds(splashScreenDuration);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
