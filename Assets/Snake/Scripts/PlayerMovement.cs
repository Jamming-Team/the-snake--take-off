using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
         CharacterController _controller;
         [SerializeField] float speed = 12f;
         [SerializeField] float gravity = -9.81f;
         [SerializeField] float jumpHeight = 3f;

         [SerializeField] Transform groundCheck;
         [SerializeField] float groundDistance = 0.4f;
         [SerializeField] LayerMask groundMask;
         
         Vector3 velocity;
         bool isGrounded;
         
         void Start()
         {
             _controller = GetComponent<CharacterController>();
         }

         void Update()
         {
             isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

             if (isGrounded && velocity.y < 0)
             {
                 velocity.y = -2f;
             }
             
             float x = Input.GetAxis("Horizontal");
             float z = Input.GetAxis("Vertical");
             
             Vector3 move = transform.right * x + transform.forward * z;
             _controller.Move(move * speed * Time.deltaTime);

             if (Input.GetButtonDown("Jump") && isGrounded)
             {
                 velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
             }

             velocity.y += gravity * Time.deltaTime;
             _controller.Move(velocity * Time.deltaTime);
         }
    }
}
