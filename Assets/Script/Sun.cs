using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;
    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
           GameObject.Destroy(gameObject);
        }
    }

    //阳光被点击后飞到UI位置，增加阳光数量
    private void OnMouseDown()
    {
        //TODO: 阳光飞到UI位置然后销毁
        
        GameObject.Destroy(gameObject);
        GameManager.instance.ChangeSunNum(25);
    }
}
