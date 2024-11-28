
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityUI: MonoBehaviour
{

   
    const string playerTag = "Player";

    [SerializeField] int _maxSliderValue = 100;
    [SerializeField] int _originalSliderValue;
    IInvincibility _ability;
  
   bool _deactivate = false;
   private Transform _ui;
     float timer = 2f;
    void Start()
    {
        _ui = this.gameObject.transform.GetChild(0);
        _ui.gameObject.SetActive(false);
      _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value = _maxSliderValue;
    }
    
        
      public void SetActive()
    {
       _ui.gameObject.SetActive(true);
       _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value  = _maxSliderValue;
       _originalSliderValue = _maxSliderValue;
    }

    public void ReduceSliderValue(int dmg)
    {
         _originalSliderValue  -= dmg * 10;

       
         
         if(_originalSliderValue < 50)
            _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value -= Mathf.Clamp(_originalSliderValue,0,50);
        if(_originalSliderValue < 2)
            _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value -= Mathf.Clamp(_originalSliderValue,0,2);
         if(_originalSliderValue < 1) 
         {
            _originalSliderValue = 0;
            StartCoroutine(Deactivate());
            
             
         }
    }

     System.Collections.IEnumerator Deactivate()
     {
        yield return new WaitForSeconds(5f);
        _ui.gameObject.SetActive(false);
     }
    public int GetSliderValue () => _originalSliderValue;

   
    
}

