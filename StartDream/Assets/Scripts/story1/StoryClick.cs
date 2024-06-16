using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryClick : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Start()
    {
        OnStartButtonClick();
    }
   
}
