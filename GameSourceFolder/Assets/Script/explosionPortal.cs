using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class explosionPortal : MonoBehaviour
{
    public GameObject explosionParticlePrefab;
    public GameObject portalParticlePrefab;
    public Transform portalSpawnPoint;
    public GameObject door; // Reference to the door GameObject
    public float portalExistDuration = 60f;
    private bool explosionTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!explosionTriggered && collision.gameObject.CompareTag("door"))
        {
            // Instantiate explosion particle
            Instantiate(explosionParticlePrefab, collision.contacts[0].point, Quaternion.identity);

            // Deactivate or destroy the door GameObject
            door.SetActive(false); // Or Destroy(door) if you want to completely remove it

            // Instantiate portal particle
            GameObject portal = Instantiate(portalParticlePrefab, portalSpawnPoint.position, portalSpawnPoint.rotation);
            StartCoroutine(ActivatePortalForDuration(portal));

            // Set the explosionTriggered flag to true
            explosionTriggered = true;
        }
    }

    private IEnumerator ActivatePortalForDuration(GameObject portal)
    {
        yield return new WaitForSeconds(portalExistDuration);
        Destroy(portal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (explosionTriggered && other.CompareTag("Player"))
        {
            // Load the next scene when the player collides with the portal
            SceneManager.LoadScene("Advanced");
        }
    }
}
