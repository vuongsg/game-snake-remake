using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeBehavior : MonoBehaviour
{
    public GameObject gameControllerObject;
    public GameObject snakeSegment;
	public List<GameObject> snakeSegments;
	public AudioClip creepAudioClip;
	public AudioClip pointAudioClip;
	public AudioClip gameOverAudioClip;

    [SerializeField]
	private int originalLength = 4;

	private Vector3 direction;
    private GameController gameController;
    private Vector3 originalPosition;
	private AudioSource audioSource;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        //set fixedTime in Project Settings to be 0.06f
        snakeSegments = new List<GameObject>();
        gameController = gameControllerObject.GetComponent<GameController>();
        originalPosition = transform.position;
        snakeSegments.Add(gameObject);
		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = creepAudioClip;
        audioSource.playOnAwake = false;    //important
		Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector3.left;
        else if (Input .GetKeyDown(KeyCode.RightArrow))
            direction = Vector3.right;

        if (direction != Vector3.zero)
            gameController.SetIsPlaying(true);
    }

	private void FixedUpdate()
	{
        if (!gameController.IsPlaying())
            return;

        audioSource.Play();
        for (int i = snakeSegments.Count - 1; i > 0; i--)
            snakeSegments[i].transform.position = snakeSegments[i - 1].transform.position;

		transform.Translate(direction);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Food"))
		{
			audioSource.PlayOneShot(pointAudioClip);
			gameController.IncreaseScore();
			var newSegment = Instantiate(snakeSegment, snakeSegments.Last().transform.position, Quaternion.identity);
			snakeSegments.Add(newSegment);
		}
        else if (collision.transform.CompareTag("Wall"))
        {
			audioSource.PlayOneShot(gameOverAudioClip);
			gameController.EndGame();
        }
	}

	public void Reset()
	{
        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            Destroy(snakeSegments[i]);
            snakeSegments.RemoveAt(i);
        }

        transform.position = originalPosition;
        int dir = Random.Range(0, 3);
        for (int i = 0; i < originalLength; i++)
        {
            GameObject newSegment = null;

            switch (dir)
            {
                case 0:
					newSegment = Instantiate(snakeSegment, snakeSegments.Last().transform.position + Vector3.up, Quaternion.identity);
					break;
                case 1:
					newSegment = Instantiate(snakeSegment, snakeSegments.Last().transform.position + Vector3.down, Quaternion.identity);
					break;
                case 2:
					newSegment = Instantiate(snakeSegment, snakeSegments.Last().transform.position + Vector3.left, Quaternion.identity);
					break;
                case 3:
					newSegment = Instantiate(snakeSegment, snakeSegments.Last().transform.position + Vector3.right, Quaternion.identity);
					break;
            }

            snakeSegments.Add(newSegment);
        }

        direction = Vector3.zero;
	}

    public void HiddenSnake()
    {
        foreach (var part in snakeSegments)
            part.SetActive(false);
    }

    public void ShowSnake()
    {
        foreach (var part in snakeSegments)
            part.SetActive(true);
    }
}
