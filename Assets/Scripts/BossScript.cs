using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossScript : MonoBehaviour
{
    [SerializeField] private SceneManagerScript sceneManagerScript;


    private void Awake()
    {
        sceneManagerScript = new SceneManagerScript();
    }
    private void OnDestroy()
    {
        Debug.Log("Boossss DIED");
        sceneManagerScript.LoadScene(2);
        
    }
}
