using UnityEngine;

public class LockY : MonoBehaviour
{
public Transform player;
public float fixedY = 5f; // desired Y position

void LateUpdate() {
    transform.position = new Vector3(player.position.x, fixedY, transform.position.z);
}
}
