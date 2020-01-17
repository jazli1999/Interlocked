using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelPause : MonoBehaviour {
    gamemanager1 gameMgr;

    public void Awake()
    {
        GameObject.Find("ContinueBtn").GetComponent<Button>().onClick.AddListener(()=> { OnContinueBtn(); });
        GameObject.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() => { OnBackMainMenu(); });
        GameObject.Find("restart").GetComponent<Button>().onClick.AddListener(() => { OnRestart(); });
        GameObject.Find("exitBtn").GetComponent<Button>().onClick.AddListener(() => { OnExit(); });

        gameMgr = GameObject.Find("gamemanager1").GetComponent<gamemanager1>();

    }

    void OnExit()
    {
       // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    void OnContinueBtn()
    {
        gameObject.SetActive(false);
        gameMgr.SwitchState(gamemanager1.STATE.Normal);
    }
    void OnRestart()
    {  
        gameMgr.RetryLevel();
    }

    void OnBackMainMenu()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
