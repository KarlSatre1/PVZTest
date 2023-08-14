using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : MonoBehaviour
{
    //定义僵尸移动变量
    public Vector3 direction = new Vector3(-1,0, 0);  //僵尸移动方向
    public float speed = 1f;

    //定义僵尸攻击变量
    public float damage;
    public float damageInterval = 1f;
    private float damageTimer;

    //定义僵尸血量变量
    public float health = 100;
    public float currentHealth;
    public float lostHeadHealth = 50;

    //定义僵尸相关部件
    private Animator animator;
    private GameObject head;

    //定义僵尸状态变量
    private bool isWalk;
    private bool lostHead;
    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        //初始化僵尸状态和参数
        isWalk = true;
        isDie = false;
        lostHead = false;

        animator = GetComponent<Animator>();
        head = transform.Find("Head").gameObject;
        
        damageTimer = 0;
        currentHealth = health;
        head.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(isDie)
            return;
        Move();
    }

    //僵尸移动逻辑
    private void Move()
    {
        if (isWalk)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }  

    //触发开始
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(isDie)
            return;
        //碰到植物,停止移动
       if (other.tag == "Plant")
       {
            isWalk = false;
            Debug.Log("碰撞了");
            animator.SetBool("Walk", false);
       }
        
    }

    //触发过程
    private void OnTriggerStay2D(Collider2D other)
    {
     
        //碰到植物,造成伤害
        if (other.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                //other.GetComponent<Plant>().TakeDamage(damage);
                damageTimer = 0;
                // //todo:对植物造成伤害,这里只对豌豆射手造成伤害改成对所有植物造成伤害
                Plant plant = other.GetComponent<Plant>();
                float newHealth = plant.ChangeHealth(-damage);
                if (newHealth <= 0)
                {
                    isWalk = true;
                    animator.SetBool("Walk", true);
                }
            }
        }

    }

    //触发结束 
    private void OnTriggerExit2D(Collider2D other)
    {

        //离开植物,或者植物被消灭,继续移动
        if (other.tag == "Plant")
        {
            isWalk = true;
            animator.SetBool("Walk", true);
        }
    }

    //受伤逻辑
    public void ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        //僵尸掉头逻辑
        if (currentHealth <= lostHeadHealth && !lostHead)
        {
            Debug.Log("头掉了");
            lostHead = true;
            animator.SetBool("LostHead", true);
            head.SetActive(true);
        }
        //僵尸死亡逻辑
        if (currentHealth <= 0)
        {
            
            animator.SetTrigger("Die");
            isDie = true;
            //GameObject.Destroy(gameObject, 1f);
        }
    }

    public void DieAniOver()
    {
        animator.enabled = false;
        //GameManager.instance.ZombieDied(ganmeObject);
        GameObject.Destroy(gameObject);
    }
}
