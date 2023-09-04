using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant
{
    // Start is called before the first frame update
    public float interval;
    public float timer;
    public GameObject bullet;
    public Transform bulletPos;

    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!start)
        {
            return;
        }
        timer +=Time.deltaTime;
        if(timer >= interval)
        {
            timer = 0;
            Instantiate(bullet,bulletPos.position,Quaternion.identity);
            //播放射击音效
            SoundManager.instance.PlaySound(Globals.S_Shoot);

        }
    }
    
    //受伤逻辑

}
