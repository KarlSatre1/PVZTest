using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject darkBg;
    public GameObject progressBar;
    public  float waitTime;
    public int useSun;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        darkBg = transform.Find("dark").gameObject;
        progressBar = transform.Find("progress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }

    void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1- per;
    }

    void UpdateDarkBg()
    {
        if (progressBar.GetComponent<Image>().fillAmount == 0)
        {
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }
}
