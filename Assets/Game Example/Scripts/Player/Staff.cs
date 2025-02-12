using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public ActiveWeapon ActiveWeapon;
    public GameObject Active_Weapon;
    public PlayerController PlayerController;
    public GameObject Player;
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        Active_Weapon = GameObject.Find("Active Weapon");
        ActiveWeapon = Active_Weapon.GetComponent<ActiveWeapon>();
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        MouseFollowWithOffset();
    }


    public void Attack() {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnStaffProjectileAnimEvent() {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
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
