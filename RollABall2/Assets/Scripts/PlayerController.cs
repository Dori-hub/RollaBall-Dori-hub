using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private float t;

    public TextMeshProUGUI countText;

    public TextMeshProUGUI timerText;

    public GameObject winTextObject;

    private Rigidbody rb;

    private int count;

    private bool keepTiming;

    private float movementX;

    private float movementY;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        startTimerText();
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Punts: " + count.ToString();

        if (count >= 5)
        {
            winTextObject.SetActive(true);
        }
    }

    void startTimerText()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pick"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    void Update()
    {
        t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(1);
        }
    }
}
