using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerBehavior : MonoBehaviour
{
    /*
     * Объект маркер (красный круг)
     * Атрибут Header добавляет подпись к компоненту в самом Unity
     * Атрибут SerializeField позволяет сделать поле приватным, но всё равно видимым в редакторе Unity
    */
    [Header("Add marker")] 
    [SerializeField]
    private GameObject PlaneMarkerPrefab;

    // Ссылка на менеджер лучей
    private ARRaycastManager arRaycastManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManagerScript = FindObjectOfType<ARRaycastManager>();

        PlaneMarkerPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMarkerPosition();
    }

    private void UpdateMarkerPosition()
    {
        // Список объектов, пересекаемых лучами
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        this.arRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits,
            TrackableType.Planes);

        // Если найдено хоть одно пересечение, то помещаем маркер на первую обнаруженную плоскость
        if (hits.Count > 0)
        {
            PlaneMarkerPrefab.transform.position = hits[0].pose.position;
            PlaneMarkerPrefab.SetActive(true);
        }
    }
}