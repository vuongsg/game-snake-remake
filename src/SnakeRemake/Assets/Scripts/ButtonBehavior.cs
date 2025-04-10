using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public Texture2D hoverTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void MouseEnter()
	{
		Cursor.SetCursor(hoverTexture, Vector2.zero, CursorMode.Auto);
	}

	public void MouseExit()
	{
		Cursor.SetCursor(null , Vector2.zero, CursorMode.Auto);
	}
}
