using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public LoadingScreenManager loadingScreenManager;

    public PlayerHealth playerHealth;
    public ActiveInventory ActiveInventory;
    public bool FacingLeft { get { return facingLeft; } }

    public List<string> weapons = new List<string>();
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;


    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private Knockback knockback;
    private float startingMoveSpeed;
    private bool start;

    private bool facingLeft = false;
    private bool isDashing = false;
    AudioManager audioManager;

    public static bool grewCrystal = false;
    [SerializeField]private AudioSource audioSource;
    private int points;
    void Awake() {
        StaticVariableManager.timer = 0f;
        grewCrystal = false;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        start = false;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        points = StaticVariableManager.score;
        playerControls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;

        //ActiveInventory.EquipStartingWeapon();
        playerControls.Disable();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }


    private void Update() {
        string sceneName = SceneManager.GetActiveScene().name;
        StaticVariableManager.timer += Time.deltaTime;

        if (loadingScreenManager.gameStart && !start&& sceneName == "Scene1") 
        {
            audioManager.PlaySFX(audioManager.Falling);
            myAnimator.SetTrigger("Fall");
            playerControls.Enable();
            start = true;
        }
        else if(sceneName == "Town" && !start)
        {
            playerControls.Enable();
            start = true;
        }
        PlayerInput();
    }

    private void FixedUpdate() {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider() {
        return weaponCollider;
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move() {
        if (knockback.GettingKnockedBack || (playerHealth.currentHealth < 1)) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) {
            mySpriteRender.flipX = true;
            facingLeft = true;
        } else {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash() {
        if (!isDashing) {
            myAnimator.SetBool("Sneaking", true);
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            //StartCoroutine(EndDashRoutine());
        }
        else {
            myAnimator.SetBool("Sneaking", false);
            moveSpeed = startingMoveSpeed;
            myTrailRenderer.emitting = false;
            isDashing = false;
        }
    }

    private IEnumerator EndDashRoutine() {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole")){
            playerControls.Disable();
        }
        if(collision.CompareTag("Magic Ring"))
        {
            if(StaticVariableManager.score-points >= 2)
            {
                playerControls.Disable();
                myAnimator.SetTrigger("Rise");
                Invoke("Endgame", 0.3f);
            }
        }
        if (collision.CompareTag("FlashLightItem"))
        {
            audioSource.Play();
            weapons.Add("FlashLight");
            Destroy(collision.gameObject);

        }
        if (collision.CompareTag("LanternItem"))
        {
            audioSource.Play();
            weapons.Add("Lantern");
            Destroy(collision.gameObject);
        }
    }
    void Endgame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
