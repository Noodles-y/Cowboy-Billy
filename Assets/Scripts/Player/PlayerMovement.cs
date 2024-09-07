using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  public float acceleration = 1;
  public float maxSpeed = 10;
  public float jumpForce = 2;
  public float groundCheckRange = 1.1f;
  public float inputBufferWindow = 0.5f;
  
  private PlayerInputs inputs;
  private Rigidbody rb;
  private Vector2 inputMove = Vector2.zero;
  private float inputJump = -10;
  [SerializeReference] private bool grounded = false;
  [SerializeReference] private float currentVelocity;

  // Start is called before the first frame update
  void Start()
  {
    inputs = new PlayerInputs();
    inputs.Player.Enable();
    
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    inputMove = inputs.Player.Move.ReadValue<Vector2>();
    
    if(inputs.Player.Jump.WasPressedThisFrame())
    {
      inputJump = Time.time;
    }
  }
  
  void FixedUpdate() {
    CheckGround();
    
    //Apply forces
    ManageMovement();
    ManageJump();
    ManageRotation();
  }
  
  void CheckGround() {
    grounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckRange);
  }
  
  void ManageMovement() {    
    currentVelocity = rb.velocity.magnitude;
    
    if(currentVelocity < maxSpeed) {
      Vector3 force = inputMove.x * Vector3.right + inputMove.y * Vector3.forward;
      rb.AddForce(force * acceleration);
    }
  }
  
  void ManageJump() {
    if(grounded && Time.time - inputJump < inputBufferWindow)
    {
      // Apply jump force
      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
  }
  
  void ManageRotation() {
    if(inputMove.magnitude > 0.25f) {
      // Turn the player towards the move direction
      Vector3 target = transform.position + inputMove.x*Vector3.right + inputMove.y*Vector3.forward;
      transform.LookAt(target);
    }
  }
}
