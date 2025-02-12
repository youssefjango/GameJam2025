using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLight : MonoBehaviour, IWeapon
{
    [Header("Weapon Info")]
    [SerializeField] private WeaponInfo weaponInfo;

    [Header("Flashlight Settings")]
    public GameObject emittedLight;  // Reference to Light2D object
    public float flashMultiplier = 2f; // Multiplier for flashbang when pressing X
    public float angleExpandSpeed = 100f; // Speed of opening the flashlight angle
    public float maxLightAngle = 40f; // Maximum opening angle
    public float minLightAngle = 0f; // Minimum (starting) angle

    [Header("References")]
    public ActiveWeapon ActiveWeapon;
    public GameObject Active_Weapon;
    private Light2D lightFlashLight; // Light2D component
    private bool isLightOn = false;

    private void Awake()
    {
        maxLightAngle = 10 * StaticVariableManager.upgradeMp;
        Active_Weapon = GameObject.Find("Active Weapon");
        ActiveWeapon = Active_Weapon.GetComponent<ActiveWeapon>();

        lightFlashLight = emittedLight.GetComponent<Light2D>(); // Get Light2D component
        lightFlashLight.pointLightInnerAngle = minLightAngle; // Start at a small angle
        lightFlashLight.pointLightOuterAngle = minLightAngle;

        TurnOnLight(); // Light starts on
        StartCoroutine(ExpandFlashlight()); // Smoothly open the angle
    }
    public void Attack()
    {
        /*if (Time.time >= nextToggleTime)
        {
            ToggleLight();
        }*/
    }
    private void Update()
    {
        if (!isLightOn) return;

        ActiveWeapon.batteryAmount -= Time.deltaTime; // Decrease battery over time
        lightFlashLight.intensity = 5 * ActiveWeapon.batteryAmount / ActiveWeapon.gettotalbatteryAmount();

        if (ActiveWeapon.batteryAmount <= 0f)
        {
            TurnOffLight(); // Auto turn off if battery runs out
        }
    }

    private IEnumerator ExpandFlashlight()
    {
        while (lightFlashLight.pointLightInnerAngle < maxLightAngle)
        {
            lightFlashLight.pointLightInnerAngle += angleExpandSpeed * Time.deltaTime;
            lightFlashLight.pointLightOuterAngle += angleExpandSpeed * Time.deltaTime*1.5f;
            yield return null;
        }
        lightFlashLight.pointLightInnerAngle = maxLightAngle; // Ensure exact max value
    }

    public void FlashTrigger()
    {
        if (isLightOn)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        float originalIntensity = lightFlashLight.intensity;
        lightFlashLight.intensity = flashMultiplier * ActiveWeapon.gettotalbatteryAmount();
        ActiveWeapon.batteryAmount -= 3;

        yield return new WaitForSeconds(0.2f);

        while (lightFlashLight.intensity > originalIntensity)
        {
            lightFlashLight.intensity -= Time.deltaTime * 10f;
            yield return null;
        }
        lightFlashLight.intensity = originalIntensity;
    }

    private void ToggleLight()
    {
        if (isLightOn)
        {
            TurnOffLight();
        }
        else
        {
            TurnOnLight();
        }
    }

    private void TurnOnLight()
    {
        isLightOn = true;
        emittedLight.SetActive(true);
    }

    private void TurnOffLight()
    {
        isLightOn = false;
        emittedLight.SetActive(false);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void SetBatteryAmount(float batteryAmount)
    {
        ActiveWeapon.batteryAmount = batteryAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crystal"))
        {
            PlayerController.grewCrystal = true;
        }
    }
}
