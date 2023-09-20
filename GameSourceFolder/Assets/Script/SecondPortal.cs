using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player_code;

    private void Start()
    {
        player_code = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player hit portal");
            SceneManager.LoadScene("GameWin");
        }
    }
}
