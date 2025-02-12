using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private Transform weaponCollider;
    public ActiveWeapon ActiveWeapon;
    public GameObject Active_Weapon;
    public PlayerController PlayerController;
    public GameObject Player;

    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;
    private bool firstGen = true;
    void Awake() {
        firstGen = true;
        playerControls = new PlayerControls();
    }

    private void Start() {
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        weaponCollider = PlayerController.GetWeaponCollider();
        Active_Weapon = GameObject.Find("Active Weapon");
        ActiveWeapon = Active_Weapon.GetComponent<ActiveWeapon>();
        if (ActiveWeapon == null)
        {
            ActiveWeapon = FindObjectOfType<ActiveWeapon>();
            EquipStartingWeapon();
            if (ActiveWeapon == null)
            {EquipStartingWeapon();
                
                Debug.LogError("ActiveWeapon component not found in the scene!");
            }
        }
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(4);
    }

    private void ToggleActiveSlot(int numValue) {
        if ((numValue - 1) == 1)
        {
            if (PlayerController.weapons.Contains("FlashLight"))
            {
                ToggleActiveHighlight(numValue - 1);
            }
        }
        else if ((numValue - 1) == 2)
        {
            if (PlayerController.weapons.Contains("Lantern"))
            {
                ToggleActiveHighlight(numValue - 1);
            }
        }
        else
        {
            ToggleActiveHighlight(numValue - 1);
        }

    }

    private void ToggleActiveHighlight(int indexNum) {
        activeSlotIndexNum = indexNum;

        /*foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }*/

        //this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        // Validate ActiveWeapon is initialized
        if (ActiveWeapon == null)
        {
            Debug.LogError("ActiveWeapon is null, cannot spawn a new weapon.");
            return;
        }

        // Ensure activeSlotIndexNum is valid
        if (activeSlotIndexNum < 0 || activeSlotIndexNum >= transform.childCount)
        {
            Debug.LogError("activeSlotIndexNum is out of bounds.");
            ActiveWeapon.WeaponNull();
            return;
        }

        // Ensure the child has an InventorySlot
        InventorySlot slot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>();
        if (slot == null)
        {
            Debug.LogError("InventorySlot not found in the child.");
            ActiveWeapon.WeaponNull();
            return;
        }

        // Ensure the slot has weapon info
        WeaponInfo weaponInfo = slot.GetWeaponInfo();
        if (weaponInfo == null)
        {
            Debug.LogWarning("No weapon info found in the InventorySlot.");
            ActiveWeapon.WeaponNull();
            return;
        }

        // Check if weaponPrefab is valid
        if (weaponInfo.weaponPrefab == null)
        {
            Debug.LogError("Weapon prefab is missing in WeaponInfo.");
            return;
        }
        if (ActiveWeapon.CurrentActiveWeapon != null && ActiveWeapon.CurrentActiveWeapon.name != (weaponInfo.weaponPrefab.name))
        {
            Destroy(ActiveWeapon.CurrentActiveWeapon.gameObject);
            weaponCollider.gameObject.SetActive(false);
            // Spawn and assign the new weapon
            GameObject newWeapon = Instantiate(weaponInfo.weaponPrefab, ActiveWeapon.transform);
            newWeapon.name = weaponInfo.weaponPrefab.name;
            Debug.Log(weaponInfo.weaponPrefab.name + "(Clone)");
            Debug.Log(ActiveWeapon.CurrentActiveWeapon.name);
            ActiveWeapon.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        }
        if(firstGen == true)
        {
            weaponCollider.gameObject.SetActive(false);
            // Spawn and assign the new weapon
            GameObject newWeapon = Instantiate(weaponInfo.weaponPrefab, ActiveWeapon.transform);
            newWeapon.name = weaponInfo.weaponPrefab.name;
            ActiveWeapon.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
            firstGen = false;
        }

        
    }
    

}
