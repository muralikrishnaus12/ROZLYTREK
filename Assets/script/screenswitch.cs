using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // The name of the scene to switch to
    public string sceneName;

    // References to the buttons
    private Button switchButton;
    private Button homeButton;

    void Start()
    {
        // Try to get the Button component from the same GameObject for scene switching
        switchButton = GetComponent<Button>();

        // If not found, try to get it from children (useful if the script is on a parent GameObject)
        if (switchButton == null)
        {
            switchButton = GetComponentInChildren<Button>();
        }

        // If switchButton is still not found, log an error
        if (switchButton == null)
        {
            Debug.LogError("Switch button component not found on the GameObject or its children.");
            return;
        }

        // Add a listener to the switchButton to call the SwitchScene method when clicked
        switchButton.onClick.AddListener(SwitchScene);

        try
        {
            // Find the home button by tag
            GameObject homeButtonObject = GameObject.FindGameObjectWithTag("home");

            // If the home button is found, get its Button component
            if (homeButtonObject != null)
            {
                homeButton = homeButtonObject.GetComponent<Button>();

                // If homeButton is found, add a listener to call the homeScene method when clicked
                if (homeButton != null)
                {
                    homeButton.onClick.AddListener(homeScene);
                }
                else
                {
                    Debug.LogError("Home button component not found on the GameObject with the 'home' tag.");
                }
            }
            else
            {
                throw new System.Exception("No GameObject found with the 'home' tag.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
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

    public void homeScene()
    {
        // Load the home scene
        SceneManager.LoadScene("menuscreen");
    }
}
