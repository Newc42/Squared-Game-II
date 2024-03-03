using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public int levelToLoad;
    private GameObject inGameMenu;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(SceneManager.GetActiveScene().buildIndex != 0){
                inGameMenu = GameObject.FindGameObjectWithTag("InGameMenu");
                if(Time.timeScale == 1){
                    PouseGame();
                }else{         
                    ResumeGame();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R)){
            ReloadScene();
        }
    }

    public void PouseGame(){
        Time.timeScale = 0;
        inGameMenu.GetComponent<InGameMenu>().ShowInGameMenu();
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        inGameMenu = GameObject.FindGameObjectWithTag("InGameMenu");
        inGameMenu.GetComponent<InGameMenu>().HideInGameMenu();
    }

    public void LoadNextLevel(){
        SceneManager.LoadScene(1);
    }

    public void LoadFirstScene(){
        SceneManager.LoadScene(2);
    }

    public void LoadMainMenu(){
        SetTimeScaleToOne();
        if(SceneManager.GetActiveScene().buildIndex != 0){
            SceneManager.LoadScene(0);
        }
    }

    public void Exit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ReloadScene(){
        SetTimeScaleToOne();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    private void SetTimeScaleToOne(){
        if(Time.timeScale == 0){
            Time.timeScale = 1;
        }
    }
}
