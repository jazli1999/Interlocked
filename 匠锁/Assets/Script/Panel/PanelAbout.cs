using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAbout : MonoBehaviour {

    gamemanager1 gameMgr;
    // Use this for initialization
    void Start () {
        gameMgr = GameObject.Find("gamemanager1").GetComponent<gamemanager1>();
        

    }

    // Update is called once per frame
    void Update () {

	}
}
