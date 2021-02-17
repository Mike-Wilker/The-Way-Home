using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    float timeRemaining = 120.0f;
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            SceneManager.LoadScene("Victory");
        }
        GetComponent<Text>().text = Mathf.Floor(timeRemaining).ToString();
    }
}
