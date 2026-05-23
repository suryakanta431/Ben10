using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    // Called from Ben10TransformationManager
    public void DisablePanel()
    {
        panel.SetActive(false);
    }

    public void EnablePanel()
    {
        panel.SetActive(true);
    }
}
