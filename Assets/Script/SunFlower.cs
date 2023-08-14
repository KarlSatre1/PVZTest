using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{   // Start is called before the first frame update
    
    public float readyTime;
    private float timer;
    public GameObject sunPrefab;
    private int sunNum;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        timer =  0;

    }

    // Update is called once per frame
    void Update()
    {
        if (!plantStart)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= readyTime)
        {
            animator.SetBool("Ready", true);
        }
    }

    public void BornSunOver()
    {
        BornSun();
        animator.SetBool("Ready", false);
        timer = 0;
    }   

    private void BornSun()
    {
        GameObject sunNew = Instantiate(sunPrefab); //生成sunPrefab物体
        sunNum += 1;
        float randomX;
        if(sunNum % 2 == 1)
        {
            randomX = Random.Range(transform.position.x - 30,transform.position.x - 20);
        }
        else
        {
            randomX = Random.Range(-transform.position.x + 20, transform.position.x + 30);

        }
        float randomY = Random.Range(transform.position.y - 20, transform.position.y + 20);
        sunNew.transform.position = new Vector3(randomX, randomY,-0.1f);


    }
}
