using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Start : MonoBehaviour
{

    Button c_btn;
    Button e_btn;

	// Use this for initialization
	void Start () {

        c_btn = GameObject.Find("Button").GetComponent<Button>();
        c_btn.onClick.AddListener(() => { GoToMainMenu(); });
        c_btn = GameObject.Find("exit").GetComponent<Button>();
        c_btn.onClick.AddListener(() => { exit(); });
    }
	
	// Update is called once per frame
	void Update () {

        
	}
   void exit()
    {
        Application.Quit();
    }
    void GoToMainMenu()
        {
            SceneManager.LoadScene("SelectLevel");
        }
}
