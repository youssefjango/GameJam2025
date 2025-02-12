using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject Player;

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private float currentHealth;
    private Knockback knockback;
    private Flash flash;
    AudioManager audioManager;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        audioManager.PlaySFX(audioManager.hitEnemy);
        currentHealth -= damage+(StaticVariableManager.upgradeDmg-1f);
        knockback.GetKnockedBack(PlayerController.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
