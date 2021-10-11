using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;


    private int count;
    private int winCount;
    float timer;
    public bool won;

    [Header("Ui Stuff")]
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timerText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;

        countText.wordSpacing = 100;
        countText.fontSize = 50;
        countText.color = Color.green;
        countText.fontStyle = FontStyles.SmallCaps;
        countText.text = "Count: " + count + " / " + winCount;

        winText.text = "";

        timer = 0;
        won = false;
    }

    void Update()
    {
        if(won == false)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
        }
        
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
        countText.text = "Count: " + count + " / " + winCount;
        // Debug.Log("Pick Up Count: " + count);
        if (count == winCount)
        {
            won = true;
            winText.text = "You Win!\n" + "<color=red><size=50>" + "Your Time:" + timer.ToString("F3");
        }
    }
}
