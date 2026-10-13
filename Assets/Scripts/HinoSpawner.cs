using System;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class HinoSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _xSpawnRange;
    [SerializeField] private Vector2 _ySpawnRange;
    [SerializeField] private Vector2 _spawnerRateRange;
    
    [SerializeField] private GameObject _prefab;

    private float _timer;
    private float _currentTargetTime;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _currentTargetTime)
        {
            _timer = 0;
            SpawnHino();
            _currentTargetTime = Random.Range(_spawnerRateRange.x, _spawnerRateRange.y);
        }
    }
    
    private void SpawnHino()
    {
        GameObject hino = Instantiate(_prefab, transform.position, Quaternion.identity);
        float yTime = (float)Random.Range(0, 100) / 100;
        float xTime = (float)Random.Range(0, 100) / 100;
        hino.transform.position += 
            new Vector3(ChipironUtility.EvaluateVector2(_xSpawnRange,xTime),  
                ChipironUtility.EvaluateVector2(_ySpawnRange,yTime));

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector2 center = new Vector2((_xSpawnRange.x + _xSpawnRange.y) / 2 + transform.position.x,
            (_ySpawnRange.x + _ySpawnRange.y) / 2 + transform.position.y);
        Vector2 size = new Vector2(Mathf.Abs(_xSpawnRange.x -_xSpawnRange.y),
            Mathf.Abs(_ySpawnRange.x - _ySpawnRange.y)) ;
        Gizmos.DrawWireCube(center,size);
    }
}
