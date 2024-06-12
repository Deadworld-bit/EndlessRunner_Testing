using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] titlePrefabs;
    [SerializeField] private float titleSpawn = 0;
    [SerializeField] private float titleLength = 6;
    [SerializeField] private int numberOfTitles = 5;
    [SerializeField] private Transform playerTransform;

    private List<GameObject> activeTitles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTitles; i++)
        {
            if (i == 0)
            {
                SpawnTitle(0);
            }
            else
            {
                SpawnTitle(Random.Range(1, titlePrefabs.Length));
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 30 > titleSpawn - (numberOfTitles * titleLength))
        {
            SpawnTitle(Random.Range(1, titlePrefabs.Length));
            DeleteTitle();
        }
    }

    private void SpawnTitle(int tileIdex)
    {
        GameObject gameObject = Instantiate(titlePrefabs[tileIdex], transform.forward * titleSpawn, transform.rotation);
        activeTitles.Add(gameObject);
        titleSpawn += titleLength;
    }

    private void DeleteTitle()
    {
        Destroy(activeTitles[0]);
        activeTitles.RemoveAt(0);
    }
}
