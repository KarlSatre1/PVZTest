using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public  float health = 100;
    protected  float currentHealth;
    protected bool plantStart;
    protected Animator animator;  //private,public,protected的区别
    protected BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = health;
        plantStart = false;
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator.speed = 0;
        boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //启动植物的开关
    public void SetPlantStart()
    {
        plantStart = true;
        animator.speed = 1;
        boxCollider2D.enabled = true;
    }
//受伤逻辑
    public virtual float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num,0,health);
        if(currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }
}
