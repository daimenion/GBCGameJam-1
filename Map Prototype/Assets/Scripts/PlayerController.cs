using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    //public
    public int CharacterNumber{ get; private set; }
    public int PlayerNumber { get; private set; }
    //private
    Player RewiredPlayer;
    float MoveSpeed;
    Vector3 moveDirection;
    Rigidbody rBody;

    enum PlayerNum
    {
        P1,
        P2,
        P3,
        P4
    }
    [SerializeField] PlayerNum playerNum;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 5;
        rBody = GetComponent<Rigidbody>();
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        //if (RewiredPlayer == null) {
        foreach (PlayerController pl in players)
        {
            if (pl != gameObject.GetComponent<PlayerController>())
            {
                if (CharacterNumber == pl.CharacterNumber)
                {
                    Random.Range(0, 4);
                }
                else
                {
                   
                }
            }
        }
        //}
        switch (playerNum)
        {
            case PlayerNum.P1:
                PlayerNumber = 0;
                break;
            case PlayerNum.P2:
                PlayerNumber = 1;
                break;
            case PlayerNum.P3:
                PlayerNumber = 2;
                break;
            case PlayerNum.P4:
                PlayerNumber = 3;
                break;
            default:
                break;
        }
        RewiredPlayer = ReInput.players.GetPlayer(PlayerNumber);
    }

    // Update is called once per frame
    void Update()
    {
        InputHandle();
    }

    void FixedUpdate()
    {
        rBody.velocity = moveDirection;
    }

    void SetCharacter() {
        switch (CharacterNumber) {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:
               
                break;
        }
    }

    void InputHandle() {
        moveDirection.x = RewiredPlayer.GetAxisRaw("horizontal");
        moveDirection.z = RewiredPlayer.GetAxisRaw("vertical");
        moveDirection = moveDirection.normalized * MoveSpeed;
    }
}
