using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panchFoodCheck : MonoBehaviour
{
    public Image nameLabel;//위에피자
    public float waittime;//기다리는 시간
    public float discountpay;//얼마나 깍는지

    public GameObject EatHere;//먹는 입
    public GameObject Panch;//팬치팬치

    public GameObject EatedDish;//먹은 접시

    public Text AddMoneyText;

    private string[] PanchWnatFood = new string[4];
    public bool YesThis = false;
    public int FoodCheckNum = 0;
    public bool Eatting = false;
    public bool EatFinish = false;//다 먹었는지 확인
    public int Pay;//낼 돈
    public int PayForCount = 5000;//진짜 낼 돈


    void Start()
    {
        //손님이 원하는 음식에 대한 정보
        PanchWnatFood[0] = "Cabbage";
        PanchWnatFood[1] = null;
        PanchWnatFood[2] = null;
        PanchWnatFood[3] = null;
        waittime = 60.0f;//손님이 기다리는 최대 시간을 위의 타이머UI에 전송
        nameLabel.SendMessage("SetAliveTime", waittime);

        //시작과 동시에 손님이 기다리는 시간의 초기값 설정
        StartCoroutine(DiscountTime(0.0f));

    }

    //받은 접시 위에 있는 음식이 원하는 음식과 정보가 일치하는지 확인
    //배열을 비교해 원하는 음식의 배열이 있을 때 마다 FoodCheckNum에 1씩 더함
    //FoodCheckNum의 값이 4가 OnTriggerStay함수가 실행되어 식사를 한다.
    void CheckFoodYes(string[] Foods)
    {
        FoodCheckNum = 0;
        int i;
        for (i = 0; i < 4; i++)
        {
            if (PanchWnatFood[i] != Foods[i])
            {
                break;
            }
            else
            {
                FoodCheckNum += 1;
            }
        }
    }

    //받은 음식이 원하는 음식이 맞을 경우 실행되는 함수
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Dish")
        {
            StartCoroutine("LittleDelayEat");//약간의 딜레이가 없으면 너무 어색해서 딜레이를 줌

            //'먹는 중(bool값 Eatting)'상태가 아닐 때만 음식을 받는 다.
            if (FoodCheckNum == 4 && !Eatting)
            {
                //음식을 먹는 중으로 인식하면 남은 대기시간 표시 UI를 지운다.
                nameLabel.SendMessage("ImEatting");

                //받은 금액을 표시하는 UI에 지불할 금액 정보를 전송한다.
                AddMoneyText.SendMessage("AddMoney", Pay);

                /*받은 음식을 지정된 위치로 옮기고 충돌 방지를 위해 식사 시 음식의 Collider값을 false로 한다.*/
                other.transform.SetParent(EatHere.transform);
                other.GetComponent<BoxCollider>().enabled = false;
                other.transform.position = EatHere.transform.position;
                other.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                other.GetComponent<Rigidbody>().isKinematic = true;

                //'먹는 중' 상태에는 다른 접시 안 받도록 설정한다.
                Eatting = true;
                StartCoroutine("DelayEatting");//식사를 실행(밑의 DelayEatting 코루틴 함수 실행)
            }
        }
    }


    //잠깐 딜레이
    IEnumerator LittleDelayEat()
    {
        yield return new WaitForSeconds(0.1f);
    }

    //5초간 식사 후 손님이 사라지는 함수
    IEnumerator DelayEatting()
    {
        yield return new WaitForSeconds(5.0f);
        Instantiate(EatedDish, EatHere.transform.position, EatHere.transform.rotation);
        Panch.SetActive(false);
    }
    //기다린 시간을 측정하는 함수
    IEnumerator DiscountTime(float cool)
    {
        while (cool <= waittime)
        {
            cool += Time.deltaTime;
            discountpay += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    //손님이 음식을 기다린 시간에 반비례하여 지불할 돈을 깎는 함수
    //일정 시간동안 음식을 받지 않으면 손님은 사라지게 한다.
    void CountTime()
    {
        if (!Eatting)
        {
            if ((waittime / 4 * 2) <= (waittime - discountpay))
            {
                Pay = PayForCount * 1;
            }
            else if ((waittime / 4) <= (waittime - discountpay) && (waittime - discountpay) < (waittime / 4 * 2))
            {
                Pay = PayForCount / 4 * 3;
            }
            else if ((waittime * 0) <= (waittime - discountpay) && (waittime - discountpay) < (waittime / 4))
            {
                Pay = PayForCount / 4 * 2;
            }
            else
            {
                Panch.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
    }
}
