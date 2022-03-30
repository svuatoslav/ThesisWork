using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingMenu = null;
    public void CompanyGame()
    {
        SceneManager.LoadScene(1);
    }  
    public void TrainingGame()
    {
        SceneManager.LoadScene(2);
    }  

    public void SandboxGame()
    {
        SceneManager.LoadScene(3);
    }
    public void ShowSetting()
    {
        _settingMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
