using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float damage = 15f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    //对触碰到的僵尸造成伤害
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            other.GetComponent<ZombieNormal>().ChangeHealth(-damage);
            GameObject.Destroy(gameObject);
        }
    }
}
