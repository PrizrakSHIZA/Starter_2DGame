using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public AudioSource CrushSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Game Over (crash)
        if (collision.collider.tag == "Enemy" && !RoadMovement.gameover)
        {
            collision.gameObject.GetComponent<NPCMovement>().Crashed();
            CrushSound.Play();
            RoadMovement.GameOver(GameOverFlag.crashed);
        }

        //Pick up fuel
        if (collision.collider.tag == "Fuel")
        {
            Destroy(collision.gameObject);
            transform.parent.GetComponent<FuelSystem>().AddFuel(Random.Range(25, 50));
        }
    }
}
