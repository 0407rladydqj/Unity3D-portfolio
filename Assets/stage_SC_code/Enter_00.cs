using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_00 : MonoBehaviour
{
    public Sprite CurrentSprite;
    public Sprite NextSprite;
    private SpriteRenderer spriteRenderer;

    bool isBorder;
    bool isBorder2;

    public GameObject Target;
    private float Dist;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CurrentSprite;
    }

    void isCheck()
    {
        Dist = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        /*
        Debug.DrawRay(transform.position, transform.right * 2, Color.red);
        isBorder = Physics.Raycast(transform.position, transform.right * 2, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, transform.right * -2, Color.green);
        isBorder2 = Physics.Raycast(transform.position, transform.right * -2, LayerMask.GetMask("Player"));
        */
    }

    void isTeleport()
    {
        if (Dist < 1.7)
        {
            spriteRenderer.sprite = NextSprite;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("stage_00");
            }
        }
        else
        {
            spriteRenderer.sprite = CurrentSprite;
        }
        /*
        if (isBorder && isBorder2)
        {
            spriteRenderer.sprite = NextSprite;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("stage_00");
            }
        }
        else
        {
            Debug.Log("asdf");
            spriteRenderer.sprite = CurrentSprite;
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isCheck();
        isTeleport();
        /*
        float DE = Vector2.Distance(gameObject.transform.position, target.transform.position);
        if (DE <= 1)
        {
            Debug.Log(DE);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("stage_00");
            }
        }
        //Debug.Log(Vector2.Distance(gameObject.transform.position, target.transform.position));
        */
    }
}
