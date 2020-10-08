using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        RotateIcons();
    }

    // rotates player icons
    void RotateIcons()
    {
        // rotates player icons in opposite directions
        if (gameObject.name == "Player 1 Icon")
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else if (gameObject.name == "Player 2 Icon")
        {
            transform.Rotate(Vector3.down * speed * Time.deltaTime);
        }
    }

    // loads the next scene and begins the game (attached to START button in UI)
    public void LoadGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Prototype 1");
    }
}
