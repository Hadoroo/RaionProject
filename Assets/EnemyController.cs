using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die
};

public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState curState = EnemyState.Wander;

    public float range;
    public float speed;
    
    private bool chooseDir = false;
    private bool dead = false;
    private Vector2 randomDir;


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
        }

        if (IsPlayerInRange(range) && curState != EnemyState.Die)
        {
            curState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && curState != EnemyState.Die)
        {
            curState = EnemyState.Wander;
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
        randomDir = new Vector3(0, 0, Random.Range(0,360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
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

    public void Death()
    {
        Destroy(gameObject);
    }

}
