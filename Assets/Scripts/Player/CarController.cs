using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    void Update()
    {
        if (RoadMovement.gameover)
            return;

        //right left moving
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * speed * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * -speed * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}