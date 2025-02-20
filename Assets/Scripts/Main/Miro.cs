using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(-58.45f, 7.45f, 0);
        }
    }
}
