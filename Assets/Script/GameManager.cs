using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int sunNum;  //需要将太阳数量的文本控件拖拽到这里

    //僵尸生成的变量
    public GameObject zombiePrefab;
    public GameObject bornParent;
    public float createZombieTime;
    private int zOrderIndex = 0;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
       
    }   


    void Start() //游戏开始之初显示UIManager中的sunNum
    {
        //instance = this;
        UIManager.instance.InitUI();
        CreateZombie();
        InvokeRepeating("CreateSunDown",3,2);
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

    public void CreateSunDown()
    {
        // 获取左下角、右上角的世界坐标
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);
        // 加载Sun预制件（另一种办法）
        GameObject sunPrefab = Resources.Load("Prefab/Sun") as GameObject;
        Debug.Log("CreateSunDown(),执行了随机生成太阳的逻辑1");
        // 初始化太阳的位置
        float x = Random.Range(leftBottom.x + 30, rightTop.x - 30);
        Vector3 bornPos = new Vector3(x, rightTop.y, 0);
        GameObject sun = Instantiate(sunPrefab, bornPos, Quaternion.identity);
        // 设置目标位置
        float y = Random.Range(leftBottom.y + 100, leftBottom.y + 30);
        sun.GetComponent<Sun>().SetTargetPos(new Vector3(bornPos.x, y, 0));

    }

}
