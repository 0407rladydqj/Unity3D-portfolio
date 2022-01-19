using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panchFoodCheck : MonoBehaviour
{
    public Image nameLabel;//��������
    public float waittime;//��ٸ��� �ð�
    public float discountpay;//�󸶳� �����

    public GameObject EatHere;//�Դ� ��
    public GameObject Panch;//��ġ��ġ

    public GameObject EatedDish;//���� ����

    public Text AddMoneyText;

    private string[] PanchWnatFood = new string[4];
    public bool YesThis = false;
    public int FoodCheckNum = 0;
    public bool Eatting = false;
    public bool EatFinish = false;//�� �Ծ����� Ȯ��
    public int Pay;//�� ��
    public int PayForCount = 5000;//��¥ �� ��


    void Start()
    {
        //�մ��� ���ϴ� ���Ŀ� ���� ����
        PanchWnatFood[0] = "Cabbage";
        PanchWnatFood[1] = null;
        PanchWnatFood[2] = null;
        PanchWnatFood[3] = null;
        waittime = 60.0f;//�մ��� ��ٸ��� �ִ� �ð��� ���� Ÿ�̸�UI�� ����
        nameLabel.SendMessage("SetAliveTime", waittime);

        //���۰� ���ÿ� �մ��� ��ٸ��� �ð��� �ʱⰪ ����
        StartCoroutine(DiscountTime(0.0f));

    }

    //���� ���� ���� �ִ� ������ ���ϴ� ���İ� ������ ��ġ�ϴ��� Ȯ��
    //�迭�� ���� ���ϴ� ������ �迭�� ���� �� ���� FoodCheckNum�� 1�� ����
    //FoodCheckNum�� ���� 4�� OnTriggerStay�Լ��� ����Ǿ� �Ļ縦 �Ѵ�.
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

    //���� ������ ���ϴ� ������ ���� ��� ����Ǵ� �Լ�
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Dish")
        {
            StartCoroutine("LittleDelayEat");//�ణ�� �����̰� ������ �ʹ� ����ؼ� �����̸� ��

            //'�Դ� ��(bool�� Eatting)'���°� �ƴ� ���� ������ �޴� ��.
            if (FoodCheckNum == 4 && !Eatting)
            {
                //������ �Դ� ������ �ν��ϸ� ���� ���ð� ǥ�� UI�� �����.
                nameLabel.SendMessage("ImEatting");

                //���� �ݾ��� ǥ���ϴ� UI�� ������ �ݾ� ������ �����Ѵ�.
                AddMoneyText.SendMessage("AddMoney", Pay);

                /*���� ������ ������ ��ġ�� �ű�� �浹 ������ ���� �Ļ� �� ������ Collider���� false�� �Ѵ�.*/
                other.transform.SetParent(EatHere.transform);
                other.GetComponent<BoxCollider>().enabled = false;
                other.transform.position = EatHere.transform.position;
                other.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                other.GetComponent<Rigidbody>().isKinematic = true;

                //'�Դ� ��' ���¿��� �ٸ� ���� �� �޵��� �����Ѵ�.
                Eatting = true;
                StartCoroutine("DelayEatting");//�Ļ縦 ����(���� DelayEatting �ڷ�ƾ �Լ� ����)
            }
        }
    }


    //��� ������
    IEnumerator LittleDelayEat()
    {
        yield return new WaitForSeconds(0.1f);
    }

    //5�ʰ� �Ļ� �� �մ��� ������� �Լ�
    IEnumerator DelayEatting()
    {
        yield return new WaitForSeconds(5.0f);
        Instantiate(EatedDish, EatHere.transform.position, EatHere.transform.rotation);
        Panch.SetActive(false);
    }
    //��ٸ� �ð��� �����ϴ� �Լ�
    IEnumerator DiscountTime(float cool)
    {
        while (cool <= waittime)
        {
            cool += Time.deltaTime;
            discountpay += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    //�մ��� ������ ��ٸ� �ð��� �ݺ���Ͽ� ������ ���� ��� �Լ�
    //���� �ð����� ������ ���� ������ �մ��� ������� �Ѵ�.
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
