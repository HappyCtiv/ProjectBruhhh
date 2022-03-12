using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaerMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpForce = 2f;
    public float runSpeed = 18f;
    private float speedSave;

    public bool isVisible = true;// invis check

    private float SpeedPrevious;
    [SerializeField] float boostTimer = 5f;
    private bool boosted = false;
    public GameObject Hands;

	public KeyCode RunKey = KeyCode.LeftShift;


    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool IsGrounded;
    

    public AudioClip JumpSound;
    AudioSource audioSource;


    void Start()
    {
        speedSave = speed;
        SpeedPrevious = speed;
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (IsGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        if (!isVisible)
        {
            Hands.SetActive(false);
        }
        else
        {
            Hands.SetActive(true);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;     
        
	    if (Input.GetKey(RunKey))
        {
            speed = runSpeed;
        }
        else
        {
            speed = speedSave;
        }


        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetButtonDown ("Jump") && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce* -2f* gravity);
            audioSource.PlayOneShot(JumpSound);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Debug.Log("Player speed: " + speed);


        // Can be used for any type of boost.
        if (boostTimer > 0.1f && (boosted == true || isVisible == false))
        {    
            boostTimer -= Time.deltaTime;
        }
        else if (boostTimer <= 0.1f)
        {
            speedSave = SpeedPrevious;
            boosted = false;
            isVisible = true;
            boostTimer = 5f;
            Debug.Log ("You are visible");
        }
    }

    public void SpeedMultipl(float speedMult)
    {
        boosted = true;
        SpeedPrevious = speedSave;
        speedSave *= speedMult;
    }
}
