using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanchWait : MonoBehaviour
{

    public Image img_Skill;
    private float time;
    public float discountpay;//얼마나 깍는지 알아서 색 변화
    void Start()
    {

    }

    void SetAliveTime(float ttime)
    {
        time = ttime;
        StartCoroutine(CoolTime(time));
    }

    void Update()
    {
        ColorChange();
    }

    //남은 시간에 따라 타이머 UI의 색이 변화
    void ColorChange()
    {
        if ((time / 4) <= (time - discountpay) && (time - discountpay) < (time / 4 * 2))
        {
            img_Skill.GetComponent<Image>().color = new Color(250 / 255f, 120 / 255f, 0 / 255f, 120 / 255f);
        }
        else if ((time * 0) <= (time - discountpay) && (time - discountpay) < (time / 4))
        {
            img_Skill.GetComponent<Image>().color = new Color(250 / 255f, 0 / 255f, 32 / 255f, 150 / 255f);
        }
    }

    //남은 시간에 비례하여 타이머의 모양이 변화
    IEnumerator CoolTime(float cool)
    {
        while (cool <= time)
        {
            cool -= Time.deltaTime;
            discountpay += Time.deltaTime;
            img_Skill.fillAmount = (cool / time);
            yield return new WaitForFixedUpdate();
        }
    }

    //손님이 식사를 시작했다는 신호를 받으면 UI를 사라지게 하는 함수
    void ImEatting()
    {
        gameObject.SetActive(false);
    }
}