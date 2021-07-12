using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float PLAYER_DIST_SPAWN_PART = 50f;
    [SerializeField] int width, height;
    [SerializeField] GameObject middle1, edge1, middle2, edge2;
    [SerializeField] private GameObject player;
    string[] tileType = {"Floor", "Edge"};
    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        for (int x = (int)this.transform.position.x; x < this.transform.position.x+width; x++)
        {
            for (int y = (-height/2); y < height/2; y++)
            {
                spawnObj(PickTile("Floor"), x, y);
            }
            spawnObj(PickTile("Edge"), x, height/2);
            spawnObj(PickTile("Edge"), x, -height/2);
        }
    }

    void spawnObj(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < PLAYER_DIST_SPAWN_PART)
        {
            this.transform.position += new Vector3(width+1,0,0);
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
