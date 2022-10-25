using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Noise noiseUI;
    public Animator animatorModel;

    private CharacterController controller;
    private BoxCollider boxCollider;
    private float speed;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        speed = GameSettings.playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.isPause == false)
        {
            Vector3 move = MoveVector();

            if (move != Vector3.zero)
            {
                controller.Move(move * Time.deltaTime * speed);
                gameObject.transform.forward = move;
                noiseUI.StartIncreaseNoise();
                animatorModel.SetInteger("State", 1);
            }
            else
            {
                noiseUI.StopIncreaseNoise();
                animatorModel.SetInteger("State", 0);
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") == true)
        {
            Global.game.ShowEnd(false);
            animatorModel.SetInteger("State", 2);
        }
        else if (collider.gameObject.CompareTag("EndLevel") == true)
        {
            Global.game.ShowEnd(true);
            animatorModel.SetInteger("State", 0);
        }
    }
    private Vector3 MoveVector()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            move += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            move += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            move += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            move += new Vector3(1, 0, 0);
        }
        return move;
    }
}
