using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    private RaycastHit hit;

    public Transform HoldSightPoint;
    public GameObject OutLineObject;//빛나는 오브젝트

    void DrawRay() //가시범위 확인을 위해 오브젝트 확인 범위와 동일한 Ray를 그림
    {
        Debug.DrawRay(HoldSightPoint.position, transform.forward * 15, Color.red);
    }

    void FindHold() //잡을 오브젝트의 태그를 확인하여 신호를 보냄
    {
        if (Physics.Raycast(HoldSightPoint.position, transform.forward * 15, out hit))
        {
            if (hit.collider.tag == "Food" && Input.GetKeyDown(KeyCode.Space))
            {
                hit.collider.SendMessage("EnterCheck");
            }
            if (hit.collider.tag == "Dish" && Input.GetKeyDown(KeyCode.Space))
            {
                hit.collider.SendMessage("EnterCheck");
            }
            if (hit.collider.tag == "SummonBox" && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("SummonAndHold");
            }
            if (hit.collider.tag == "Food" && Input.GetKeyDown(KeyCode.LeftShift))
            {
                hit.collider.SendMessage("DomaingChedkOn");
            }
            if (hit.collider.tag == "Food" && Input.GetKeyUp(KeyCode.LeftShift))
            {
                hit.collider.SendMessage("DomaingChedkOff");
            }
        }
    }


    void OutLine()
    {
        if (Physics.Raycast(HoldSightPoint.position, transform.forward * 15, out hit))
        {
            if (hit.collider.tag == "SummonBox")
            {
                hit.collider.SendMessage("LineOnOff");
            }
            if (hit.collider.tag == "Food")
            {
                hit.collider.SendMessage("LineOnOff");
            }
            if (hit.collider.tag == "Dish")
            {
                hit.collider.SendMessage("LineOnOff");
            }
        }
    }

    IEnumerator SummonAndHold() //박스에 식재료를 소환 후 잡을 수 있도록 하는 함수
    {
        hit.collider.SendMessage("Summon");
        yield return new WaitForSeconds(0.001f);
        hit.collider.SendMessage("EnterCheck");
    }

    void FixedUpdate()
    {
        DrawRay();
        FindHold();
        OutLine();
    }
}