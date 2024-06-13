using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 1f;

    void Start()
    {

    } // Start

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    } // Update
} // SceneLoader