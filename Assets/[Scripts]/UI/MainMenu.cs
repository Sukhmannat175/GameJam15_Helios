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
        AudioController.Instance.Play("MainMenuBGM");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            optionsMenu.SetActive(false);
            tutorial.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        AudioController.Instance.Play("MainMenuClick");
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        AudioController.Instance.Play("MainMenuClick");
    }

    public void Tutorial()
    {
        tutorial.SetActive(!tutorial.activeInHierarchy);
        AudioController.Instance.Play("MainMenuClick");
    }

    public void Quit()
    {
        Application.Quit();
        AudioController.Instance.Play("MainMenuClick");
    }
}
