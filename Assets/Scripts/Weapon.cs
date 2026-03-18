using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 100f; 
    public float bulletPrefabLifetime = 3f;

    [Header("Muzzle Flash Settings")]
    public GameObject muzzleFlashVisual; 
    public float flashDuration = 0.05f; 
    
    private AudioSource audioSource;   
    private Coroutine flashRoutine; 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (muzzleFlashVisual != null)
            muzzleFlashVisual.SetActive(false);
    }

    public void FireWeapon() 
    {
        Debug.Log("Senjata Menembak!");

        if (muzzleFlashVisual != null)
        {
            if (flashRoutine != null) StopCoroutine(flashRoutine); 
            flashRoutine = StartCoroutine(FlashMuzzle());
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(bulletSpawn.forward * bulletVelocity, ForceMode.VelocityChange);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        if (audioSource != null && audioSource.clip != null)
            audioSource.PlayOneShot(audioSource.clip); 
    }

    private IEnumerator FlashMuzzle()
    {
        muzzleFlashVisual.SetActive(true);
        
        ParticleSystem ps = muzzleFlashVisual.GetComponent<ParticleSystem>();
        if (ps != null) 
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Play();
        }

        yield return new WaitForSeconds(0.15f); // Naikkan dari 0.07 ke 0.15
        
        muzzleFlashVisual.SetActive(false);
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bullet != null) Destroy(bullet);
    }
}