using UnityEngine;

public class PlayerCollision_VNLong : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Player Died");
        Destroy(gameObject); // Or trigger game over screen
    }
}
