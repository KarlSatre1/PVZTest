using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public bool gameStart;
    public static GameManager instance;
    public int sunNum;  //需要将太阳数量的文本控件拖拽到这里

    //僵尸生成的变量
    public GameObject zombiePrefab;
    public GameObject bornParent;
    public float createZombieTime;
    private int zOrderIndex = 0;

    //读取数据表的逻辑
    public LevelData levelData;
    [HideInInspector]



    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
       
    }   

    void Start()
    {
        ReadData();
    }

    private void GameStart() //游戏开始之初显示UIManager中的sunNum
    {   
        


        //instance = this;
        UIManager.instance.InitUI();
    }   

    public void GameReallyStart()
    {
        GameManager.instance.gameStart = true;
        CreateZombie();
    }
    

    void ReadData()
    {
        StartCoroutine(LoadTable());

    }

    IEnumerator LoadTable()
    {
        ResourceRequest request = Resources.LoadAsync("Level");
        yield return request;
        levelData = request.asset as LevelData;
        for (int i = 0; i < levelData.LevelDataList.Count; i++)
        {
            Debug.Log("测试僵尸生成" + levelData.LevelDataList[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //阳光数量变化
    public void ChangeSunNum(int changeNum)
    {
        sunNum += changeNum;
        if(sunNum <= 0)
        {
            sunNum = 0;
        }

        //todo: 更新UI,阳光数量变化,改变卡片状态
        UIManager.instance.UpdateUI();

    }

    public void CreateZombie()
    {
        StartCoroutine(DelayCreateZombie());
    }

//僵尸生成,协程函数的使用
    IEnumerator DelayCreateZombie()
    {

        yield return new WaitForSeconds(createZombieTime);
        GameObject zombie = Instantiate(zombiePrefab);
        int index = Random.Range(2, 4);
        Transform zobieLine = bornParent.transform.Find("born" + index.ToString());
        zombie.transform.parent = zobieLine;    
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex; //设置僵尸生成的层级
        zOrderIndex += 1;

        //再次启用协程
        StartCoroutine(DelayCreateZombie());
    }

}
