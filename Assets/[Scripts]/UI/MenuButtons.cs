using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject craftingMenu;
    public GameObject optionsMenu;
    public GameObject tutorial;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            AudioController.Instance.Play("MenuClick");
            if (craftingMenu.activeInHierarchy)
            {
                craftingMenu.SetActive(false);
                return;
            }

            if (optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
                return;
            }

            if (tutorial.activeInHierarchy)
            {
                tutorial.SetActive(false);
                return;
            }

            PauseMenu();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            CraftingMenu();
        }
    }

    public void PauseMenu()
    {
        AudioController.Instance.Play("MenuClick");
        if (!pauseMenu.activeInHierarchy)
        {
            craftingMenu.SetActive(false);
            pauseMenu.SetActive(true);
            GameController.Instance.Pause(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            GameController.Instance.Pause(false);
        }
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        AudioController.Instance.Play("MainMenuClick");
    }

    public void Tutorial()
    {
        AudioController.Instance.Play("MainMenuClick");
        tutorial.SetActive(!tutorial.activeInHierarchy);
    }

    public void CraftingMenu()
    {
        AudioController.Instance.Play("MenuClick");
        if (pauseMenu.activeInHierarchy) { return; }
        craftingMenu.SetActive(!craftingMenu.activeInHierarchy);
    }

    public void QuitToStart()
    {
        AudioController.Instance.Play("MainMenuClick");
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
