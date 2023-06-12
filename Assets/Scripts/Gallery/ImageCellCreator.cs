using UnityEngine;

public class ImageCellCreator : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private string _linkToServer;

    [Range(1, 66)] [SerializeField] private int _startFrom;
    [Range(1, 66)] [SerializeField] private int _endTo;

    [SerializeField] private GameObject _cell;


    private void Awake()
    {
        if (_endTo <= _startFrom)
            Debug.LogError("_endTo lesser than _startFrom");
        else
            CreateCells();
    }

    private void CreateCells()
    {
        float BottomY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;
        for (int i = _startFrom; i < _endTo+1; i++)
        {
            ImageCellSetup _cellSetup = Instantiate(_cell, _content).GetComponent<ImageCellSetup>();
            _cellSetup.URL = _linkToServer + i+".jpg";
            _cellSetup._bottomY = BottomY;
        }
    }
}
