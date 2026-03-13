using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    
    public float speed = 5f;
    public float gravity = -9.8f; // Tambahkan gravitasi
    public float jumpHeight = 1.5f;

    private bool crouching = false;
    private bool lerpCrouch = false;
    private bool sprinting = false;
    private float crouchTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch){
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);
        
            if (p >= 1){
                lerpCrouch = false;
                crouchTimer = 0;}
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // Gerakkan karakter berdasarkan input
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Logika Gravitasi sederhana
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -1.5f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        lerpCrouch = true;
        crouchTimer = 0;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8f; // Kecepatan sprint
        else
            speed = 5f; // Kecepatan normal
    }
}