using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    [Header("Check this option if you are overriding the spawn as indicated below.")]
    public bool overridePlayerSpawnLocation = false;
    [Header("newPlayerLocation should be the location in the scene that will be loaded.\n" +
        "An easy way to do this is to copy the position component from a game\n" +
        "object in that scene and paste it into this field.")]
    public Vector3 newPlayerLocation;
    public Vector3 newPlayerRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (overridePlayerSpawnLocation)
        {
            GameManager.SetLoadingSpawn(newPlayerLocation, newPlayerRotation);
        }
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
