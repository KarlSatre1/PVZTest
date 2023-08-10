using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Card : MonoBehaviour
{
    public GameObject objectPrefab;//卡牌对应的物体预制件
    private GameObject curGameObject;//卡牌对应的物体实例
    private GameObject darkBg;
    private GameObject progressBar;
    public  float waitTime;
    public int useSun;
    private float timer;
    // Start is called before the first frame update

    void Start()
    {
        darkBg = transform.Find("dark").gameObject;
        progressBar = transform.Find("progress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }

    void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1- per;
    }

    void UpdateDarkBg()
    {
        if (progressBar.GetComponent<Image>().fillAmount == 0)
        {
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }
//鼠标拖曳开始的方法
    public void OnBeginDrag(BaseEventData data)
    {
        Debug.Log("OnBeginDrag" + data.ToString());
        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject = Instantiate(objectPrefab);
        curGameObject.transform.position = TranslateScreenToWorld(pointerEventData.position);
    }
    
  
//鼠标拖拽的方法
    public void OnDrag(BaseEventData data)
    {
        //Debug.Log("OnDrag" + data.ToString());
        if(curGameObject == null)
           {
                return;
            }
            PointerEventData pointerEventData = data as PointerEventData;
            //根据当前鼠标位置，更新物体位置
            curGameObject.transform.position = TranslateScreenToWorld(pointerEventData.position);
    }



    //鼠标抬起的方法
    public void OnEndDrag(BaseEventData data)   //拖拽结束
    {
        Debug.Log("OnEndDrag" + data.ToString());
        if(curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        //获取当前鼠标位置下的碰撞体
        Collider2D [] col = Physics2D.OverlapPointAll(TranslateScreenToWorld(pointerEventData.position));
        //遍历碰撞体,判断物体为“土地”且没有子物体时
        foreach (Collider2D c in col)
        {
            if(c.tag == "Land"&& c.transform.childCount == 0)
            {
                //把物体放到碰撞体位置，作为其子物体
                //curGameObject.transform.SetParent(c.transform);
                curGameObject.transform.parent = c.transform;
                curGameObject.transform.localPosition = Vector3.zero;
                //重置默认值，生成结束
                curGameObject = null;
                break;
            }
        }
        //如果没有符合条件的土地，说明没有放置到合适的位置，销毁物体
        if(curGameObject != null)
        {
            GameObject.Destroy(curGameObject);
            curGameObject = null;
        }

    }

    //转换坐标的工具函数
    public static Vector3 TranslateScreenToWorld(Vector3 position)
    {
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0 );
    }
}

