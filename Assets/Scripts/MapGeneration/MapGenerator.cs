using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private float PLAYER_DIST_SPAWN_PART = 50f;
    int dWidth = 6, height = 6;
    [SerializeField] private List<Floors> floor;
    [SerializeField] private List<EdgeMiddle> wallTops;
    [SerializeField] private List<EdgeOuter> wallMiddle;
    [SerializeField] private List<Biomes> EnemyBiomes;
    [SerializeField] GameObject BaseEnemy;
    [SerializeField] private List<Ability> abilitys;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] GameObject abilityPickup;
    [SerializeField] GameObject autoAttackMod;
    //[SerializeField] GameObject[] enemys;
    private GameObject placeholder;

    [Range(75, 250)] [SerializeField] private int BiomeLength = 100;
    [Range(0, 250)] [SerializeField] private int TransitionLength = 100;
    [Range(0, 100)] [SerializeField] int roomSpawnChance;
    [Range(0f, 2f)] [SerializeField] float enemySpawnChanceIncrease;
    [Range(6, 15)] [SerializeField] int corridorWidth;
    [Range(1, 10)] [SerializeField] int heightDifferance;
    [Range(0.000001f, 5f)] [SerializeField] float TileSpawnThing;
    [Range(0.000001f, 5f)] [SerializeField] float WallTopThingy;
    [Range(0.000001f, 5f)] [SerializeField] float Wallmiddlethingy;
    float Enemythingy;
    int startWidth, startHeight, pHeight;
    string[] tileType = { "Floor", "EdgeVertical", "EdgeHorizontal" };



    [SerializeField] private GameObject player;
    [SerializeField] private GameObject MapStorage;
    [SerializeField] private GameObject EnemyStorage;
    int widthMin = 2, heightMin = 2;


    [System.Serializable]
    public class Biomes
    {
        public List<EnemyStats> Enemys;
        public Biomes()
        {

        }
    }
    [System.Serializable]
    public class Floors
    {
        public List<GameObject> FloorTiles;
        public Floors()
        {

        }
    }
    [System.Serializable]
    public class EdgeMiddle
    {
        public List<GameObject> TopTiles;
        public EdgeMiddle()
        {

        }
    }
    [System.Serializable]
    public class EdgeOuter
    {
        public List<GameObject> WallMiddle;
        public EdgeOuter()
        {

        }
    }
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

    void Update()
    {
        if (this.transform.position.x- player.transform.position.x <= PLAYER_DIST_SPAWN_PART)
        {
            this.transform.position += new Vector3(dWidth + 1, 0, 0);
            Generation();
        }
        Enemythingy =   500 / player.transform.position.x+1;
    }





    void Generation()
    {
        pHeight = height;
        float rnd = Random.Range(0, 100);
        if (rnd <= roomSpawnChance)
        {
            dWidth = Random.Range(widthMin, corridorWidth);
            height += Random.Range(heightMin, heightDifferance * 2);
            //Start of the room
            if (height >= pHeight)
            {
                float heightdiferance = height - pHeight - 1;
                //Debug.Log(heightdiferance);
                for (int i = 0; i < heightdiferance; i++)
                {
                    spawnObj(PickTile("EdgeVertical", 3), (int)this.transform.position.x - 1, (height - i) / 2 + 1); // Top
                    spawnObj(PickTile("EdgeVertical", 3), (int)this.transform.position.x - 1, (-height + i) / 2 + 1);// Bottom
                    if (i == 0)
                    {
                        spawnObj(PickTile("EdgeHorizontal", 2), (int)this.transform.position.x - 1, (-height + i) / 2 - 1);
                    }
                }
            }
        }
        else
        {
            //End of the room

            if (height > startHeight)
            {
                float heightdiferance = height - startHeight - 1;
                for (int i = 0; i < heightdiferance; i++)
                {
                    spawnObj(PickTile("EdgeVertical", 3), (int)this.transform.position.x, (height - i) / 2 + 1); // Top
                    spawnObj(PickTile("EdgeVertical", 3), (int)this.transform.position.x, (-height + i) / 2 + 1); //Bottom
                    if (i == 0)
                    {
                        spawnObj(PickTile("EdgeHorizontal", 2), (int)this.transform.position.x, (-height - i) / 2 - 1);
                    }
                }

            }
            dWidth = startWidth;
            height = startHeight;

        }
        for (int x = (int)this.transform.position.x; x < this.transform.position.x + dWidth; x++)
        {
            for (int y = (-height / 2); y < height / 2; y++)
            {
                spawnObj(PickTile("Floor", 0,new Vector2(x,y)), x, y);
            }
            spawnObj(PickTile("EdgeHorizontal", 1), x, height / 2);
            spawnObj(PickTile("EdgeHorizontal", 1), x, -height / 2 - 1);
        }
    }

    void spawnObj(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity, MapStorage.transform);
    }

    void SpawnEnemy(EnemyStats noice, int width, int height, int layer)
    {
        GameObject obj = BaseEnemy;
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity, EnemyStorage.transform);
        obj.GetComponent<EnemyController>().stats = noice;
        obj.GetComponent<SpriteRenderer>().sortingOrder = layer;
    }

    void SpawnWeapon(Weapon noice, int width, int height, int layer)
    {
        GameObject obj = autoAttackMod;
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity, EnemyStorage.transform);
        obj.GetComponent<WeaponPickupEntity>().storedWeapon = noice;
        obj.GetComponent<SpriteRenderer>().sortingOrder = layer;
    }

    void SpawnAbility(Ability noice, int width, int height, int layer)
    {
        GameObject obj = abilityPickup;
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity, EnemyStorage.transform);
        obj.GetComponent<AbilityPickupEntity>().storedAbility = noice;
        obj.GetComponent<SpriteRenderer>().sortingOrder = layer;
    }

    // Update is called once per frame

    GameObject PickTile(string type, int layer)
    {
        GameObject thing = null;
        GameObject tempEnemy = null;
        float rnd = player.transform.position.x + Random.Range(-TransitionLength / 2, TransitionLength / 2);
        if (wallTops.Count != 0)
        {
            if (type == "EdgeVertical")
            {
                rnd -= (wallTops.Count * BiomeLength) * (int)(rnd / (wallTops.Count * BiomeLength));

                int floortile = Mathf.RoundToInt(((Random.Range(0f, wallTops[(int)(rnd / BiomeLength)].TopTiles.Count) * Random.Range(0f, wallTops[(int)(rnd / BiomeLength)].TopTiles.Count)) / (float)(wallTops[(int)(rnd / BiomeLength)].TopTiles.Count * WallTopThingy)));

                if (floortile >= wallTops[(int)(rnd / BiomeLength)].TopTiles.Count)
                {
                    floortile = wallTops[(int)(rnd / BiomeLength)].TopTiles.Count - 1;
                }
                if (floortile < 0)
                {
                    floortile = 0;
                }
                thing = wallTops[(int)(rnd / BiomeLength)].TopTiles[floortile];
                thing.GetComponent<SpriteRenderer>().sortingOrder = layer;
            }
        }
        if (wallMiddle.Count != 0)
        {
            if (type == "EdgeHorizontal")
            {
                rnd -= (wallMiddle.Count * BiomeLength) * (int)(rnd / (wallMiddle.Count * BiomeLength));

                int floortile = Mathf.RoundToInt(((Random.Range(0f, wallMiddle[(int)(rnd / BiomeLength)].WallMiddle.Count) * Random.Range(0f, wallMiddle[(int)(rnd / BiomeLength)].WallMiddle.Count)) / (float)(wallMiddle[(int)(rnd / BiomeLength)].WallMiddle.Count * Wallmiddlethingy)));

                if (floortile >= wallMiddle[(int)(rnd / BiomeLength)].WallMiddle.Count)
                {
                    floortile = wallMiddle[(int)(rnd / BiomeLength)].WallMiddle.Count - 1;
                }
                if (floortile < 0)
                {
                    floortile = 0;
                }
                thing = wallMiddle[(int)(rnd / BiomeLength)].WallMiddle[floortile];
                thing.GetComponent<SpriteRenderer>().sortingOrder = layer;
            }
        }
        else
        {
            thing = placeholder;
        }
        return thing;
    }

    GameObject PickTile(string type, int layer, Vector2 pos)
    {
        GameObject thing = null;
        float rnd = player.transform.position.x + Random.Range(-TransitionLength / 2, TransitionLength / 2);
        if (floor.Count != 0)
        {
            if (type == "Floor")
            {
                
                rnd -= (floor.Count * BiomeLength) * (int)(rnd / (floor.Count * BiomeLength));

               int floortile = Mathf.RoundToInt(((Random.Range(0f, floor[(int)(rnd / BiomeLength)].FloorTiles.Count) * Random.Range(0f, floor[(int)(rnd / BiomeLength)].FloorTiles.Count)) / (float)(floor[(int)(rnd / BiomeLength)].FloorTiles.Count * TileSpawnThing)));
                
                if (floortile >= floor[(int)(rnd / BiomeLength)].FloorTiles.Count)
                {
                    floortile = floor[(int)(rnd / BiomeLength)].FloorTiles.Count - 1;
                }
                if (floortile < 0)
                {
                    floortile = 0;
                }
                thing = floor[(int)(rnd / BiomeLength)].FloorTiles[floortile];
                thing.GetComponent<SpriteRenderer>().sortingOrder = layer;


                Debug.Log(thing.name);
                float random = Random.Range(0f, 100f);
                float enemySpawnChance = Mathf.Sqrt((0.03f* player.transform.position.x + height/ 50)*enemySpawnChanceIncrease);
                if (random < enemySpawnChance)
                {
                    int EnemyChance = Mathf.RoundToInt(((Random.Range(0f, EnemyBiomes[(int)(rnd / BiomeLength)].Enemys.Count) * Random.Range(0f, EnemyBiomes[(int)(rnd / BiomeLength)].Enemys.Count)) / (float)(EnemyBiomes[(int)(rnd / BiomeLength)].Enemys.Count * Enemythingy)));
                    if (EnemyChance >= EnemyBiomes[(int)(rnd / BiomeLength)].Enemys.Count)
                    {
                        EnemyChance = EnemyBiomes[(int)(rnd / BiomeLength)].Enemys.Count - 1;
                    }
                    if (EnemyChance < 0)
                    {
                        EnemyChance = 0;
                    }
                    SpawnEnemy(EnemyBiomes[(int)(rnd / BiomeLength)].Enemys[EnemyChance], (int)pos.x, (int)pos.y,layer+1);
                }

                if (height > startHeight + heightDifferance * 3)
                {
                    float R = Random.Range(0f, 100f);
                    float abilitySpawnChance = Mathf.Sqrt((0.03f * player.transform.position.x + height / 100) * enemySpawnChanceIncrease);
                    if (R < abilitySpawnChance)
                    {
                        SpawnAbility(abilitys[Random.Range(0, abilitys.Count)], (int)pos.x, (int)pos.y, layer + 1);
                    }
                }
                
            }
        }
        else
        {
            thing = placeholder;
        }
        return thing;
    }
}
