using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleCycleMovement : MonoBehaviour
{
    public float speed = 2f;
    public float endpos = -10f;
    public Transform[] parts;


    private void Start()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = gameObject.transform.GetChild(i);
        }
    }

    void Update()
    {
        parts = parts.OrderByDescending(x => x.position.y).ToArray();
        foreach (Transform part in parts)
        {
            part.position -= new Vector3(0, speed * Time.deltaTime, 0);
            if (part.position.y <= endpos)
            {
                part.position = new Vector3(0, parts[0].position.y + parts[0].gameObject.GetComponent<RectTransform>().rect.height, 0);
            }
        }
    }
}
