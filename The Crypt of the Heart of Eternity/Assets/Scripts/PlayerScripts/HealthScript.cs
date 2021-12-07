using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{

    [SerializeField] private GameObject healthball;
    [SerializeField] private GameObject speedBall;
    [SerializeField] private GameObject jumpBall;
    [SerializeField] private GameObject damageball;


    [SerializeField] private GameObject greenBeatle;
    [SerializeField] private GameObject purplebeetle;
    [SerializeField] private GameObject redbeetle;
    [SerializeField] private GameObject skeleton;


    [SerializeField] private GameObject diamond;
    public int healBallProbability=2;
    public int speedBallProbablity =1;
    public int jumpBallProbability =1;
    public int damageBallProbability =1;

    private bool summon = true;

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;
    private float half;

    public bool is_Player, is_licht, is_Skeleton;

    public bool is_Dead;

    private PlayerStates Player_states;

    [SerializeField]
    private GameObject win, lose;

    void Awake()
    {
        half = health / 2;
       if(is_licht || is_Skeleton)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        if (is_Player)
        {
            Player_states = GetComponent<PlayerStates>();
        }
    }


    void Start()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
        {
            return;
        }

        health -= damage;
        if (is_Player)
        {
            //displaye health
            Player_states.Display_HealthStats(health);
        }

        if(is_licht|| is_Skeleton)
        {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }

            if(is_licht && summon && health <= half)
            {

                navAgent.velocity = Vector3.zero;
                navAgent.isStopped = true;
                enemy_Controller.enabled = false;

                enemy_Anim.Summon();
                summon = false;
                Invoke("Summons", 1f);

                


            }
        }

        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayerDied()
    {
        if (is_licht || is_Skeleton)
        {
            if (is_licht)
            {
                //win.SetActive(true);
                Vector3 position = new Vector3(90f,3f,-26f);
                //position.y += 1;
                //Quaternion rotate = transform.rotation;
                //Quaternion.AngleAxis

                Instantiate(diamond, position, Quaternion.AngleAxis(-90, Vector3.right));


            }
            

            if (is_Skeleton)
            {
                int total = healBallProbability + speedBallProbablity + damageBallProbability + jumpBallProbability;
                //Random rnd = new Random();
                int newball = Random.Range(1, total+1);
                //print(newball);
                Vector3 position = transform.position;
                position.y += 1;
                if (newball <= healBallProbability)
                {
                    
                    Instantiate(healthball, position, Quaternion.identity);
                }
                else if (newball<=healBallProbability+speedBallProbablity)
                {
                    Instantiate(speedBall, position, Quaternion.identity);
                }
                else if (newball <= healBallProbability + speedBallProbablity + jumpBallProbability)
                {
                    Instantiate(jumpBall, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(damageball, position, Quaternion.identity);
                }
            }
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();
            //may remove
            Invoke("Remove", 3f);
        }

        if (is_Player)
        {
            //is_Player = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            //print(enemies.Length);
            for(int i = 0; i<enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
                enemies[i].SetActive(false);
            }
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            lose.SetActive(true);

        }

        if(tag == Tags.PLAYER_TAG)
        {
            //Invoke("RestartGame",3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Demo");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HealthBall" && tag ==Tags.PLAYER_TAG)
        {

            print(other.gameObject);
            health += 10f;
            Player_states.Display_HealthStats(health);
            other.gameObject.SetActive(false);
        }
    }


    void resume()
    {
        //navAgent.velocity = Vector3.zero;
        navAgent.isStopped = false;
        enemy_Controller.enabled = true;
    }

    void Summons()
    {

        Vector3 pos = transform.position;
        pos.y += 1;
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(1, 5);
            if (ran == 1)
            {

                Instantiate(skeleton, Random.insideUnitSphere * 5 + pos, Quaternion.identity);
            }
            if (ran == 2)
            {
                Instantiate(greenBeatle, Random.insideUnitSphere * 5 + pos, Quaternion.identity);
            }
            if (ran == 3)
            {
                Instantiate(redbeetle, Random.insideUnitSphere * 5 + pos, Quaternion.identity);
            }
            if (ran == 4)
            {
                Instantiate(purplebeetle, Random.insideUnitSphere * 5 + pos, Quaternion.identity);
            }

            //navAgent.isStopped = false;
            Invoke("resume", 3f);
        }
    }
    void Remove()
    {
        Destroy(this);
    }
    //may remove
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
