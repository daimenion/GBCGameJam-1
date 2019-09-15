using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    //public
    public int CharacterNumber{ get; private set; }
    public int PlayerNumber { get; private set; }
    [SerializeField] PlayerNum playerNum;
    //private
    float MoveSpeed;
    bool start;
    Player RewiredPlayer;
    Vector3 moveDirection;
    Rigidbody rBody;
    

    enum PlayerNum
    {
        P1,
        P2,
        P3,
        P4
    }
    PlayerController[] players;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 5;
        rBody = GetComponent<Rigidbody>();
        players = FindObjectsOfType<PlayerController>();

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
        CharacterNumber = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            foreach (PlayerController pl in players)
            {
                if (pl.tag != tag)
                {
                    if (CharacterNumber == pl.CharacterNumber)
                    {
                        CharacterNumber = Random.Range(0, 4);
                    }
                }
            }
            SetCharacter();
            StartCoroutine("CharacterSeted");
        }
        InputHandle();
    }

    void FixedUpdate()
    {
        rBody.velocity = moveDirection;
    }

    void SetCharacter() {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 100.5f, 0);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 100.5f, 0);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 100, 0);

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 100, 0);

        switch (CharacterNumber) {
            case 0:
                GetComponent<MeshFilter>().mesh = cube.GetComponent<MeshFilter>().mesh;
                break;
            case 1:
                GetComponent<MeshFilter>().mesh = sphere.GetComponent<MeshFilter>().mesh;
                break;
            case 2:
                GetComponent<MeshFilter>().mesh = capsule.GetComponent<MeshFilter>().mesh;
                break;
            case 3:
                GetComponent<MeshFilter>().mesh = cylinder.GetComponent<MeshFilter>().mesh;
                break;
        }
        //after the mesh is set destroy the created ones 
        Destroy(cube);
        Destroy(sphere);
        Destroy(capsule);
        Destroy(cylinder);

    }
    IEnumerator CharacterSeted() {
        yield return new WaitForSeconds(0.3f);
        start = true;
        StopAllCoroutines();
    }
    void InputHandle() {
        moveDirection.x = RewiredPlayer.GetAxisRaw("horizontal");
        moveDirection.z = RewiredPlayer.GetAxisRaw("vertical");
        moveDirection = moveDirection.normalized * MoveSpeed;
    }
}
