using UnityEngine;

public class Item : MonoBehaviour {

    public ItemName itemName;

    public void OnSelection_Click() { 
        Store.itemActions.TryGetValue(itemName, out var action);
        action();                
    }
    
}

public enum ItemName { 
    ExtraTongue,
    ExtraJump,
    ExtraHealth,
    ExtraSpeed//This one requires UI adjustment.
}

public enum ItemValue { 
    ExtraTongue = 50,
    ExtraJump = 40,
    ExtraSpeed = 50,
    ExtraHealth = 1
}
