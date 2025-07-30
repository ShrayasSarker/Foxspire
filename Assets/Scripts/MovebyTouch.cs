using UnityEngine;

public class MovebyTouch : MonoBehaviour
{
    void Update()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(transform.position, touchPosition, Color.red); // Draw a line for debugging
        }
        
    }
}
