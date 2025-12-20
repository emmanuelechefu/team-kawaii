using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string lobbySceneName = "Lobby";
    [SerializeField] public string nextLevelScene = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"EXIT TRIGGER HIT by: {other.name} | tag={other.tag}");

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not player, ignoring.");
            return;
        }

        Debug.Log($"Loading scene: {nextLevelScene}");
        SceneManager.LoadScene(nextLevelScene);
    }
}
