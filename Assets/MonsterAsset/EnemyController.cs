using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType
{
    Melee,
    Ranged
};

public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState curState = EnemyState.Wander;

    public EnemyType enemyType;

    public float range;
    public float speed;
    public float attackRange;
    public float coolDown;
    public float health;
    public static float healthAmount;
    
    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;

    private Vector2 randomDir;

    public GameObject bulletPrefab;

     private void Awake()
    {
        // Assign the value of public float to static float in Awake() method.
        healthAmount = health;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case(EnemyState.Wander):
                Wander();
                break;
            case(EnemyState.Follow):
                Follow();
                break;
            case(EnemyState.Die):
                break;
            case(EnemyState.Attack):
                Attack();
                break;
        }

        if (IsPlayerInRange(range) && curState != EnemyState.Die)
        {
            curState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && curState != EnemyState.Die)
        {
            curState = EnemyState.Wander;
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            curState = EnemyState.Attack;
        }

    }


    private bool IsPlayerInRange(float range)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator chooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * Random.Range(0.5f, 2.5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, nextRotation, t);
            yield return null;
        }
        chooseDir = false;
    }   

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(chooseDirection());
        }

        transform.position += transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range))
        {
            curState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if(!coolDownAttack)
        {
            switch(enemyType)
            {
                case(EnemyType.Melee):
                    GameController.DamagedPlayer(1);
                    StartCoroutine(CoolDown());
                    break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        health = health - BulletController.damage;
        if (health <= 0)
        {
        Destroy(gameObject);
        }
    }

}