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

    //썬양배추 지정
    public GameObject Chpoed;

    //Vector3 forceDirection;
    public bool isPlayerEnter;
    public bool isHandOn=false;
    bool isFoodOn;
    bool isFoodOnPoint;
    public bool isDomaOn=false;

    //음식 다진거 확인
    public bool isChoping;
    public int ChopCount=0;
    public int Max = 5;

    public int ShootSpeed = 0;//던지는 속도

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
        if (other.tag == "TrashPoint") //쓰레기통에 닿으면 파괴
        {
            Destroy(gameObject);
        }

        if (isFoodOnIt == false) //오브젝트가 특정 위치에 없을 때만 실행(isFoodOnIt값)
        {
            //도마에 닿을 경우 isDomaOn 값을 true로 변환
            if (other.tag == "DomaPoint")
            {
                isDomaOn = true;
            }

            //도마나 테이블에 닿을 경우 중앙에 고정
            if (other.tag == "FoodOnPoint" || other.tag == "DomaPoint")
            {
                isFoodOnIt = true;
                gameObject.transform.position = other.transform.position;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    //요리사로부터 SendMessage를 통해 명령을 받아 요리사의 손에 위치하도록 하는 함수
    void EnterCheck()
    {
        if (isHandOn == false)
        {
            isPlayerEnter = true;
        }
    }

    //잡히기, 떨어지기 실행
    /*시간차를 주지 않으면 버튼 누르는 시간이 고려되지 않아서 밑에 함수의 두 조건을 동시에 실행된다. 즉 잡자마자 떨어트리는 경우를 방지하기 위하여 약간의 시간차를 둠*/
    void Hand()
    {
        //스페이스 키가 눌리고 요리사의 시야에 있을 경우
        if (Input.GetKey(KeyCode.Space) && isPlayerEnter)
        {
            StartCoroutine("LittleDelayHold");
        }

        //스페이스 키가 눌리고 요리사의 손에 잡힌 상태인 경우
        if (Input.GetKeyDown(KeyCode.Space) && isHandOn)
        {
            StartCoroutine("LittleDelayDrop");
        }
    }

    IEnumerator LittleDelayHold() //살짝 딜레이를 준 후 요리사의 손에 위치하는 함수
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

    IEnumerator LittleDelayDrop()//살짝 딜레이를 준 후 요리사의 손으로부터 떨어지는 함수
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isHandOn = false;
        isPlayerEnter = false;
        playerEquipPoint.transform.DetachChildren();

        //던지기 관련
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

    void Shoot() //던질때 날아가는 속도
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

    //PlaterSight로부터 SendMessage받아 요리 하는 함수
    //도마 위에 있을 경우 SendMessage를 통해 요리 명령을 받으면 요리한다.
    //요리 중일땐 bool값 isChoping을 true로, 아닐땐 false로 한다.
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

    void Choping() //음식이 도마위에 있고 요리사로부터 받은 명령이 실행될 경우 요리 시작
    {
        if (isDomaOn)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isChoping)
            {
                //도마 위에서 요리가 되는 동안 요리사에 있는 bool값 isChoping을 true로 바꿈
                GameObject.Find("wak5").GetComponent<WakMoveIn_2>().isChoping = true;
                InvokeRepeating("Chop", 0.5f, 0.5f);
            }
        }
    }

    //그만 요리하는 함수
    void StopChop()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //요리가 멈추면 요리사에 있는 bool값 isChoping을 false로 바꿈
            GameObject.Find("wak5").GetComponent<WakMoveIn_2>().isChoping = false;
            CancelInvoke("Chop");
        }
    }

    //0.5초 간격으로 Chop()함수를 실행시켜 변수 ChopCount값에 1씩 더함
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

    //ChopCount가 5를 넘으면 요리가 완료 된걸로 판단하여 요리된 음식을 가져오고 재료는 파괴
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
