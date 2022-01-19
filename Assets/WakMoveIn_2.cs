using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WakMoveIn_2 : MonoBehaviour
{
    public Transform RayPoint;
    //public Transform HoldSightPoint;

    public float moveSpeed = 10.0f;
    public float Accel = 1.0f;//스피드업
    public float rotationSpeed = 500.0f;
    private Rigidbody body;

    bool isBorder;
    public bool isChoping=false;//food로부터 썰때 값이 true로 바뀌도록 받아옴

    

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
    }

    void StopToWall()
    {
        Debug.DrawRay(RayPoint.position, transform.forward*3, Color.green);
        isBorder = Physics.Raycast(RayPoint.position, transform.forward, 3, LayerMask.GetMask("Wall"));
    }

    void SpeedUp()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Accel = 2.5f;
        }
        else
        {
            Accel = 1.0f;
        }
    }
    

    void MoveForward()
    {
        if (!isBorder && !isChoping)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(0, 0, moveSpeed * Accel * Time.deltaTime);
            }
        }
    }

    void MoveTurn()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);
        direction.Normalize();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (direction != Vector3.zero && !isChoping)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void Freeze()
    {
        body.angularVelocity = Vector3.zero;
        body.velocity = Vector3.zero;
    }


    /*
    public void Pickup(GameObject Food)
    {
        SetEquip(Food, true);
    }

    void Drop()
    {
        GameObject Food = playerEquipPoint.GetComponentInChildren<Rigidbody>().gameObject;
        SetEquip(Food, false);
    }

    void SetEquip(GameObject Food, bool isEquip)
    { 

    }
    */

    

    void FixedUpdate()
    {
        StopToWall();
        MoveForward();
        MoveTurn();
        Freeze();
        SpeedUp();
    }
}
