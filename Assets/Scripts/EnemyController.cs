using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody enemyRb;

    public float speed;

    public float changeTime = 3.0f; // time before the enemy reverses direction
    private float timer;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        // reference to the enemy rigidbody
        enemyRb = GetComponent<Rigidbody>();

        // initialize timer to time before the enemy reverses direction
        timer = changeTime; 
    }

    // Update is called once per frame
    void Update()
    {
        // start the timer countdown
        timer -= Time.deltaTime;

        if (timer < 0) // if timer runs out
        {
            direction = -direction; // change direction
            timer = changeTime; // reset timer
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = enemyRb.position;
        position.z = position.z + Time.deltaTime * speed * direction;

        // move enemy along z axis
        enemyRb.MovePosition(position);
    }
}
