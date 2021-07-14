using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private float PLAYER_DIST_SPAWN_PART = 50f;
    int dWidth = 6, height = 6;
    [SerializeField] GameObject[] floor;
    [SerializeField] GameObject[] edgesMiddle;
    [SerializeField] GameObject[] edgesOuter;
    //middle1, edge1, middle2, edge2;
    private GameObject placeholder;

    [Range(75, 250)] [SerializeField] private int BiomeLength = 100;
    [Range(0, 250)] [SerializeField] private int TransitionLength = 100;
    [Range(0, 100)] [SerializeField] int roomSpawnChance;
    [Range(6, 15)] [SerializeField] int corridorWidth;
    [Range(1, 10)] [SerializeField] int heightDifferance;

    int startWidth, startHeight, pHeight;
    string[] tileType = {"Floor", "EdgeVertical","EdgeHorizontal"};



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
            //Start of the room
            if (height >= pHeight)
            {
                float heightdiferance = height - pHeight-1;
                //Debug.Log(heightdiferance);
                for (int i = 0; i < heightdiferance; i++)
                {
                    spawnObj(PickTile("EdgeVertical",3), (int)this.transform.position.x -1, (height - i) / 2+1); // Top
                    spawnObj(PickTile("EdgeVertical",3), (int)this.transform.position.x -1, (-height + i) / 2 +1);// Bottom
                    if (i == 0)
                    {
                    spawnObj(PickTile("EdgeHorizontal",2), (int)this.transform.position.x - 1, (-height + i) / 2 -1); 
                    }
                }
            }
        }
        else
        {
            //End of the room

            if (height > startHeight)
            {
                float heightdiferance = height - startHeight -1;
                for (int i = 0; i < heightdiferance; i++)
                {
                    spawnObj(PickTile("EdgeVertical",3), (int)this.transform.position.x, (height-i)/2+1); // Top
                    spawnObj(PickTile("EdgeVertical",3), (int)this.transform.position.x, (-height+i)/2 + 1); //Bottom
                    if (i == 0)
                    {
                        spawnObj(PickTile("EdgeHorizontal",2), (int)this.transform.position.x , (-height - i) / 2 - 1);
                    }
                }

            }
            dWidth = startWidth;
            height = startHeight;

        }
        for (int x = (int)this.transform.position.x; x < this.transform.position.x+dWidth; x++)
        {
            for (int y = (-height/2); y < height/2; y++)
            {
                spawnObj(PickTile("Floor",0), x, y);
            }
            spawnObj(PickTile("EdgeHorizontal",1), x, height/2);
            spawnObj(PickTile("EdgeHorizontal",1), x, -height/2-1);
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

    GameObject PickTile(string type, int layer)
    {
        GameObject thing = null;
        //float rnd = Random.Range(0, middle.Length)*(player.transform.position.x/2);
        float rnd = player.transform.position.x + Random.Range(-TransitionLength/2, TransitionLength/2);
        if (floor.Length != 0)
        {
            if (type == "Floor")
            {
                rnd -= (floor.Length * BiomeLength) * (int)(rnd / (floor.Length * BiomeLength));
                thing = floor[(int)(rnd / BiomeLength)];
                thing.GetComponent<SpriteRenderer>().sortingOrder = layer;
            }
        }
        if (edgesMiddle.Length != 0)
        {
            if (type == "EdgeVertical")
            {
                rnd -= (edgesMiddle.Length * BiomeLength) * (int)(rnd / (edgesMiddle.Length * BiomeLength));
                thing = edgesMiddle[(int)(rnd / BiomeLength)];
                thing.GetComponent<SpriteRenderer>().sortingOrder = layer;
            }
        }
        if (edgesOuter.Length != 0)
        {
            if (type == "EdgeHorizontal")
            {
                rnd -= (edgesOuter.Length * BiomeLength) * (int)(rnd / (edgesOuter.Length * BiomeLength));
                thing = edgesOuter[(int)(rnd / BiomeLength)];
                thing.GetComponent<SpriteRenderer>().sortingOrder = layer;
            }
        }
        else
        {
            thing = placeholder;
        }
     return thing;
    }
}
