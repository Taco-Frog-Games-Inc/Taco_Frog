
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class InvincibilityUI: MonoBehaviour
{

   
    const string playerTag = "Player";

    [SerializeField] int _maxSliderValue = 100;
    [SerializeField] int _originalSliderValue;
    [SerializeField] float _deactivationCountDown = 5f;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _gulpClip, _powerReduce, _deactivate;
    IInvincibility _ability;
  
   
   private Transform _ui;
     
    
    void Awake()
    {
       _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        _ui = this.gameObject.transform.GetChild(0);
        _ui.gameObject.SetActive(false);
      _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value = _maxSliderValue;
    }
    
        
      public void SetActive()
    {
       _audioSource.PlayOneShot(_gulpClip);
       _ui.gameObject.SetActive(true);
       _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value  = _maxSliderValue;
       _originalSliderValue = _maxSliderValue;
    }

    public void ReduceSliderValue(int dmg)
    {
         _originalSliderValue  -= dmg * 10;
          _audioSource.PlayOneShot(_powerReduce);

         if(_originalSliderValue < 50)
            _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value -= Mathf.Clamp(_originalSliderValue,0,50);

        if(_originalSliderValue < 2)
            _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value -= Mathf.Clamp(_originalSliderValue,0,2);

         if(_originalSliderValue < 1) 
         {
           _audioSource.Stop();
            _originalSliderValue = 0;
            StartCoroutine(Deactivate());
            
             
         }
           
    }

     System.Collections.IEnumerator Deactivate()
     {
        yield return new WaitForSeconds(_deactivationCountDown);
        
        _audioSource.PlayOneShot(_deactivate);
        _ui.gameObject.SetActive(false);
        
     }
    public int GetSliderValue () => _originalSliderValue;

   
    
}

