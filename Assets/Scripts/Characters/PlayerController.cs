using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    private bool isGrounded;
    private bool isSilding = false;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float maxSpeed = 20f;
    private int currentPath = 1; // 0: Left, 1: Middle, 2: Right
    [SerializeField] private int pathDistance = 4; // The distance between paths
    [SerializeField] private float jumpHeight = 2f; // The jump height
    [SerializeField] private float gravity = -20f;

    [SerializeField] private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!PlayerManager.gameStart)
        {
            return;
        }

        //Increase speed base on time player survive
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.05f * Time.deltaTime;
        }

        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        //Player input
        if (controller.isGrounded)
        {
            // direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                FindObjectOfType<AudioManager>().PlaySound("Swipe");
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeDown && !isSilding)
        {
            FindObjectOfType<AudioManager>().PlaySound("Swipe");
            StartCoroutine(Slide());
        }

        if (SwipeManager.swipeRight)
        {
            FindObjectOfType<AudioManager>().PlaySound("Swipe");
            currentPath++;
            if (currentPath == 3)
            {
                currentPath = 2;
            }
        }

        if (SwipeManager.swipeLeft)
        {
            FindObjectOfType<AudioManager>().PlaySound("Swipe");
            currentPath--;
            if (currentPath == -1)
            {
                currentPath = 0;
            }
        }

        //Character's position
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (currentPath == 0)
        {
            targetPosition += Vector3.left * pathDistance;
        }
        else if (currentPath == 2)
        {
            targetPosition += Vector3.right * pathDistance;
        }

        // transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);

        if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.gameStart)
        {
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpHeight;
    }

    private IEnumerator Slide()
    {
        isSilding = true;
        animator.SetBool("isSlide", true);
        controller.center = new Vector3(0, 0.5f, 0);
        controller.height = 1f;
        yield return new WaitForSeconds(1.533f);

        animator.SetBool("isSlide", false);
        controller.center = new Vector3(0, 1f, 0);
        controller.height = 2f;
        isSilding = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("Game Over");
            FindObjectOfType<AudioManager>().StopSound("Main Theme");
            FindObjectOfType<AudioManager>().StopSound("Run");
        }
    }
}
