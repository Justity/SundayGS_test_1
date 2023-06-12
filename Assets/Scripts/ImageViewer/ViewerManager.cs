using UnityEngine;
using UnityEngine.UI;

public class ViewerManager : MonoBehaviour
{
    [SerializeField] private Image _imageToView;
    private void Awake()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        _imageToView.sprite = ImageSaver._ImageSaver._sprite;
    }
}
