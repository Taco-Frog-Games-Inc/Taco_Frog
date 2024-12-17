using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour {

    [SerializeField] private GameObject player;
    private static PlayerController playerCtrl;

    private static TextMeshProUGUI moneyText;
    private static TextMeshProUGUI userText;

    public static Dictionary<ItemName, Action> itemActions =
        new(){{ItemName.ExtraTongue, ExtraTongue_Action },
              {ItemName.ExtraJump, ExtraJump_Action },
              {ItemName.ExtraSpeed, ExtraSpeed_Action },
        };

    private void Start() {
        playerCtrl = player.GetComponent<PlayerController>();
        moneyText = gameObject.transform.parent.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        userText = gameObject.transform.parent.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        moneyText.text = playerCtrl.playerData.moneyValue.ToString();
    }

    static void ExtraJump_Action() { SetAction((int)ItemValue.ExtraJump, 3, "jumpHeight"); }

    static void ExtraTongue_Action() { SetAction((int)ItemValue.ExtraTongue, 5, "tongueAttackLength"); }

    static void ExtraSpeed_Action() { SetAction((int)ItemValue.ExtraSpeed, 5, "speed"); }

    private static void SetAction(int val, int toIncrease, string prop) {
        if (playerCtrl.playerData.hasPressedPause) {
            userText.text = "";
            if (playerCtrl.playerData.moneyValue >= val) {                

                FieldInfo property = typeof(PlayerData).GetField(prop);
                if (property != null) {
                    var initPropVal = property.GetValue(playerCtrl.playerData);
                    int res = Convert.ToInt32(initPropVal) + toIncrease;
                    property.SetValue(playerCtrl.playerData, res);
                    playerCtrl.playerData.moneyValue -= val;
                }

                else throw new Exception("This property does not exists on type PlayerData");

                playerCtrl.SetPlayerStats();
                moneyText.text = playerCtrl.playerData.moneyValue.ToString();
            }
            else userText.text = "Not enough gold";            
        }
    }

}