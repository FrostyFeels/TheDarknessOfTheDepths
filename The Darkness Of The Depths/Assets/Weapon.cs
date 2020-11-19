using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public RangedWeaponStats weaponStats;

    public int damage;
    public int headshotDamage;
    public int piercing;
    public int range;
    public int ammo;
    public int maxAmmo;
    public int numberOfShots;
    public int lifeSteal;

    public float AS;
    public float reloadSpeed;
    public float minSpread;
    public float maxSpread;
    public float knockback;
    public float bulletSpeed;
    public float shieldDamage;
    public float armorPen;

    public bool autoShooting;

    public float nextTimeToFire;

    private float angle;

    public bool reloading;
    public Vector2 dir;

    public GameObject bulletPrefab;
    Rigidbody2D rb;

    Transform cursor;
    Transform weapon;
    PlayerMovement movement;

    


    // Start is called before the first frame update
    void Start()
    {
        damage = weaponStats.dmg;
        headshotDamage = weaponStats.headShotDamage;
        piercing = weaponStats.piercing;
        armorPen = weaponStats.armorPen;
        range = weaponStats.range;
        ammo = weaponStats.ammo;
        maxAmmo = ammo;
        numberOfShots = weaponStats.NumberOfShots;
        lifeSteal = weaponStats.lifeSteal;

        AS = weaponStats.fireRate;
        reloadSpeed = weaponStats.reloadSpeed;
        minSpread = weaponStats.minSpread;
        maxSpread = weaponStats.maxSpread;
        knockback = weaponStats.knockback;
        bulletSpeed = weaponStats.BulletSpeed;
        shieldDamage = weaponStats.shieldDmg;
        armorPen = weaponStats.armorPen;

        autoShooting = weaponStats.autoShooting;

        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        cursor = GameObject.Find("Cursor").GetComponent<Transform>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        weapon = gameObject.GetComponent<Transform>();
     
    }

    private void OnEnable()
    {
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (cursor.transform.position - transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (reloading)
             return;


        if (ammo <= 0 || (ammo < maxAmmo && Input.GetKeyDown(KeyCode.R)))
        {
            StartCoroutine(Reloading());
        }
        if (!autoShooting)
        {
            if (Input.GetMouseButtonDown(0) && nextTimeToFire < Time.time)
            {
                nextTimeToFire = Time.time + (1 / AS);
                Shooting();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && nextTimeToFire < Time.time)
            {
                nextTimeToFire = Time.time + (1 / AS);
                Shooting();
            }
        }

   
    }

    void Shooting()
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, weapon.position, Quaternion.Euler(0f, 0f, angle + 90f));
            bullet.transform.Rotate(0f, 0f, Random.Range(minSpread, maxSpread));
            Bullet bulletStats = bullet.GetComponent<Bullet>();
            bulletStats.speed = bulletSpeed;
        }

        ammo--;
        if(dir.x > 0)
        {
            movement.direction = -1;
            Debug.Log("UwU");
        }
        if(dir.x < 0)
        {
            Debug.Log("OwO");
            movement.direction = 1;
        }
        rb.velocity = new Vector2(rb.velocity.x, -dir.y * knockback);
        movement.currentSpeed = dir.x * knockback;
        if(movement.currentSpeed < 0)
        {
            movement.currentSpeed = -dir.x * knockback;
        }
        
        
        
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity;
    }
    IEnumerator Reloading()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        ammo = maxAmmo;
        reloading = false;
       
    }
}
