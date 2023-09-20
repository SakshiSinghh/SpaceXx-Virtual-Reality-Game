//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description:
 * Player health updater
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
  
    public float Health_amt;
   
    public Image Health_Img;
    public GameObject HealthBar_Canvas;
    public float HealthMax_amt;
    public TextMeshProUGUI healthText;
   

    void Start()
    {
        
        UpdateHealthText();
        updateHealthImg();
       

    }

    public void Damage(float _dmg, Vector3 _pos)
    {
        Health_amt -= _dmg;
        UpdateHealthText();
        updateHealthImg();
        
        //calculating the fillAmount 
        
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Health_amt <= 0)
        {
            //then die

            
            SceneManager.LoadScene("GameOver");



        }
    }

    public void UpdateHealthText()
    {
        // Ensure the healthText field is assigned in the Inspector
        if (healthText != null)
        {
            healthText.text =Health_amt.ToString();
          
        }

       
    }

    public void updateHealthImg()
    {
        Health_Img.fillAmount = Health_amt / HealthMax_amt;
    }
    
}
