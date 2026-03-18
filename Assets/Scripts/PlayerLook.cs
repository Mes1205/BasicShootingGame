using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    
    private float xRotation = 0f;

    [Header("Settings Mouse")]
    public float mouseXSensitivity = 50f;
    public float mouseYSensitivity = 50f;

    [Header("Settings Keyboard/Panah")]
    public float keyboardSensitivityMultiplier = 8f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        float currentXSensitivity = mouseXSensitivity;
        float currentYSensitivity = mouseYSensitivity;

        if (Mathf.Abs(input.x) > 0 && Mathf.Abs(input.x) <= 1f) 
        {
            currentXSensitivity *= keyboardSensitivityMultiplier;
            currentYSensitivity *= keyboardSensitivityMultiplier;
        }

        // Hitung rotasi X (Atas/Bawah)
        xRotation -= (mouseY * Time.deltaTime) * currentYSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // 1. Terapkan ke kamera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Putar tubuh pemain (Kiri/Kanan)
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * currentXSensitivity);
    }
}