using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float x;
    Vector2 move;
    Rigidbody2D rb;
    private Vector3 originalScale;
    public Animator anim;
    private bool isShooting = false;
    private bool isRunning = false;
    public bool isOnGround;
    public float jumpForce = 5.0f;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    public float runMultiplier = 1.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    private bool alreadyFired = false;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    private bool wasOnGround;

    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip electricShockSound;

    [Header("Ammo Settings")]
    public int maxAmmo = 32;
    private int currentAmmo;
    private bool isReloading = false;

    [Header("Shooting Conditions")]
    public bool canShootOnlyWhileRunning = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        originalScale = transform.localScale;
        anim = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
        wasOnGround = true;
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveHorizontal));

        float currentSpeed = speed;

        bool isMoving = Mathf.Abs(moveHorizontal) > 0.1f;
        bool isShiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isRunningInput = isMoving && isShiftHeld;

        if (isRunningInput)
        {
            currentSpeed *= runMultiplier;
            isRunning = true;
            anim.SetFloat("run", Mathf.Abs(moveHorizontal));
        }
        else
        {
            isRunning = false;
            anim.SetFloat("run", -1.0f);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        bool isShootingInput = Input.GetMouseButtonDown(0);

        if (isShootingInput && Time.time >= nextFireTime && !isReloading && currentAmmo > 0)
        {
            bool canShoot = true;
            string denyReason = "";

            if (canShootOnlyWhileRunning)
            {
                if (!isRunning)
                {
                    canShoot = false;
                    denyReason = "You can only shoot while running! (Shift + A/D)";
                }
                else if (!isMoving)
                {
                    canShoot = false;
                    denyReason = "You can only shoot while running! (Shift + A/D)";
                }
            }

            if (canShoot)
            {
                currentAmmo--;
                UpdateAmmoUI();

                isShooting = true;
                anim.SetFloat("shoot", Mathf.Abs(moveHorizontal));

                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log(denyReason);
                
            }
        }
        else if (isShootingInput && currentAmmo <= 0 && !isReloading)
        {
            Debug.Log("Out of ammo! Press R to reload.");
        }
        else
        {
            isShooting = false;
            anim.SetFloat("shoot", -1.0f);
            alreadyFired = false;
        }

        checkGround();

        if (isOnGround && !wasOnGround)
        {
            jumpsRemaining = maxJumps;
        }

        wasOnGround = isOnGround;

        if (Input.GetKeyDown(KeyCode.W) && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
            jumpsRemaining--;
        }

        Vector2 movement = new Vector2(moveHorizontal * currentSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void checkGround()
    {
        isOnGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }

    void Shoot()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        BulletController bulletScript = bullet.GetComponent<BulletController>();
        bulletScript.direction = transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        bulletScript.speed = bulletSpeed;
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        if (reloadSound != null)
        {
            AudioSource.PlayClipAtPoint(reloadSound, transform.position);
        }

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(1.5f);

        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        isReloading = false;
        Debug.Log("Reload complete! Ammo: " + currentAmmo);
    }
    void UpdateAmmoUI()
    {
        if (AmmoManager.Instance != null)
        {
            AmmoManager.Instance.SetAmmo(currentAmmo);
        }
    }
}