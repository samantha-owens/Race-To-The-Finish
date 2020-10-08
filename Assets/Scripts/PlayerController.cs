using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody playerRb;

    public ParticleSystem dustParticle;

    [SerializeField] Camera mainCam, hoodCam;
    [SerializeField] KeyCode switchKey;
    [SerializeField] KeyCode boostKey;

    private bool boostUsed = false;

    [SerializeField] float speed = 20.0f;
    [SerializeField] float turnSpeed = 50.0f;
    [SerializeField] float ramForce = 800.0f;
    [SerializeField] float boostForce;

    private float horizontalInput;
    private float forwardInput;

    public string horizontalAxis;
    public string verticalAxis;

    private Vector3 playerStart;

    private void Start()
    {
        // reference to game manager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // reference to player rigidbody
        playerRb = GetComponent<Rigidbody>();

        // position the player at the starting line each game
        playerStart = transform.position;
    }

    void Update()
    {
        ResetPlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
        SpeedBoost();
    }

    // Moves the vehicle based on player input using arrow keys for P1, WASD for P2
    void MovePlayer()
    {
        // only allows the player to move once the countdown is done and game is started
        if (gameManager.gameStart)
        {
            horizontalInput = Input.GetAxis(horizontalAxis);
            forwardInput = Input.GetAxis(verticalAxis);

            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            // changes camera view using right shift for P1, tab for P2
            if (Input.GetKeyDown(switchKey))
            {
                mainCam.enabled = !mainCam.enabled;
                hoodCam.enabled = !hoodCam.enabled;
            }
        }
    }

    // if the boost key is pressed, players will get a brief speed boost
    void SpeedBoost()
    {
        // boost key is Q for P1, spacebar for P2
        if (Input.GetKeyDown(boostKey) && gameManager.gameStart && !boostUsed)
        {
            playerRb.AddRelativeForce(Vector3.forward * boostForce, ForceMode.Impulse);
            dustParticle.gameObject.SetActive(true);

            // starts a 2 second cooldown period before the boost can be used again
            boostUsed = true;
            StartCoroutine("BoostTimer");
        }
    }

    // timer before the boost can be used again
    IEnumerator BoostTimer()
    {
        yield return new WaitForSeconds(2);
        boostUsed = false;
        dustParticle.gameObject.SetActive(false);
    }

    // resets the player at the starting line if the player falls off the road before the finish line
    void ResetPlayer()
    {
        if (transform.position.y < -15 && transform.position.z < 200 && !gameManager.gameOver)
        {
            transform.position = playerStart;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if a player comes into contact with the opponent, player propels the opponent away
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody opponentRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromOpponent = collision.gameObject.transform.position - transform.position;
            opponentRb.AddForce(awayFromOpponent * ramForce, ForceMode.Impulse);
        }

        // if a player comes into contact with an enemy, player is propelled away from the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromEnemy = (transform.position - collision.gameObject.transform.position);
            playerRb.AddForce(awayFromEnemy * ramForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if a player crosses the finish line, they 'win', get a point, and the game can be restarted
        if (other.gameObject.CompareTag("Finish") && !gameManager.gameOver)
        {
            if (gameObject.name == "Player 1")
            {
                gameManager.GameOverP1();
                gameManager.winText.text = name + " WINS!";
            }
            else if (gameObject.name == "Player 2")
            {
                gameManager.GameOverP2();
                gameManager.winText.text = name + " WINS!";
            }
        }
    }
}
