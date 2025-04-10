using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    public Collider2D foodBorder;
    public GameObject snake;

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    private SnakeBehavior snakeBehavior;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minX = foodBorder.bounds.min.x;
        minY = foodBorder.bounds.min.y;
        maxX = foodBorder.bounds.max.x;
        maxY = foodBorder.bounds.max.y;
        snakeBehavior = snake.GetComponent<SnakeBehavior>();

        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        RandomizePosition();
	}

    public void RandomizePosition()
    {
		int x = Mathf.RoundToInt(Random.Range(minX, maxX));
		int y = Mathf.RoundToInt(Random.Range(minY, maxY));

		while (IsOccupiedBySnake(x, y))
		{
			x = Mathf.RoundToInt(Random.Range(minX, maxX));
			y = Mathf.RoundToInt(Random.Range(minY, maxY));
		}

		transform.position = new Vector3(x, y, 0);
	}

    private bool IsOccupiedBySnake(int x, int y)
    {
        foreach (var part in snakeBehavior.snakeSegments)
        {
			if (IsCollidedPosition(part, x, y))
				return true;
		}

        return false;
    }

    private bool IsCollidedPosition(GameObject obj, int x, int y)
    {
        return obj.transform.position.x == x && obj.transform.position.y == y;
    }
}
