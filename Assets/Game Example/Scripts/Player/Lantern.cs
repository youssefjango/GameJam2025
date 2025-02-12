using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour, IWeapon
{
    public ActiveWeapon ActiveWeapon;
    public GameObject Active_Weapon;
    public PlayerController PlayerController;
    public GameObject Player;
    [SerializeField] private WeaponInfo weaponInfo;

    public GameObject light2D; // Reference to the Light 2D object

    private void Awake() {
        Active_Weapon = GameObject.Find("Active Weapon");
        ActiveWeapon = Active_Weapon.GetComponent<ActiveWeapon>();
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
    }

    private void Update() {
        MouseFollowWithOffset();
        // Countdown the timer if the light is on
    }


    public void Attack() {
    }


    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
