using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanchWait : MonoBehaviour
{

    public Image img_Skill;
    private float time;
    public float discountpay;//�󸶳� ����� �˾Ƽ� �� ��ȭ
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

    //���� �ð��� ���� Ÿ�̸� UI�� ���� ��ȭ
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

    //���� �ð��� ����Ͽ� Ÿ�̸��� ����� ��ȭ
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

    //�մ��� �Ļ縦 �����ߴٴ� ��ȣ�� ������ UI�� ������� �ϴ� �Լ�
    void ImEatting()
    {
        gameObject.SetActive(false);
    }
}