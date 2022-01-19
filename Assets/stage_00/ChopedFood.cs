using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopedFood : MonoBehaviour
{
    GameObject player;
    GameObject playerEquipPoint;
    GameObject FoodOnPoint;
    GameObject foodPoint;
    GameObject TableFoodOnPoint;
    GameObject DomaOnPoint;

    //�� ���� �̸�
    string FoodName = "Cabbage";

    //Vector3 forceDirection;
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
        if (other.tag == "TrashPoint") //�������뿡 ������ �ı�
        {
            Destroy(gameObject);
        }

        if (isFoodOnIt == false)
        {
            //������ ���̺� ���� ��� �߾ӿ� ����
            if (other.tag == "FoodOnPoint" || other.tag == "DomaPoint")
            {
                isFoodOnIt = true;
                gameObject.transform.position = other.transform.position;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        //���ÿ� ������ ���� �߾ӿ� ��ġ�ϰ� �ݶ��̴� ���� false�� �Ѵ�
        if (other.tag == "DishPoint")
        {
            isFoodOnIt = false;
            transform.SetParent(other.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }

        //���ÿ� ���� ��� ���ÿ� ������ ����(FoodName)�� ����
        if (other.tag == "DishPoint")
        {
            other.SendMessage("AddFood", FoodName);
        }

    }

    void OnTriggerStay(Collider other)
    {
        
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



    IEnumerator LittleHoldDelay()
    {
        yield return new WaitForSeconds(0.1f);
        isPlayerEnter = false;
        isHandOn = true;
    }
    IEnumerator LittleDropDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isHandOn = false;
        playerEquipPoint.transform.DetachChildren();

        GetComponent<Rigidbody>().AddForce(transform.forward * ShootSpeed, ForceMode.Impulse);//������
    }

    void Hand()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerEnter)
        {
            isFoodOnIt = false;
            transform.SetParent(playerEquipPoint.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine("LittleHoldDelay");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isHandOn)
        {
            StartCoroutine("LittleDropDelay");
        }
    }

    void Shoot()
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



    void EnterCheck()
    {
        if (isHandOn == false)
        {
            isPlayerEnter = true;
        }
    }
    void DomaingChedkOn()
    {
        //isChoping = true;
    }
    void DomaingChedkOff()
    {
        //isChoping = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        Hand();
        Shoot();
    }
}

