using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private Sprite[] _statesSprites = new Sprite[3]; 
    [SerializeField] private float _speed = 0.0066f;
    [SerializeField] private int _health = 3;

    private SpriteRenderer _spriteRenderer = null;
    private Level _level = null;
    private PoolObject _poolObject = null;

    private List<Vector3> _peakPoints = new List<Vector3>();

    private int _currentHealth = 3;

    private float _currentSpeed = 0.0066f;
    private float _currentDistance = 0;

    public List<Vector3> PeakPoints
    {
        set
        {
            _peakPoints = value;
        }
    }

    public Level Level
    {
        set
        {
            _level = value;
        }
    }

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _poolObject = gameObject.GetComponent<PoolObject>();
    }

    private void FixedUpdate()
    {
        PositionCalculation();
    }

    private void PositionCalculation()
    {
        if (_currentDistance == 0 || _currentDistance == 1)
        {
            _currentSpeed = -_currentSpeed;

            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            PeakPointCalculation();
        }

        transform.position = BezierCalculation.GetPoint(_peakPoints, _currentDistance)[0];

        _currentDistance += _currentSpeed;
        _currentDistance = Mathf.Clamp01(_currentDistance);
    }

    private void PeakPointCalculation()
    {
        for (int i = 0; i < _peakPoints.Count; i++)
        {
            float height = Random.Range(_level.MinHeight, _level.MaxHeight + 1);

            Vector3 newPoint = new Vector3(_level.PeakPoints[i].x, height, 0);

            _peakPoints[i] = newPoint;
        }
    }

    public void Hit()
    {
        _currentHealth--;

        if (_currentHealth < 0)
        {
            _poolObject.RetunToPool();
            _currentSpeed = _speed;
            _currentHealth = _health;
            _spriteRenderer.sprite = _statesSprites[_currentHealth];
            _currentDistance = 0;

            _level.WhitchSCount = 1;
        }
        else
        {
            _spriteRenderer.sprite = _statesSprites[_currentHealth];
            _currentSpeed /= 2;
        }
    }
}
