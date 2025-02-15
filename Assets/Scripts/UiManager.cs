using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    [SerializeField] public GameObject Menu_canvas;
    [SerializeField] public GameObject Level_canvas;

    public void StartGame() 
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
        }
        
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchLevelSelect() 
    {
        Menu_canvas.SetActive(false);
        Level_canvas.SetActive(true);
    }
    public void SwitchMenu()
    {
        Menu_canvas.SetActive(true);
        Level_canvas.SetActive(false);
    }

    public void LoadLevel(int i) 
    {
        SceneManager.LoadScene(i);
    }

    public void ResetProgress() 
    {
        PlayerPrefs.DeleteAll();
    }




}
