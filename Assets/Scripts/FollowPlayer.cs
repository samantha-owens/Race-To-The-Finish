using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 offset = new Vector3(0, 5, -9);

    void LateUpdate()
    {
        // camera position follows player position, offset the camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
    }
}
