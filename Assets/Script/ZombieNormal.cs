using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1,0, 0);
    public float speed = 1f;
    private bool isWalk;
    private Animator animator;
    public float damage;
    public float damageInterval = 1f;
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
                //todo:对植物造成伤害
                Peashooter peashooter = other.GetComponent<Peashooter>();
                float newHealth = peashooter.ChangeHealth(-damage);
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
}
