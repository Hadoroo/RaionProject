using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator anim;

    Vector2 movement;

    public GameObject bulletPrefab;

    public float moveSpeed = 5f;
    public float BulletSpeed;
    private float lastFire;
    public float fireDelay;

    public Text collectedText;
    public static int collectedAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        fireDelay = GameController.FireRate;
        moveSpeed = GameController.MoveSpeed;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");

        if ((shootHor != 0  || shootVer != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;

        }

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

        

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        collectedText.text = "Items Collected: " + collectedAmount;
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
        (x < 0) ? Mathf.Floor(x) * BulletSpeed : Mathf.Ceil(x) * BulletSpeed,
        (x < 0) ? Mathf.Floor(y) * BulletSpeed : Mathf.Ceil(y) * BulletSpeed
    );
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(x, y, 0));
        bullet.transform.rotation = rotation;
    }
}
