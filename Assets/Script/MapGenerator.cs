using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float PLAYER_DIST_SPAWN_PART = 50f;
    int dWidth = 6, height = 5;
    [SerializeField] GameObject middle1, edge1, middle2, edge2;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject MapStorage;
    int startWidth, startHeight, pHeight;
    string[] tileType = {"Floor", "Edge"};

    [Range(0, 100)] [SerializeField] int roomSpawnChance;
    [Range(6, 15)] [SerializeField] int corridorWidth;
    [Range(2, 10)][SerializeField] int heightDifferance;
    int widthMin = 2, heightMin = 2;
    // Start is called before the first frame update
    void Start()
    {
        startWidth = dWidth;
        startHeight = height;
        Generation();
    }

    void Generation()
    {
        pHeight = height;
        float rnd = Random.Range(0, 100);
        if (rnd <= roomSpawnChance)
        {            
            dWidth = Random.Range(widthMin, corridorWidth);
            height += Random.Range(heightMin, heightDifferance);
            if (height > pHeight)
            {
                float heightdiferance = height - pHeight;
                Debug.Log(heightdiferance);
                for (int i = 0; i < heightdiferance; i++)
                {
                    spawnObj(PickTile("Edge"), (int)this.transform.position.x -1, (height - i) / 2);
                    spawnObj(PickTile("Edge"), (int)this.transform.position.x -1, (-height + i) / 2 - 1);
                }
            }
        }
        else
        {
            if (height > startHeight)
            {
                float heightdiferance = height - startHeight;
                for (int i = 0; i < heightdiferance; i++)
                {
                    spawnObj(PickTile("Edge"), (int)this.transform.position.x, (height-i)/2);
                    spawnObj(PickTile("Edge"), (int)this.transform.position.x, (-height+i)/2 - 1);
                }
            }
            dWidth = startWidth;
            height = startHeight;
        }
        for (int x = (int)this.transform.position.x; x < this.transform.position.x+dWidth; x++)
        {
            for (int y = (-height/2); y < height/2; y++)
            {
                spawnObj(PickTile("Floor"), x, y);
            }
            spawnObj(PickTile("Edge"), x, height/2);
            spawnObj(PickTile("Edge"), x, -height/2-1);
        }
    }

    void spawnObj(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity, MapStorage.transform);     
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < PLAYER_DIST_SPAWN_PART)
        {
            this.transform.position += new Vector3(dWidth+1,0,0);
            Generation();
        }
    }

    GameObject PickTile(string type)
    {
        GameObject thing;
        float rnd = Random.Range(2, 60)*player.transform.position.x;
        if (type == "Floor")
        {
            if (rnd <= 100)
            {
                thing = middle1;
            }
            else if (rnd >= 100)
            {
                thing = middle2;
            }
            else
            {
                thing = middle1;
            }
        }
        else if (type == "Edge")
        {
            {
                if (rnd <= 100)
                {
                    thing = edge1;
                }
                else if (rnd >= 100)
                {
                    thing = edge2;
                }
                else
                {
                    thing = edge1;
                }
            }
        }
        else
        {
            thing = edge1;
        }
     return thing;
    }
}
