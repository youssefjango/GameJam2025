using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public ActiveWeapon ActiveWeapon;
    public GameObject Active_Weapon;
    public PlayerController PlayerController;
    public GameObject Player;
    AudioManager audioManager;

    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    public float currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    const string TOWN_TEXT = "Town";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    void Awake() {
        maxHealth = StaticVariableManager.upgradeHp;
        Active_Weapon = GameObject.Find("Active Weapon");
        ActiveWeapon = Active_Weapon.GetComponent<ActiveWeapon>();
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();

        ActiveWeapon = GetComponent<ActiveWeapon>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        StaticVariableManager.isDead = false; 
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        if (other.gameObject.CompareTag("AiEnemy"))
        {
            TakeDamage(1, other.gameObject.transform);
        }
        if (enemy) {
            TakeDamage(1, other.transform);
        }
        
    }

    public void TakeDamage(int damageAmount, Transform hitTransform) {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        CheckIfPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private void CheckIfPlayerDeath()
    {
        if(currentHealth <= 0 && !StaticVariableManager.isDead)
        {
            StaticVariableManager.score = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            audioManager.PlaySFX(audioManager.death);
            Invoke("deathdelay", 1);
            
        }
    }
    void deathdelay()
    {
        StaticVariableManager.isDead = true;
        Destroy(Active_Weapon.gameObject);
        currentHealth = 0;
        
        StartCoroutine(DeathLoadSceneRoutine());
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

}
