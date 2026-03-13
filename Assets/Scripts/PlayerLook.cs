using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    [Header("Settings Mouse")]
    public float mouseXSensitivity = 50f;
    public float mouseYSensitivity = 50f;

    [Header("Settings Keyboard/Panah")]
    public float keyboardSensitivityMultiplier = 8f; // Pengali khusus keyboard

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Cek apakah input berasal dari keyboard (biasanya nilainya bulat -1, 0, 1)
        // Kita gunakan pengali tambahan jika itu bukan dari gerakan mouse yang halus
        float currentXSensitivity = mouseXSensitivity;
        float currentYSensitivity = mouseYSensitivity;

        // Jika input sangat konsisten (seperti tekan tombol), kita perkuat
        if (Mathf.Abs(input.x) > 0 && Mathf.Abs(input.x) <= 1f) 
        {
            currentXSensitivity *= keyboardSensitivityMultiplier;
            currentYSensitivity *= keyboardSensitivityMultiplier;
        }

        // Hitung rotasi X (Atas/Bawah)
        xRotation -= (mouseY * Time.deltaTime) * currentYSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Terapkan ke kamera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Putar tubuh pemain (Kiri/Kanan)
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * currentXSensitivity);
    }
}