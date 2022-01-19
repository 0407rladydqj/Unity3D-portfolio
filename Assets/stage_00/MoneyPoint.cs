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

    //받은 금액 정보를 받아서 변수 NowMoney에 더함
    void AddMoney(int PlusMoney)
    {
        NowMoney = NowMoney + PlusMoney;
    }

    //받은 금액을 표시하는 텍스트
    void Update()
    {
        Moneytext.text = "오늘 수입: " + NowMoney + "원";
    }
}
