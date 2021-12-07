using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{

    private AudioSource footStep_Sound;

    [SerializeField]
    private AudioClip[] footStep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distence;

    void Awake()
    {
        footStep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootStepSound();
    }

    void CheckToPlayFootStepSound()
    {

        //if we are not on the ground return
        if (!character_Controller.isGrounded)
            return;

        //on ground test moving
        if (character_Controller.velocity.sqrMagnitude > 0)
        {

            //accumulated distance is the value how far we can go
            //eg make step or sprint, move while crouching
            //until we play the footstep sound
            accumulated_Distance += Time.deltaTime;
            if(accumulated_Distance > step_Distence)
            {
                footStep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footStep_Sound.clip = footStep_Clip[Random.Range(0, footStep_Clip.Length)];
                footStep_Sound.Play();

                accumulated_Distance = 0f;
            }

        }
        else
        {
            accumulated_Distance = 0f;
        }
    }
}
