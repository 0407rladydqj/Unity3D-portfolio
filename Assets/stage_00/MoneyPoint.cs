using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPoint : MonoBehaviour
{
    Text Moneytext;
    int NowMoney = 0;
    void Start()
    {
        Moneytext = GetComponent<Text>();
    }

    //���� �ݾ� ������ �޾Ƽ� ���� NowMoney�� ����
    void AddMoney(int PlusMoney)
    {
        NowMoney = NowMoney + PlusMoney;
    }

    //���� �ݾ��� ǥ���ϴ� �ؽ�Ʈ
    void Update()
    {
        Moneytext.text = "���� ����: " + NowMoney + "��";
    }
}
