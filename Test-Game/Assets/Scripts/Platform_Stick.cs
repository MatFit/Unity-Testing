using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Stick : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asd");
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


    // ok so the only confusing part here is how the set parent doesn't interfere with the player movement
    // I know the setparent function sets the player transform as the child of the platform transform, but I am struggling to understand
    // how the player movement isn't affected by this
}
