using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void HeadAniOver()//动画结束后销毁头部
    {
        //GameObject.Destroy(gameObject);
        gameObject.SetActive(false);
    }

    // public  void HeadAniOver()
    // {
    //     GameObject.SetActive(false);
    // }
}
