using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    // Writing this under the assumption that it is in the Player's object


    private Animator anim;
    private Rigidbody2D player_2d;
    bool fall = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player_2d = GetComponent<Rigidbody2D>();
        // player_grav = GetComponent<G>
    }

    private void Update()
    {
        if (fall)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Saw"))
        {
            Death();
        }
    }
    private void Death()
    {
        anim.SetTrigger("death");
        player_2d.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        Debug.Log("asdasd");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FallOff()
    {
        Debug.Log("chec");
        fall = true;
    }
    // in the animation event, that is were the restartlevel function is used.
}
