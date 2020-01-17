using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager1 : MonoBehaviour
{
    public PanelResult resultPanel;

    public GameObject pausePanel;
    public GameObject aboutPanel;

    public Sprite[] bg_sprites;
    private static int total = 0;
    private static bool[] unlocked = null;
    private static int std = 0;
    private static int left = 0;
    public static int steps = 0;
    public static Text step_text;
    public Text level_text;
    public Text time_text;

    private int all;

    private AudioSource _audioSource = null;
    public AudioClip background_music = null;
    private int flag = 0;


    public enum STATE
    {
        Normal,
        Pause,
        About,
        Result
    }
    public STATE m_state;
    private LevelInfo levelInfo;

    [HideInInspector]
    public int levelId;
    float _timer;

    void Start()
    {
        Camera.main.orthographic = false;

        level_text = GameObject.Find("levelTxt").GetComponent<Text>();
        level_text.text = "关  卡 " + levelId.ToString();
        step_text = GameObject.Find("stepTxt").GetComponent<Text>();
        time_text = GameObject.Find("timeTxt").GetComponent<Text>();

        Init();

        step_text.text = steps + "  步";

    }
    public static void Setsteps()
    {

        step_text.text = steps + "  步";
    }
    // Update is called once per frame
    void Update()
    {


        switch (m_state)
        {
            case STATE.Normal:
                {
                    step_text.gameObject.SetActive(true);
                    time_text.gameObject.SetActive(true);
                    _timer += Time.deltaTime;
                    UpdateTimeDisplay((int)_timer);
                    updateUnlockStatus();
                }
                break;

            case STATE.Pause:
                break;
            case STATE.About:
                break;
            case STATE.Result:
                {
                    step_text.gameObject.SetActive(false);
                    time_text.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }

    }
    private void Awake()
    {
        levelInfo = DataMgr.Instance().levelInfo;
        levelId = DataMgr.Instance().levelId;

        GameObject.Find("PanseBtn").GetComponent<Button>().onClick.AddListener(() => { OnPauseBtn(); });
        GameObject.Find("AudioBtn").GetComponent<Button>().onClick.AddListener(() => { OnAudioBtn(); });
        GameObject.Find("AboutBtn").GetComponent<Button>().onClick.AddListener(() => { OnAboutBtn(); });
    }

    private void destroyAll()
    {
        for (int i = 1; i <= this.all; i++)
        {
            GameObject obj = GameObject.Find("part" + i);
            if (obj != null)
            {
                obj.SetActive(false);
                Destroy(obj);
            }
        }
    }

    void Init()
    {
        Image bg = GameObject.Find("bg").GetComponent<Image>();
        bg.sprite = bg_sprites[levelId - 1];
        buildAll();
        SwitchState(STATE.Normal);
        _timer = 0;
        steps = 0;
        step_text.text = steps + "  步";

    }

    private void buildAll()
    {
        String para = @"Assets\Script\models\" + levelId + ".wood";
        StreamReader model = new StreamReader(para);
        string outline = model.ReadLine();
        string line;
        int counter = 1;
        total = 0;

        string[] maxesString = outline.Split(' ');
        for (int i = 0; i < 3; i++)
        {
            if (std < Int32.Parse(maxesString[i]))
            {
                std = Int32.Parse(maxesString[i]);
            }
        }


        // partitial counter
        while ((line = model.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
            string[] nums = line.Split(' ');
            int allNum = nums.Length;
            if (allNum % 3 == 0)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                string baseName = "part" + Convert.ToString(counter);
                obj.transform.position = new Vector3(Int32.Parse(nums[0]), Int32.Parse(nums[1]), Int32.Parse(nums[2]));
                obj.AddComponent<Rigidbody>();
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.GetComponent<Rigidbody>().mass = 0.01F;
                obj.GetComponent<Rigidbody>().drag = 0;
                obj.GetComponent<Rigidbody>().angularDrag = 0;
                obj.GetComponent<Rigidbody>().freezeRotation = true;
                obj.GetComponent<Renderer>().material= Resources.Load("pic/"+Convert.ToString(counter)) as Material;
                obj.GetComponent<Collider>().material.dynamicFriction = 0;
                obj.GetComponent<Collider>().material.staticFriction = 0;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.name = baseName;
                total += 1;

                // particle counter
                for (int i = 1; i < allNum / 3; i++)
                {
                    GameObject derived = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    derived.GetComponent<Renderer>().material = Resources.Load("pic/" + Convert.ToString(counter)) as Material;
                    derived.transform.position = new Vector3(Int32.Parse(nums[i * 3]), Int32.Parse(nums[i * 3 + 1]), Int32.Parse(nums[i * 3 + 2]));
                    derived.transform.parent = obj.transform;
                }
                if (levelId == 2)
                {
                    if (counter == 1)
                    {
                    
                        Vector3 scale = obj.transform.localScale;
                        scale.x = 1.1f;
                        scale.y = 1.1f;
                        scale.z = 0.8f;
                        obj.transform.localScale = scale;
                    }
                    if (counter == 2)
                    {
                    
                        Vector3 scale = obj.transform.localScale;
                        Vector3 pos = obj.transform.position;
                        scale.z = 1.0f;
                        scale.y = 1.08f;
                        pos.y = -0.925f;
                        obj.transform.localScale = scale;
                    }

                    if (counter == 4)
                    {
                        Vector3 pos = obj.transform.position;
                        pos.y = 2.35f;
                        pos.x = -0.55f;
                        obj.transform.position = pos;


                    }
                    if (counter == 3)
                    {
                        Vector3 pos = obj.transform.position;
                        pos.y = 0.156f;
                        pos.x = -0.55f;
                        obj.transform.position = pos;


                    }
                }
                if (levelId == 3)
                {
                    if (counter == 4 || counter == 3)
                    {

                        Vector3 scale = obj.transform.localScale;
                        Vector3 pos = obj.transform.position;

                        scale.x = 0.85f;

                        scale.z = 1.3f;
                        pos.z = pos.z - 1.5f;
                        obj.transform.localScale = scale;
                        obj.transform.position = pos;
                    }
                    if (counter == 2 || counter == 5)
                    {

                        Vector3 scale = obj.transform.localScale;
                        scale.x = 1.15f;
                        scale.y = 0.85f;
                        obj.transform.localScale = scale;
                    }
                    if (counter == 6 || counter == 7)
                    {

                        Vector3 scale = obj.transform.localScale;
                        scale.x = 0.85f;
                        scale.y = 1.15f;
                        scale.z = 0.85f;
                        obj.transform.localScale = scale;
                    }

                }

                if (levelId == 4)
                {
                    if (counter == 1)
                    {
                        //print("here");
                        Vector3 scale = obj.transform.localScale;
                        scale.x = 0.80f;

                        obj.transform.localScale = scale;
                    }
                    if (counter == 2)
                    {
                        //print("here");
                        Vector3 scale = obj.transform.localScale;
                        scale.z = 0.85f;

                        obj.transform.localScale = scale;
                    }
                    if (counter == 3)
                    {
                        //print("here");
                        Vector3 scale = obj.transform.localScale;
                        scale.y = 0.85f;

                        obj.transform.localScale = scale;
                    }

                }
                if (levelId == 5)
                {
                    if (counter == 2 || counter == 3)
                    {
                        //print("here");
                        Vector3 scale = obj.transform.localScale;
                        scale.x = 1.05f;
                        scale.y = 1.05f;
                        scale.z = 0.95f;
                        obj.transform.localScale = scale;
                    }


                }
                if (levelId == 6)
                {
                    if (counter == 4 || counter == 1)
                    {
                       
                        Vector3 scale = obj.transform.localScale;
                        scale.y = 0.90f;
                        scale.x = 0.90f;
                        scale.z = 1.10f;
                        obj.transform.localScale = scale;
                    }
                    if (counter == 2 || counter == 3)
                    {
                     
                        Vector3 scale = obj.transform.localScale;
                        scale.x = 0.90f;
                        scale.z = 0.90f;
                        obj.transform.localScale = scale;
                    }
                    if (counter == 6 || counter == 5)
                    {
                      
                        Vector3 scale = obj.transform.localScale;
                        Vector3 pos = obj.transform.position;
                        if (counter == 6) { pos.y = pos.y - 0.1f; }
                        else { pos.y = pos.y + 0.1f; }
                        obj.transform.position = pos;
                        scale.y = 0.90f;
                        scale.z = 0.90f;
                        obj.transform.localScale = scale;
                    }
                }

   
            if (levelId == 7)
                {
                    if (counter == 1)
                    {
                        Vector3 scale = obj.transform.localScale;
                        scale.y = 1.05f;

                        scale.z = 0.8f;
                        obj.transform.localScale = scale;
                    }
                    else
                    {
                        Vector3 scale = obj.transform.localScale;
                        scale.x = 0.95f;

                        scale.y = 0.95f;
                        obj.transform.localScale = scale;

                    }
                }
                obj.AddComponent<Demo>();
                obj.AddComponent<Self>();
            }
            else
            {
                System.Console.WriteLine("Error: Dimensions Unmatch");
            }
            counter++;
        }
        unlocked = new bool[total];
        left = total;
        this.all = total;
    }

    public void NextLevel()
    {
        if (levelId != 7)
        {
            int levelCount = DataMgr.Instance().levelData.levels.Length;
            if (levelId < levelCount)
            {
                levelId++;
                DataMgr.Instance().levelId = levelId;
                levelInfo = DataMgr.Instance().levelData.levels[levelId - 1];
            }
            Init();
            GameObject.Find("levelTxt").GetComponent<Text>().text = "关  卡 " + levelId.ToString();
        }
        else
        {
            GameObject.Find("NextText").GetComponent<Text>().text = "敬 请 期 待";
        }

    }
    public void RetryLevel()
    {
        destroyAll();
        Init();
    }
    public void SwitchState(STATE state)
    {

        m_state = state;
        switch (state)
        {
            case STATE.Normal:
                resultPanel.gameObject.SetActive(false);
                pausePanel.SetActive(false);
                aboutPanel.SetActive(false);
                break;
            case STATE.Pause:
                pausePanel.SetActive(true);
                break;
            case STATE.About:
                break;
            case STATE.Result:
                resultPanel.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    void UpdateTimeDisplay(int time)
    {
        System.TimeSpan tspan = new System.TimeSpan(0, 0, time);
        string timeStr = string.Format("{0:00}:{1:00}", tspan.Minutes, tspan.Seconds);
        time_text.text = timeStr.Substring(0, 2) + " : " + timeStr.Substring(3, 2);

    }

    public void updateUnlockStatus()
    {
        for (int i = 0; i < total; i++)
        {
            if (unlocked[i] == false)
            {
                unlocked[i] = true;
                GameObject cube1 = GameObject.Find("part" + Convert.ToString(i + 1));
                for (int j = 0; j < total; j++)
                {
                    if (i != j && unlocked[j] == false)
                    {
                        GameObject cube2 = GameObject.Find("part" + Convert.ToString(j + 1));

                        Vector3 diff = cube1.transform.position - cube2.transform.position;
                        if (diff.x > std || diff.x < -std || diff.y > std
                            || diff.y < -std || diff.z > std || diff.z < -std)
                        {
                            continue;
                        }
                        else
                        {
                            unlocked[i] = false;
                            break;
                        }
                    }
                }

                if (unlocked[i])
                {
                    cube1.GetComponent<Self>().setUnlocked(true);
                    left--;
                    if (left == 0)
                    {

                        SwitchState(STATE.Result);
                        resultPanel.MatchResult(true);
                    }
                }
            }
        }
    }


    void OnPauseBtn()
    {
        SwitchState(STATE.Pause);
    }

    void OnAudioBtn()
    {
        //声音开关
        //声音开关
        flag++;
        if (flag == 1) { _audioSource = gameObject.AddComponent<AudioSource>(); }

        //加载 Audio Clip 对象
        background_music = Resources.Load<AudioClip>("music/background_music");
        //播放声音
        _audioSource.loop = true;
        _audioSource.clip = background_music;
        _audioSource.volume = 0.25f;

        //监听按钮点击事件

        if (flag % 2 != 0)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }

    }

    void OnAboutBtn()
    {


        SwitchState(STATE.About);
        Cursor.lockState = CursorLockMode.Locked;
        // Init();
        String para = @"Assets\Script\solutions\solution" + levelId + ".wood";
        StreamReader solution = new StreamReader(para);

        destroyAll();
        buildAll();

        string names = solution.ReadLine();//读取第一行数据，第一行为块的编号

        string action;
        float sleep = 0;
        int counter = 0;

        string[] name = names.Split(' ');
        List<GameObject> part = new List<GameObject>();
        for (int i = 0; i < name.Length; i++)
        {
            part.Add(GameObject.Find("part" + name[i]));

        }
        //整体向后移一段距离
        for (int i = 0; i < part.Count; i++)
        {
            part[i].transform.Translate(new Vector3(0, 0, 50) * Time.deltaTime * 10);
        }

        while ((action = solution.ReadLine()) != null)
        {
            System.Console.WriteLine(action);

            // Debug.Log(action);
            String[] act = action.Split(' ');
            for (int i = 0; i < act.Length; i++)
                Debug.Log(int.Parse(act[i]));

            int length = act.Length;//数组长度
            Debug.Log(length);

            if (length == 5)
            {
                //part[int.Parse(act[0])-1].transform.Translate(new Vector3(int.Parse(act[1]), int.Parse(act[2]), int.Parse(act[3])) * Time.deltaTime * 10);
                int a = int.Parse(act[1]);
                int b = int.Parse(act[2]);
                int c = int.Parse(act[3]);
                float t = float.Parse(act[4]);
                if (a != 0) counter = a;
                else if (b != 0) counter = b;
                else if (c != 0) counter = c;


                StartCoroutine(DelayToInvokeDo(() =>
                {
                    part[int.Parse(act[0]) - 1].transform.Translate(new Vector3(a, b, c) * Time.deltaTime * 10);
                }, t));


            }
            else if (length == 2)
            {

                sleep = float.Parse(act[1]);
                StartCoroutine(DelayToInvokeDo(() =>
                {
                    Destroy(part[int.Parse(act[0]) - 1]);
                }, float.Parse(act[1])));

            }



        }
        StartCoroutine(DelayToInvokeDo(() =>
        {

            Cursor.lockState = CursorLockMode.None;
            SwitchState(STATE.Result);
            resultPanel.MatchResult(false);
        }, sleep));


    }

    public IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {//延时函数
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}
