using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    GameObject player;
    GameObject playerEquipPoint;
    GameObject FoodOnPoint;
    GameObject foodPoint;
    GameObject TableFoodOnPoint;
    GameObject DomaOnPoint;

    //���� �ִ� ���Ŀ� ���� ������ �����ϴ� �迭
    private string[] Foods = new string[4];
    int FoodCounter = 0;

    bool isPlayerEnter;
    bool isHandOn;
    bool isFoodOn;
    bool isFoodOnPoint;

    //���� ������ Ȯ��
    bool isChop;
    public int ChopCount = 0;
    bool isFoodOnIt = false;

    public int ShootSpeed = 0;//������ �ӵ�



    // Start is called before the first frame update
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
        GameObject.Find("wak5").GetComponent<WakMoveIn_2>().isChoping = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TrashPoint")
        {
            gameObject.SetActive(false);
        }

        //�մԿ��� ���� ��� �մԿ��� ���� ���� ������ ����
        if (other.tag == "PanchFoodOn")
        {
            other.SendMessage("CheckFoodYes", Foods);
        }

        if (isFoodOnIt == false)
        {
            if (other.tag == "FoodOnPoint" || other.tag == "DomaPoint")
            {
                isFoodOnIt = true;
                gameObject.transform.position = other.transform.position;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
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
    }



    IEnumerator LittleDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isPlayerEnter = false;
        isHandOn = true;
    }

    void Hand()
    {
        if (Input.GetKey(KeyCode.Space) && isPlayerEnter)
        {
            StartCoroutine("LittleDelayHold");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isHandOn)
        {
            StartCoroutine("LittleDelayDrop");
        }
    }

    IEnumerator LittleDelayHold()
    {
        yield return new WaitForSeconds(0.1f);
        isFoodOnIt = false;
        transform.SetParent(playerEquipPoint.transform);
        transform.localPosition = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        isPlayerEnter = false;
        isHandOn = true;
    }

    IEnumerator LittleDelayDrop()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isHandOn = false;
        isPlayerEnter = false;
        playerEquipPoint.transform.DetachChildren();

        GetComponent<Rigidbody>().AddForce(transform.forward * ShootSpeed, ForceMode.Impulse);//������
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ShootSpeed = -15;
        }
        else
        {
            ShootSpeed = 0;
        }
    }


    void EnterCheck()
    {
        isPlayerEnter = true;
    }
    void DomaingChedkOn()
    {
        //isChoping = true;
    }
    void DomaingChedkOff()
    {
        //isChoping = false;
    }

    //�丮�� �������κ��� ������ ������ �޾� Foods�迭�� ����
    //�ִ� 4������ ������ ������ �� ����
    void AddFood(string addfood)
    {
        if (FoodCounter < 4)
        {
            Foods[FoodCounter] = addfood;
            Debug.Log(addfood);
            FoodCounter += 1;
        }
    }


    void ChangeTag()
    {
        gameObject.tag = "FoodOnDish";
    }
    // Update is called once per frame
    void Update()
    {
        Hand();
        //Shoot();  //�ϴ� ���� ��������

        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log(Foods.Length);
            Debug.Log(Foods);
            Debug.Log(FoodCounter);
        }
    }
}

