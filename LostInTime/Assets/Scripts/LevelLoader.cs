using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public Transform player;
    public Transform newPlayerLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        if (player != null && newPlayerLocation != null)
        {
            player.position = newPlayerLocation.position;
        }
    }
}
