using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;
    private float timer;
    public Vector3 targetPos;
    void Start()
    {
        timer = 0;
        targetPos = Vector3.zero;
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPos != Vector3.zero && Vector3.Distance(targetPos,transform.position) > 0.1f)
        {

            //先把太阳移动到落点
            transform.position = Vector3.MoveTowards(transform.position,targetPos,10);
            return;
        }
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
