using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(FindFirstObjectByType<PlayerController>().gameObject);
            SceneManager.LoadScene(1);
        }

        if (SceneManager.GetActiveScene().buildIndex != 2 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }
    }
}
