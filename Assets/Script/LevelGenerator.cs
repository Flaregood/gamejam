using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DIST_SPAWN_PART = 50f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private Transform LevelPart_1;
    [SerializeField] private GameObject player;


    private Vector3 lastEndPosition;
    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        SpawnLevelPart();
        SpawnLevelPart();
        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }

    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DIST_SPAWN_PART)
        {
            SpawnLevelPart();
        }
    }
    private void SpawnLevelPart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(LevelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
