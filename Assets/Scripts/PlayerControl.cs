using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColor;

    private int count;
    private int winCount;
    public bool won;

    GameController gameController;
    Timer timer;

    [Header("Ui Stuff")]
    public GameObject gameOverScreen;
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timerText;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        timer = FindObjectOfType<Timer>();
    }

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        count = 0;
        gameOverScreen.SetActive(false);
        winCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;

        countText.wordSpacing = 100;
        countText.fontSize = 50;
        countText.color = Color.green;
        countText.fontStyle = FontStyles.SmallCaps;
        countText.text = "Count: " + count + " / " + winCount;

        winText.text = "";

        //timer = 0;
        won = false;
        resetPoint = GameObject.Find("Reset Point");
        originalColor = GetComponent<Renderer>().material.color;
        gameController = FindObjectOfType<GameController>();

        
        if (gameController.gameType == GameType.SpeedRun)
            StartCoroutine(timer.StartCountdown());
    }

    
    void Update()
    {
        //if (won == false)
        //{
        //    float += Time.deltaTime;
        //    timerText.text = "Time: " + timer.ToString();
        //}
    }
    void FixedUpdate()
    {
        if (resetting)
            return;

        if (gameController.controlType == ControlType.WorldTilt)
            return;
        
        if (gameController.gameType == GameType.SpeedRun && !timer.IsTiming())
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick Up"))
        {
            if (resetting)
                return;

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
            WinGame();
            won = true;
            //winText.text = "You Win!\n" + "<color=red><size=50>" + "Your Time:" + timer.timerText.text;
        }
    }

    void WinGame()
    {
        gameOverScreen.SetActive(true);
        winText.text = "You Win";

        if (gameController.gameType == GameType.SpeedRun)
            timer.StopTimer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            if (gameController.wallType == WallType.Punishing)
                StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColor;
        resetting = false;
    }
}
