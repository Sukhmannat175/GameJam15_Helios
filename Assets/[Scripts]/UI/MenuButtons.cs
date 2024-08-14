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
    public GameObject recipeMenu;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {            
            if (craftingMenu.activeInHierarchy)
            {
                AudioController.Instance.Play("BagClose");
                GameController.Instance.shine = true;
                craftingMenu.SetActive(false);
                return;
            }

            if (optionsMenu.activeInHierarchy)
            {
                AudioController.Instance.Play("MenuClick");
                optionsMenu.SetActive(false);
                return;
            }

            if (tutorial.activeInHierarchy)
            {
                AudioController.Instance.Play("MenuClick");
                GameController.Instance.shine = true;
                tutorial.SetActive(false);
                return;
            }

            if (recipeMenu.activeInHierarchy)
            {
                AudioController.Instance.Play("BagOpen");
                recipeMenu.SetActive(false);
                craftingMenu.SetActive(true);
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
            GameController.Instance.shine = false;
            GameController.Instance.Pause(true);
        }
        else
        {
            GameController.Instance.shine = true;
            pauseMenu.SetActive(false);
            GameController.Instance.Pause(false);
        }
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

    public void CraftingMenu()
    {
        if (pauseMenu.activeInHierarchy) { return; }

        if (!craftingMenu.activeInHierarchy)
        {
            GameController.Instance.shine = false;
            AudioController.Instance.Play("BagOpen");
        }
        else if (craftingMenu.activeInHierarchy)
        {
            GameController.Instance.shine = true;
            AudioController.Instance.Play("BagClose");
        }

        if (recipeMenu.activeInHierarchy)
        {
            recipeMenu.SetActive(false);
        }
        craftingMenu.SetActive(!craftingMenu.activeInHierarchy);
    }

    public void RecipeMenu()
    {
        if (pauseMenu.activeInHierarchy) { return; }

        if (!recipeMenu.activeInHierarchy) AudioController.Instance.Play("BagClose");
        else if (recipeMenu.activeInHierarchy) AudioController.Instance.Play("BagOpen");

        craftingMenu.SetActive(!craftingMenu.activeInHierarchy);
        recipeMenu.SetActive(!recipeMenu.activeInHierarchy);
    }

    public void QuitToStart()
    {
        Time.timeScale = 1;
        AudioController.Instance.Play("MainMenuClick");
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int id)
    {
        Time.timeScale = 1;
        AudioController.Instance.Play("MainMenuClick");
        SceneManager.LoadScene(id);
    }
}
