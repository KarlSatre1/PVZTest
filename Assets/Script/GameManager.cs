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

    //表格控制僵尸生成的数据
    public int   curLevelId = 1; //当前关卡
    public int  curProgressId = 1; //当前波次

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
        //ResourceRequest request2 = Resources.LoadAsync("LevelInfo");
        yield return request;
        //yield return request2;
        levelData = request.asset as LevelData;
        for (int i = 0; i < levelData.LevelDataList.Count; i++)
        {
            Debug.Log("数据载入执行成功哈哈哈" + levelData.LevelDataList[i]);
        }
        //Debug.Log("数据载入执行成功哈哈哈");
        //levelInfo = request2.asset as LevelInfo;

        GameStart();
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
        //StartCoroutine(DelayCreateZombie());
    }

    private void TableCreateZombie()
    {
        for(int i = 0; i < levelData.LevelDataList.Count; i++)
        {
            LevelItem levelitem = levelData.LevelDataList[i];
            if(levelData.LevelDataList[i].levelId == curLevelId && levelData.LevelDataList[i].progressId == curProgressId)
            {
                //生成僵尸
                // GameObject zombie = Instantiate(zombiePrefab);
                // int index = Random.Range(2, 4);
                // Transform zobieLine = bornParent.transform.Find("born" + index.ToString());
                // zombie.transform.parent = zobieLine;
                // zombie.transform.localPosition = Vector3.zero;
                // zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex; //设置僵尸生成的层级
                // zOrderIndex += 1;
            }
        }
    }

    IEnumerator ITableCreate(LevelItem levelItem)
    {
        yield return new WaitForSeconds(levelItem.createTime);
        GameObject zombiePrefab = Resources.Load("Prefab/Zombie/" + levelItem.zombieType.ToString()) as GameObject;


        GameObject zombie = Instantiate(zombiePrefab);
        int index = Random.Range(2, 4);
        Transform zobieLine = bornParent.transform.Find("born" + index.ToString());
        zombie.transform.parent = zobieLine;
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex; //设置僵尸生成的层级
        zOrderIndex += 1;
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
