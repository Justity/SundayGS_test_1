using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageCellSetup : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rt;
    [HideInInspector] public string URL;
    [HideInInspector] public float _bottomY;

    private Sprite _sprite;
    
    private void Start()
    {
        StartCoroutine(_waitToStartLoad());
    }

    public void ClickedToImage()
    {
        if (_sprite != null)
        {
            ImageSaver._ImageSaver.OpenImage(_sprite);
        }
    }
    
    private IEnumerator _waitToStartLoad()
    {
        yield return new WaitForEndOfFrame();
        while (_rt.position.y<=_bottomY-_rt.rect.height)
            yield return new WaitForSeconds(.3f);

        LoadImage(URL);
        yield return null;
    }
    

    private async void LoadImage(string URL)
    {
        Texture2D _texture = await GetTextureFromServer(URL);
        _sprite = Sprite.Create(await GetTextureFromServer(URL), new Rect(0.0f, 0.0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        _image.sprite = _sprite;
        _image.color = new Color(1, 1, 1, 1);
    }
    
    private async Task<Texture2D>GetTextureFromServer(string URL)
    {
        using( UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL) )
        {
            var asyncOp = www.SendWebRequest();

            while( asyncOp.isDone==false )
                await Task.Delay(1000/30);
        
            if( www.result!=UnityWebRequest.Result.Success )
            {
                return null;
            }
            else
            {
                return DownloadHandlerTexture.GetContent(www);
            }
        }
        
        return null;
    }
}
