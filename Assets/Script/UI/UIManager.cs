using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //单例模式
    public static UIManager instance;
    public Text sunNumText;
    // Start is called before the first frame update
   
   void Awake() 
   {
        instance = this;
  
   }

    void Start()
    {
        

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUI() //游戏开始之初读取GameManager中的sunNum
    {
        sunNumText.text = GameManager.instance.sunNum.ToString();
    }

    public void UpdateUI()
    {
        sunNumText.text = GameManager.instance.sunNum.ToString();
    }
}
