using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject win;

    private CharacterController character_controller;
    private Vector3 move_Direction;
    public float speed = 5f;
    private float gravity = 20f;
    public float jump_Force = 10f;
    private float vertical_Velocity;
    public float speedbooster = 2;
    public float jumpbooster = 2;
    public bool boosted = false;
    [SerializeField]
    private GameObject JumpBoost;

    [SerializeField]
    private GameObject SpeedBoost;

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //A-D keys are horizontal move
        //W-S keys are for vertical move
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        move_Direction = transform.TransformDirection(move_Direction);
        //multiply by time.deltime to smoothen it out otherwise number is too large
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();
        character_controller.Move(move_Direction);
    
    }

    void ApplyGravity()
    {

        vertical_Velocity -= gravity * Time.deltaTime;

        //jump
        PlayerJump();

        move_Direction.y = vertical_Velocity * Time.deltaTime;

    }

    void PlayerJump()
    {
        //only jump if on the ground and space is entered
        //getkeydown dectects if key is pressed amd will only provide true once if the key is held down
        if (character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpeedBall")
        {

            other.gameObject.SetActive(false);
            speed = speed * speedbooster;
            boosted = true;
            SpeedBoost.SetActive(true);
            Invoke("SpeedDown", 15f);

        }
        if (other.gameObject.tag == "JumpBall")
        {

            other.gameObject.SetActive(false);
            jump_Force = jump_Force * jumpbooster;
            JumpBoost.SetActive(true);
            Invoke("JumpDown", 15f);

        }

        if(other.gameObject.tag == "Diamond")
        {

            TimerController.instance.EndTimer();
            win.SetActive(true);
            other.gameObject.SetActive(false);
            if (win)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
                //print(enemies.Length);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EnemyController>().enabled = false;
                    enemies[i].SetActive(false);
                }
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
                
            }
        }

    }

    void SpeedDown()
    {
        speed /= speedbooster;
        SpeedBoost.SetActive(false);
        boosted = false;
    }

    void JumpDown()
    {
        jump_Force /= jumpbooster;
        JumpBoost.SetActive(false);
    }
}
