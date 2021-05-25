using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        //move fuel down
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        //check if to low on map
        if (transform.position.y <= -6)
        {
            Destroy(this);
        }
    }
}
