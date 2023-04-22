using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private Transform trans;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 8f;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // fixes the dude from rotating like a buffoon
        if (trans.rotation.z != 0)
        {
            trans.rotation = Quaternion.Euler(trans.rotation.x, trans.rotation.y, 0);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}



    // collision checker bound center and size, where center makes a boxcast around the player and the size of
    // box cast is same as the size of the players collision box
    // rotation is 0 since we dont want to rotate anything, and vector2 down is just a slight offset of the boxcast
    // (a little down)
    // why this works is first I'll explain why just looking for collision between the player and terrian is a mid idea.
    // because the player can exploit the game by going on the sides of the terrian and jump off of it because it's considered grounded
    // That doesn't make sense
    // But if we have a boxcast where it's displaced just a little below the player collison box, we can check that instead if there is
    // an overlap between the boxcast and terrian, the player is grounded logically as it is only located in the -y direction by just a little
    // and so it wouldn't be possible for the player to be considered grounded on the side of the map because there is no boxcast there.
    // Boxcast is like a JoJo stand to the player collision

    // And so the boxcast itself will detect overlap with the layer, the first 6 para was the positioning and size of boxcast, the last one
    // being the layer of whatever, it will detect an overlap with said layer and return true or false
