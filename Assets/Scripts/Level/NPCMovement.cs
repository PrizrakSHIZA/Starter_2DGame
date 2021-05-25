using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    Transform trans;
    [SerializeField]
    float speed;
    bool crashed = false;

    private void Start()
    {
        trans = gameObject.transform;
        speed = Random.Range(1f, RoadMovement.Speed - 1f);
    }

    void Update()
    {
        if (RoadMovement.gameover)
        {
            if (!crashed)
                speed = Mathf.Lerp(speed, -RoadMovement.lastSpeed, Time.deltaTime);
            else
                speed = 0;
        }

        trans.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if (gameObject.transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            speed = Mathf.Lerp(speed, collision.GetComponent<NPCMovement>().speed, Mathf.Abs(speed - RoadMovement.Speed) * Time.deltaTime);
        }
    }

    public void Crashed()
    {
        crashed = true;
    }
}
