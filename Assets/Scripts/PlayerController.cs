using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;


    private int count;
    private int winCount;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick Up"))
        {
            CheckCount();
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    void CheckCount()
    {
        count++;
        Debug.Log("Pick Up Count: " + count);
        if (count == winCount)
        {
            Debug.Log("You Win!");
        }
    }
}
