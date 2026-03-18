using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot; 

    private PlayerMotor motor;
    private PlayerLook look;
    private Weapon weapon;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weapon = GetComponent<Weapon>();

        // Berikan perlindungan agar tidak error jika weapon/motor kosong
        onFoot.Shoot.performed += ctx => {
            if (weapon != null) weapon.FireWeapon();
        };

        onFoot.Jump.performed += ctx => {
            if (motor != null) motor.Jump();
        };

        onFoot.Crouch.performed += ctx => {
            if (motor != null) motor.Crouch();
        };

        onFoot.Sprint.performed += ctx => {
            if (motor != null) motor.Sprint();
        };
    }

    void FixedUpdate()
    {
        if (motor != null)
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        if (look != null)
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerInput.Enable(); // MENYALAKAN SEMUA INPUT
    }

    private void OnDisable()
    {
        playerInput.Disable(); // MEMATIKAN SEMUA INPUT
    }
}