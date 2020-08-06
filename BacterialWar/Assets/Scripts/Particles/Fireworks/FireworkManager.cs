using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkManager : MonoBehaviour
{
    private float _periodMin = 0.3f;
    private float _periodMax = 1.2f;

    private List<GameObject> _fireworkPrefabs;
    private float _currentPeriod;
    private float _time;

    private void Awake()
    {
        _fireworkPrefabs = FireworkCollection.Instance.GetAllFireworks();
    }

    private void Start()
    {
        _currentPeriod = Random.Range(_periodMin, _periodMax);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _currentPeriod)
        {
            _time -= _currentPeriod;

            ShowFirework();

            _currentPeriod = Random.Range(_periodMin, _periodMax);
        }
    }

    private void ShowFirework()
    {
        var fireworkIndex = Random.Range(0, _fireworkPrefabs.Count - 1);

        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Random.Range(Screen.width * 0.2f, Screen.width * 0.8f),
            Random.Range(Screen.height * 0.3f, Screen.height * 0.8f),
            -Camera.main.transform.position.z));

        Instantiate(_fireworkPrefabs[fireworkIndex], screenPosition, Quaternion.identity, gameObject.transform);
    }
}
