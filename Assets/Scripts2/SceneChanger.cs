using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void ChangeSceneTo()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
