using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject tutorial;

    private void Start()
    {
        AudioController.Instance.Stop("MainBGM");
        AudioController.Instance.Stop("VictoryBGM");
        AudioController.Instance.Stop("GameOverBGM");
        AudioController.Instance.Play("MainMenuBGM");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            AudioController.Instance.Play("MainMenuClick");
            optionsMenu.SetActive(false);
            tutorial.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        AudioController.Instance.Play("MainMenuClick");
        SceneManager.LoadScene(1);
    }

    public void OptionsMenu()
    {
        AudioController.Instance.Play("MainMenuClick");
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        
    }

    public void Tutorial()
    {
        AudioController.Instance.Play("MainMenuClick");
        tutorial.SetActive(!tutorial.activeInHierarchy);
    }

    public void Quit()
    {
        AudioController.Instance.Play("MainMenuClick");
        Application.Quit();
    }
}
