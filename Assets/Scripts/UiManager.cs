using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    [SerializeField] public GameObject Menu_canvas;
    [SerializeField] public GameObject Level_canvas;

    [SerializeField] public Animator BatLeftAnimator;
    [SerializeField] public Animator BatRightAnimator;

    public static int SceneToLoad;

    public void StartGame() 
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {         
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.Save();            
        }

        SceneToLoad = PlayerPrefs.GetInt("CurrentLevel");

        BatLeftAnimator.SetTrigger("Close_bat");
        BatRightAnimator.SetTrigger("Close_bat");

        

        // SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));

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
