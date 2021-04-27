using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public GameObject main;
    public GameObject credits;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton() {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void BackButton() {
        main.SetActive(true);
        credits.SetActive(false);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
