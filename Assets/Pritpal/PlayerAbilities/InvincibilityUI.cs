
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityUI: MonoBehaviour, IUISubscriber
{

    //[SerializeField] int _jumpBoost = 35f;
   // _playerPrefab;
    const string playerTag = "Player";

    IInvincibility _ability;
   // [SerializeField] Spear _spear;
   // [SerializeField] Projectile _projectile;
   bool setToActive = false;
   private Transform _ui;
    void Start()
    {
        _ui = this.gameObject.transform.GetChild(0);
        _ui.gameObject.SetActive(false);
       // this.gameObject.SetActive(false);;
    }
    
        
    public bool IsActive() => setToActive;
   
    public void UpdateUI(bool setActive, IUISubscriber sub)
    {
        setToActive = setActive;
        Debug.Log("UpdateUI Called!!!");
        if(setToActive)
        {
            Debug.Log($"Set to active;{setToActive}");
            _ui.gameObject.SetActive(true);
           _ui.GetChild(0).GetChild(0).GetComponent<Slider>().value = 100;
        }   
       
    }
}

