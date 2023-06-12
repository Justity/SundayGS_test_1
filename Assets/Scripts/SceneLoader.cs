using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image _imageToFill;
    [SerializeField] private TextMeshProUGUI _progressText;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(SceneUtility.GetBuildIndexByScenePath(sceneName)));
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int PreviousSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (PreviousSceneIndex != 0)
            {
                StartCoroutine(LoadAsynchronously(PreviousSceneIndex-1));
            }
        }
    }

    private IEnumerator LoadAsynchronously (int sceneIndex)
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        float progress = operation.progress;
        while (progress < 0.9f)
        {
            progress = operation.progress;
            _imageToFill.fillAmount = progress;
            _progressText.text = progress * 100f + "%";
            yield return null;
        }
        yield return new WaitForSeconds(1.8f);
        _imageToFill.fillAmount = 1;
        _progressText.text = "100%";
        yield return new WaitForSeconds(.2f);
        operation.allowSceneActivation = true;
        yield return null;
    }
}
