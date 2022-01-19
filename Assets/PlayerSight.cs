using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    private RaycastHit hit;

    public Transform HoldSightPoint;
    public GameObject OutLineObject;//������ ������Ʈ

    void DrawRay() //���ù��� Ȯ���� ���� ������Ʈ Ȯ�� ������ ������ Ray�� �׸�
    {
        Debug.DrawRay(HoldSightPoint.position, transform.forward * 15, Color.red);
    }

    void FindHold() //���� ������Ʈ�� �±׸� Ȯ���Ͽ� ��ȣ�� ����
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

    IEnumerator SummonAndHold() //�ڽ��� ����Ḧ ��ȯ �� ���� �� �ֵ��� �ϴ� �Լ�
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