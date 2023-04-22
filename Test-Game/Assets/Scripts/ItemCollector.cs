using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    // Start is called before the first frame update
    public int cherries = 0;
    [SerializeField] private Text cherries_collect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            cherries++;
            Destroy(collision.gameObject);
            cherries_collect.text = "Cherry Collected: " + cherries;
        }

        // looks a little wonky but if I translate this into words
        // its if the object, as the player, we are colliding with is
        // the Cherry, then destory that gameObject.
    }
}
