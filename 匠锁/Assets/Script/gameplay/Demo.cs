using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Demo : MonoBehaviour
{
    Color mouseOverColor = Color.blue;
    Color originalColor;

    float endx;
    float endy;
    float endz;
    float tempx;
    float tempy;
    float tempz;
    Vector3 _mousePos;

    //音源AudioSource相当于播放器，而音效AudioClip相当于磁带
    public AudioSource music;
    public AudioClip drag;//这里给块添加拖动的音效

    void Start()
    {
       // originalColor = GetComponent<Renderer>().sharedMaterial.color;
    }

    void OnMouseOver()
    {
       // GetComponent<Renderer>().material.color = mouseOverColor;
    }

    void OnMouseExit()
    {
       // GetComponent<Renderer>().material.color = originalColor;
    }

    private void OnMouseUp()
    {
        foreach (Rigidbody objj in UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)))
        {
            ////print(objj);
            objj.isKinematic = true;
        }
        endx = transform.position.x;
        endy = transform.position.y;
        endz = transform.position.z;
        if (System.Math.Abs(endx - tempx) > 0 || System.Math.Abs(tempy - endy) > 0 || System.Math.Abs(tempz - endz) > 0)
        {

            gamemanager1.steps = gamemanager1.steps + 1;
            gamemanager1.Setsteps();
         //   //print(gamemanager1.text.text);
        //    //print("set");
        }
    }

    IEnumerator OnMouseDown()
    {
        tempx = transform.position.x;
        tempy = transform.position.y;
        tempz = transform.position.z;

        GetComponent<Rigidbody>().isKinematic = false;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosOnScreen = Input.mousePosition;
        mousePosOnScreen.z = screenPos.z;
        _mousePos = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        while (Input.GetMouseButton(0))
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePosOnScreen = Input.mousePosition;
            mousePosOnScreen.z = screenPos.z;
            Vector3 now = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
            Vector3 vec = now - _mousePos;
         //   //print(vec);
            GetComponent<Rigidbody>().AddForce(vec);

            music.clip = drag;
            music.Play();
            music.volume = 1.25f;
            //   //print("force");
            //transform.position = curPosition;
            yield return new WaitForFixedUpdate();
        }
    }
    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        drag = Resources.Load<AudioClip>("music/drag");
    }

}