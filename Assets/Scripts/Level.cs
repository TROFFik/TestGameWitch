using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private Pool _pool = null;
    [SerializeField] private int _maxWhitchCount = 0;
    [SerializeField] private int _spawnTime = 10;

    private List<Vector3> _peakPoints = new List<Vector3>();

    private float _maxHeight = 0;
    private float _minHeight = 0;

    private int _currentWhitchCount = 0;

    private bool _play = true;

    public float MaxHeight
    {
        get 
        {
            return _maxHeight;
        } 
    }

    public float MinHeight
    {
        get
        {
            return _minHeight;
        }
    }

    public int WhitchSCount
    {
        set
        {
            _currentWhitchCount -= value;
        }
    }

    public List<Vector3> PeakPoints
    {
        get
        {
            return _peakPoints;
        }

        set
        {
            _peakPoints = value;
        }
    }

    void Start()
    {
        PeakPointsCalculation();
        StartReloadTimer();
    }

    private void PeakPointsCalculation()
    {
        _minHeight = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 1)).y;
        _maxHeight = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 1)).y;

        for (float i = 0; i <= 1; i += 0.25f)
        {
            Vector3 tempPoint = _mainCamera.ViewportToWorldPoint(new Vector3(i, 0, 1));
            _peakPoints.Add(tempPoint);
        }
    }

    private void CreateWitch()
    {
        Witch newWitch = _pool.GetFreeObject(transform.position, transform).GetComponent<Witch>();

        newWitch.Level = this;
        newWitch.PeakPoints = _peakPoints;

        _currentWhitchCount++;
    }

    private async void StartReloadTimer()
    {

        while (_play)
        {
            if (_currentWhitchCount < _maxWhitchCount)
            {
                CreateWitch();
            }

            await Task.Delay(_spawnTime * 1000);
        }
    }

    private void OnDisable()
    {
        _play = false;
    }
}
