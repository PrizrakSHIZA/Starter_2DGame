using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    Vector2 pos;
    
    void Update()
    {
        if (RoadMovement.gameover)
            return;

#if UNITY_ANDROID
        //left right moving for phone
        if (Settings.SideControl)
        {
            SideControlHandler();
        }
        else
        {
            BasicControlHandler();
        }
#endif

#if UNITY_STANDALONE_WIN
        //right left moving for PC
        if (Input.GetAxis("Horizontal") > 0)
        {
            MoveRight();
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            MoveLeft();
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            MakeStable();
        }
#endif
    }

    //Control handlers
    public void BasicControlHandler()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                pos = touch.position;
            }
            else
            {
                float x = pos.x - touch.position.x;
                if (x < 0)
                {
                    MoveRight();
                }
                if (x > 0)
                {
                    MoveLeft();
                }
            }
        }
        else
        {
            MakeStable();
        }
    }

    public void SideControlHandler()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                MoveLeft();
            }
            else if (touch.position.x > Screen.width / 2)
            {
                MoveRight();
            }
        }
        else
        {
            MakeStable();
        }
    }

    //movement
    public void MoveRight()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * speed * Time.deltaTime);
    }
    public void MoveLeft()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * -speed * Time.deltaTime);
    }
    public void MakeStable()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}