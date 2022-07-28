using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(
            () =>
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);
            });
    }
}
