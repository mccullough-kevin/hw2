// Author: Kevin McCullough
// Date: 10/5/2024
// Description: Used to control the player - main logic of game including camera orientation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public GameObject cameraObject; // Reference to the camera
    public Text timerText;
    public Text endText;
    public Text bonusText; //special twist!
    public Button restartButton;
    private int pickupCount;
    private float timer = 60.0f;
    private GameObject bonusPickup;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickupCount = 0;
        timerText.text = "Time: " + timer.ToString("F0");
        endText.text = "";
        bonusText.text = "";
        restartButton.gameObject.SetActive(false); // Hide restart button at the start

        int randomIndex = Random.Range(0, 12); // range of pickups to randomize
        bonusPickup = GameObject.Find("PickUp (" + randomIndex + ")"); // finding random pickup
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player forward/backward based on z axis
        float vertical = Input.GetAxis("Vertical");
        rb.AddForce(cameraObject.transform.forward * vertical * speed);

        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontal * Time.deltaTime * speed, Space.World);

        // Update the timer
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Clamp(timer, 0f, 60f).ToString("F0");


        // Check for win or lose
        if (pickupCount >= 12) // if all pickups win condition
        {
            WinGame();
        }
        else if (timer <= 0) // Check if the timer runs out lose
        {
            GameOver();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); // Make pickup disappear
            pickupCount++;
        }

        // Check if pickup is the bonus
        if (other.gameObject == bonusPickup)
        {
            AddBonusTime(); // add 5s bonus time
        }

        // Check if the player has collected all pickups
        if (pickupCount >= 12) // Adjust based on your pickup count
        {
            WinGame();          
        }
    }

    void GameOver()
    {
        endText.text = "You lose!";
        timer = 0;
        restartButton.gameObject.SetActive(true); // Show restart button
    }

    void WinGame()
    {
        endText.text = "You Win!";
        timer = 0;
        restartButton.gameObject.SetActive(true); // Show restart button
    }

    void AddBonusTime()
    {
        timer += 5.0f; // add 5 seconds to  timer
        bonusText.text = "Bonus! Five Seconds Added!";
        StartCoroutine(ClearBonusText()); // clear the text after 5 seconds
    }

    IEnumerator ClearBonusText()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        bonusText.text = ""; // Clear  bonus text
    }




    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Restart the game
    }
}