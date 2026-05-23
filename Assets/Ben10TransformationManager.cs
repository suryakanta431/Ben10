using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class AlienTransformation
{
    public GameObject alienModel;
    public VideoClip[] transformationVideos;
}

public class Ben10TransformationManager : MonoBehaviour
{
    [Header("Alien Transformations (Index 0 = Ben)")]
    public AlienTransformation[] aliens;

    [Header("Video Components")]
    public VideoPlayer videoPlayer;
    public RawImage videoUI;

    [Header("UI")]
    public PanelToggle panelToggle;

    private int targetAlienIndex = 0;
    private bool isTransforming = false;

    void Start()
    {
        EnableOnlyAlien(0);

        if (videoUI != null)
            videoUI.gameObject.SetActive(false);

        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoFinished;
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Called from UI buttons
    public void TransformToAlien(int alienIndex)
    {
        if (isTransforming) return;
        if (alienIndex <= 0 || alienIndex >= aliens.Length) return;

        isTransforming = true;
        targetAlienIndex = alienIndex;

        // 🔹 Disable panel as soon as video starts
        if (panelToggle != null)
            panelToggle.DisablePanel();

        DisableAllAliens();

        VideoClip[] videos = aliens[alienIndex].transformationVideos;
        if (videos == null || videos.Length == 0)
        {
            OnVideoFinished(videoPlayer);
            return;
        }

        VideoClip selectedVideo = videos[Random.Range(0, videos.Length)];

        videoPlayer.clip = selectedVideo;
        videoUI.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoUI.gameObject.SetActive(false);
        EnableOnlyAlien(targetAlienIndex);
        isTransforming = false;

    }

    void EnableOnlyAlien(int index)
    {
        for (int i = 0; i < aliens.Length; i++)
        {
            aliens[i].alienModel.SetActive(i == index);
        }
    }

    void DisableAllAliens()
    {
        for (int i = 0; i < aliens.Length; i++)
        {
            aliens[i].alienModel.SetActive(false);
        }
    }
}
