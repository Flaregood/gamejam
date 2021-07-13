using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private float PLAYER_DIST_SPAWN_PART = 50f;
    int dWidth = 6, height = 5;
    [SerializeField] GameObject[] middle;
    [SerializeField] GameObject[] edges;
    //middle1, edge1, middle2, edge2;
    private GameObject placeholder;

    [Range(75, 250)] [SerializeField] private int BiomeLength = 100;
    [Range(0, 250)] [SerializeField] private int TransitionLength = 100;
    [Range(0, 100)] [SerializeField] int roomSpawnChance;
    [Range(6, 15)] [SerializeField] int corridorWidth;
    [Range(1, 10)] [SerializeField] int heightDifferance;

    int startWidth, startHeight, pHeight;
    string[] tileType = {"Floor", "Edge"};



    [SerializeField] private GameObject player;
    [SerializeField] private GameObject MapStorage;
    int widthMin = 2, heightMin = 2;
    // Start is called before the first frame update
    void Start()
    {
        startWidth = dWidth;
        startHeight = height;
        if (TransitionLength >= BiomeLength)
        {
            TransitionLength = (int)BiomeLength;
        }
        Generation();

    }

    void Generation()
    {
        pHeight = height;
        float rnd = Random.Range(0, 100);
        if (rnd <= roomSpawnChance)
        {            
            dWidth = Random.Range(widthMin, corridorWidth);
            height += Random.Range(heightMin, heightDifferance*2);
            if (height > pHeight)
            {
                float heightdiferance = height - pHeight;
                //Debug.Log(heightdiferance);
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
        GameObject thing = null;
        //float rnd = Random.Range(0, middle.Length)*(player.transform.position.x/2);
        float rnd = player.transform.position.x + Random.Range(-TransitionLength/2, TransitionLength/2);
        if (middle.Length != 0)
        {
            if (type == "Floor")
            {
                rnd -= (middle.Length * BiomeLength) * (int)(rnd / (middle.Length * BiomeLength));
                thing = middle[(int)(rnd / BiomeLength)];
            }
        }
        if (edges.Length != 0)
        {
            if (type == "Edge")
            {
                rnd -= (edges.Length * BiomeLength) * (int)(rnd / (edges.Length * BiomeLength));
                thing = edges[(int)(rnd / BiomeLength)];
            }
        }
        else
        {
            thing = placeholder;
        }
     return thing;
    }
}
