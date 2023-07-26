using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public float readyTime;
    private float timer;
    public GameObject sunPrefab;
    private int sunNum;
    void Start()
    {
        animator = GetComponent<Animator>();
        timer =  0;

    }

    // Update is called once per frame
    void Update()
    {
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
        GameObject sunNew = Instantiate(sunPrefab);
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
        sunNew.transform.position = new Vector2(randomX, randomY);


    }
}
