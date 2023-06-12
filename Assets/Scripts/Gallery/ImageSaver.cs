using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageSaver : MonoBehaviour
{
    [HideInInspector] public static ImageSaver _ImageSaver;
    [HideInInspector] public Sprite _sprite;

    [SerializeField] private SceneLoader _sceneLoader;
    
    private void Awake()
    {
        if (ImageSaver._ImageSaver == null)
        {
            _ImageSaver = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    public void OpenImage(Sprite sprite)
    {
        _sprite = sprite;
        _sceneLoader.LoadLevel("3 - Image_Viewer");
    }

    
}
