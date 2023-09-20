//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description : This script is focused on gun to shoot , InputActionproperty is set for the player
 * Instantiate gun to collides with objects
 * Bulletdamage controlls the damage and collision
 * 
 */


using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public InputActionProperty LeftHand;
    public Transform gunMuzzle;
    public GameObject bulletPrefab;
    public float shootForce = 20f;
    public float shootCooldown = 0.5f; 
    private float lastShootTime = 0f;
    public Player player_code;
    public AudioManager audioManager;
    public GameManager gameManager;


    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
    }

    private void Update()
    {
        if (LeftHand.action.WasPressedThisFrame())
        {
            print("shoot");
            Shoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        audioManager.PlaySFX(audioManager.GunShot);
        GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(gunMuzzle.forward * shootForce, ForceMode.Impulse);
        bullet.GetComponent<BulletDamage>().player_code = player_code;
        bullet.GetComponent<BulletDamage>().gameManager = gameManager;
    }
}
