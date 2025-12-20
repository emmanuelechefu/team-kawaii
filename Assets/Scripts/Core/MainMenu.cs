using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKey(KeyCode.Return))
            SceneManager.LoadScene("Tutorial");
    }
}