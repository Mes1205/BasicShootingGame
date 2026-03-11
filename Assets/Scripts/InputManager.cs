using System.Collections;
using System.Collections.Generic; // Perbaikan: 'using' bukan 'USING'
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot; // Perbaikan: Biasanya ada akhiran 's'

    private PlayerMotor motor;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
    }

    void FixedUpdate()
    {
        // Mengirimkan input Vector2 ke PlayerMotor
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}