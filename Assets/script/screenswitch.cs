using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // The name of the scene to switch to
    public string sceneName;

    // Reference to the button
    private Button button;

    void Start()
    {
        // Try to get the Button component from the same GameObject
        button = GetComponent<Button>();

        // If not found, try to get it from children (useful if the script is on a parent GameObject)
        if (button == null)
        {
            button = GetComponentInChildren<Button>();
        }

        // If button is still not found, log an error
        if (button == null)
        {
            Debug.LogError("Button component not found on the GameObject or its children.");
            return;
        }

        // Add a listener to the button to call the SwitchScene method when clicked
        button.onClick.AddListener(SwitchScene);
    }

    public void SwitchScene()
    {
        // Check if the scene name is set
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set in the SceneSwitcher script.");
        }
    }
}
