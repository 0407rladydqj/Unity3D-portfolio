using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    GameObject player;
    GameObject playerEquipPoint;
    GameObject FoodOnPoint;
    GameObject foodPoint;
    GameObject TableFoodOnPoint;
    GameObject DomaOnPoint;

    //������ ����
    public GameObject Chpoed;

    //Vector3 forceDirection;
    public bool isPlayerEnter;
    public bool isHandOn=false;
    bool isFoodOn;
    bool isFoodOnPoint;
    public bool isDomaOn=false;

    //���� ������ Ȯ��
    public bool isChoping;
    public int ChopCount=0;
    public int Max = 5;

    public int ShootSpeed = 0;//������ �ӵ�

    bool isFoodOnIt = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
        foodPoint = GameObject.FindGameObjectWithTag("ObjectFind");
        TableFoodOnPoint = GameObject.FindGameObjectWithTag("FoodOnPoint");
        DomaOnPoint = GameObject.FindGameObjectWithTag("DomaPoint");
    }

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TrashPoint") //�������뿡 ������ �ı�
        {
            Destroy(gameObject);
        }

        if (isFoodOnIt == false) //������Ʈ�� Ư�� ��ġ�� ���� ���� ����(isFoodOnIt��)
        {
            //������ ���� ��� isDomaOn ���� true�� ��ȯ
            if (other.tag == "DomaPoint")
            {
                isDomaOn = true;
            }

            //������ ���̺� ���� ��� �߾ӿ� ����
            if (other.tag == "FoodOnPoint" || other.tag == "DomaPoint")
            {
                isFoodOnIt = true;
                gameObject.transform.position = other.transform.position;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    //�丮��κ��� SendMessage�� ���� ����� �޾� �丮���� �տ� ��ġ�ϵ��� �ϴ� �Լ�
    void EnterCheck()
    {
        if (isHandOn == false)
        {
            isPlayerEnter = true;
        }
    }

    //������, �������� ����
    /*�ð����� ���� ������ ��ư ������ �ð��� ������� �ʾƼ� �ؿ� �Լ��� �� ������ ���ÿ� ����ȴ�. �� ���ڸ��� ����Ʈ���� ��츦 �����ϱ� ���Ͽ� �ణ�� �ð����� ��*/
    void Hand()
    {
        //�����̽� Ű�� ������ �丮���� �þ߿� ���� ���
        if (Input.GetKey(KeyCode.Space) && isPlayerEnter)
        {
            StartCoroutine("LittleDelayHold");
        }

        //�����̽� Ű�� ������ �丮���� �տ� ���� ������ ���
        if (Input.GetKeyDown(KeyCode.Space) && isHandOn)
        {
            StartCoroutine("LittleDelayDrop");
        }
    }

    IEnumerator LittleDelayHold() //��¦ �����̸� �� �� �丮���� �տ� ��ġ�ϴ� �Լ�
    {
        yield return new WaitForSeconds(0.1f);
        isFoodOnIt = false;
        transform.SetParent(playerEquipPoint.transform);
        transform.localPosition = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isPlayerEnter = false;
        isHandOn = true;
        isDomaOn = false;
    }

    IEnumerator LittleDelayDrop()//��¦ �����̸� �� �� �丮���� �����κ��� �������� �Լ�
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isHandOn = false;
        isPlayerEnter = false;
        playerEquipPoint.transform.DetachChildren();

        //������ ����
        GetComponent<Rigidbody>().AddForce(transform.forward * ShootSpeed, ForceMode.Impulse);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = false;
        }

        if (other.gameObject == foodPoint)
        {
            isFoodOn = false;
        }

        if (other.gameObject == DomaOnPoint)
        {
            isFoodOn = false;
        }
    }

    void Shoot() //������ ���ư��� �ӵ�
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ShootSpeed = -70;
        }
        else
        {
            ShootSpeed = 0;
        }
    }

    //PlaterSight�κ��� SendMessage�޾� �丮 �ϴ� �Լ�
    //���� ���� ���� ��� SendMessage�� ���� �丮 ����� ������ �丮�Ѵ�.
    //�丮 ���϶� bool�� isChoping�� true��, �ƴҶ� false�� �Ѵ�.
    void DomaingChedkOn()
    {
        if (isDomaOn)
        {
            isChoping = true;
        }
    }
    void DomaingChedkOff()
    {
        isChoping = false;
    }

    void Choping() //������ �������� �ְ� �丮��κ��� ���� ����� ����� ��� �丮 ����
    {
        if (isDomaOn)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isChoping)
            {
                //���� ������ �丮�� �Ǵ� ���� �丮�翡 �ִ� bool�� isChoping�� true�� �ٲ�
                GameObject.Find("wak5").GetComponent<WakMoveIn_2>().isChoping = true;
                InvokeRepeating("Chop", 0.5f, 0.5f);
            }
        }
    }

    //�׸� �丮�ϴ� �Լ�
    void StopChop()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //�丮�� ���߸� �丮�翡 �ִ� bool�� isChoping�� false�� �ٲ�
            GameObject.Find("wak5").GetComponent<WakMoveIn_2>().isChoping = false;
            CancelInvoke("Chop");
        }
    }

    //0.5�� �������� Chop()�Լ��� ������� ���� ChopCount���� 1�� ����
    void Choped()
    {
        if (isChoping)
        {
            Invoke("Chop", 0.5f);
        }
    }
    void Chop()
    {
        ChopCount += 1;
    }

    //ChopCount�� 5�� ������ �丮�� �Ϸ� �Ȱɷ� �Ǵ��Ͽ� �丮�� ������ �������� ���� �ı�
    void ChopCheck()
    {
        if (ChopCount > Max)
        {
            Instantiate(Chpoed, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Hand();
        ChopCheck();
        StopChop();
        Choping();
        Shoot();
    }
}
