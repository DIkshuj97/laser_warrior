using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0f;
    [SerializeField] int basehealth = 900;
    [SerializeField] AudioClip deathSound;
    [SerializeField] int lives =5;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] public bool canTripleShot = false;
    [SerializeField] public bool shieldsActive = false;

    [Header("Powerups")]
    [SerializeField]  GameObject tripleShotPrefab;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject shieldGameObject;
    [SerializeField] GameObject Partner;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    int health;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SetUpMoveBoundaries();
            health = (int)(basehealth - (OptionController.GetDifficulty() * 166));
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Move();
            Fire();
            TiltMove();
            lives = Mathf.Abs(health / 166);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if(shieldsActive==true)
        {
            shieldsActive = false;
            shieldGameObject.SetActive(false);
            return;
        }

        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0 && lives==0)
        {
            Die();
        }
    }

    private void Die()
    {
        float deathVolume = Mathf.Clamp(OptionController.GetMasterVolume(), 0f, deathSoundVolume);
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
    }

    public float GetLives()
    {
        return lives;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")|| Input.GetMouseButtonDown(0))
        {
           firingCoroutine= StartCoroutine(FireContinuously());
        }

        if(Input.GetButtonUp("Fire1") || Input.GetMouseButtonUp(0))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        float shootVolume = Mathf.Clamp(OptionController.GetMasterVolume(), 0f, shootSoundVolume);
        while (true)
        {
            if (canTripleShot == true)
            {
               GameObject  laser = Instantiate(tripleShotPrefab, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }

            else if (canTripleShot == false)
            {
               GameObject  laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }

            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    public void healthpickup()
    {
        health += 150;
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void EnableShields()
    {
        shieldsActive = true;
        shieldGameObject.SetActive(true);
    }

    public void EnablePartner()
    {
        Instantiate(Partner, transform.position, Quaternion.identity);
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime*moveSpeed;
        var newxPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newyPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector2(newxPos, newyPos);
    }

    private void TiltMove()
    {
        var deltaX = Input.acceleration.x* Time.deltaTime * moveSpeed;
        var newxPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.acceleration.y * Time.deltaTime * moveSpeed;
        var newyPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newxPos, newyPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
