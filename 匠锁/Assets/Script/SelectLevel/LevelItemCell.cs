using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


//选关界面，每一个关卡按钮单元
public class LevelItemCell : MonoBehaviour ,//,IPointerEnterHandler,IPointerExitHandler,
        IPointerDownHandler,IPointerUpHandler,IPointerClickHandler{
    public RectTransform numTrans;
    public GameObject LockObj;
    public GameObject pressObj;
    public Font font;
    //[HideInInspector]

    public int id;      //每个单元对应的关卡id（从1开始）
    public LevelManager lvlMgr;
   // public Text text;

    LevelInfo levelInfo;
    bool isLock;        

    //点击按钮，跳转到游戏界面
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLock)
        {
            return;
        }
        DataMgr.Instance().levelId = id;
        DataMgr.Instance().levelInfo = levelInfo;
        SceneManager.LoadScene("gameplay");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressObj.SetActive(true);
    }

    //当鼠标进入本单元矩形区域，显示当前关卡描述
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    lvlMgr.SetLevelDesc(levelInfo.desc);

    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    lvlMgr.SetLevelDesc("关卡信息");
    //}

    public void OnPointerUp(PointerEventData eventData)
    {
        pressObj.SetActive(false);
    }

    private void Awake()
    {
        LockObj.SetActive(false);
        pressObj.SetActive(false);
    }

    // Use this for initialization
    void Start () {

        //根据解锁关卡记录，设置关卡是否锁定
 
       // //print("id" + id);

        //初始化关卡数字显示

            //完全用代码动态创建一个Text对象，并作为bg的子节点
            GameObject obj = new GameObject("num",typeof(Text));
            RectTransform rtf = obj.GetComponent<RectTransform>();
            rtf.SetParent(numTrans);
            //设置数字
            Text text = obj.GetComponent<Text>();
            text.text = id.ToString();
             text.fontSize = 70;
           rtf.localPosition = new Vector3(0, 0, 0);

          text.font = font;
        text.color = new Color(0.69f, 0.26f, 0.23f, 1);
        text.alignment = TextAnchor.MiddleCenter;
        //Image img = obj.GetComponent<Image>();
        //img.sprite = lvlMgr.spr_nums[id];
        //img.SetNativeSize();    //图片原始尺寸
        //rtf.localScale = new Vector3(2,2,1);
        //rtf.localPosition = Vector3.zero;
        if (lvlMgr.unlock_levelID < id)
        {
            LockObj.SetActive(true);
            isLock = true;
            text.color = new Color(0.38f, 0.38f, 0.38f, 1);

        }
        levelInfo = DataMgr.Instance().levelData.levels[id - 1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
