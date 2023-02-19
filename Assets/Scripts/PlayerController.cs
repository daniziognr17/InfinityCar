using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 100f;
    public float speedIncreaseAmmount = 1f;
    public float speedIncreaseTime = 0.5f;
    private float horizontalInput;
    public LayerMask roadLayer;

    public GameObject lastRoad;
    public GameObject currentRoad;

    public AudioClip crashSound;
    private AudioSource audioSource;

    public float playerScore = 1f;

    public TextMeshProUGUI scoreText;
    public GameObject scorePanel;
    public GameObject gameOverPanel;

    private void Start()
    {
        DetectCurrentRoad();
        lastRoad = currentRoad;
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1;
        scorePanel.SetActive(true);
    }

    private void Update()
    {
        playerScore += Time.deltaTime;
        scoreText.SetText("Score: " + Mathf.RoundToInt(playerScore).ToString());

        if (speedIncreaseAmmount <= 5)
        {
            speedIncreaseAmmount += speedIncreaseTime * Time.deltaTime;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * speedIncreaseAmmount);
        transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);

        DetectCurrentRoad();

        if (lastRoad != currentRoad)
        {
            Destroy(lastRoad, 1f);
            lastRoad = currentRoad;
        }
    }

    void DetectCurrentRoad()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, roadLayer);
        foreach (var hitCollider in hitColliders)
        {
            currentRoad = hitCollider.transform.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            audioSource.clip = crashSound;
            audioSource.Play();
            audioSource.loop = false;
            Time.timeScale = 0f;
            scorePanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
