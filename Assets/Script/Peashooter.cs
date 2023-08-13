using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : MonoBehaviour
{
    // Start is called before the first frame update
    public float interval;
    public float timer;
    public GameObject bullet;
    public Transform bulletPos;
    public  float health = 100;
    public  float currentHealth;
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        timer +=Time.deltaTime;
        if(timer >= interval)
        {
            timer = 0;
            Instantiate(bullet,bulletPos.position,Quaternion.identity);

        }
    }
    
    //受伤逻辑
    public float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num,0,health);
        if(currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }
}
