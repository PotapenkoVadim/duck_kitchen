using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatesCounterVisual: MonoBehaviour
{
  [SerializeField] private Transform _counterTopPoint;
  [SerializeField] private Transform _plateVisualPrefab;
  [SerializeField] private PlatesCounter _platesCounter;

  private List<GameObject> _plateVisualGameObjectList;

  private void Awake()
  {
    _plateVisualGameObjectList = new List<GameObject>();
  }

  private void OnEnable()
  {
    _platesCounter.OnPlateSpawned += HandlePlateSpawned;
    _platesCounter.OnPlateRemoved += HandlePlateRemoved;
  }

  private void OnDisable()
  {
    _platesCounter.OnPlateSpawned -= HandlePlateSpawned;
    _platesCounter.OnPlateRemoved -= HandlePlateRemoved;
  }

  private void HandlePlateSpawned(object sender, EventArgs e)
  {
    Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

    float plateOffsetY = .1f;
    plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);
    _plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
  }

  private void HandlePlateRemoved(object sender, EventArgs e)
  {
    GameObject plateGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
    _plateVisualGameObjectList.Remove(plateGameObject);
    Destroy(plateGameObject);
  }
}