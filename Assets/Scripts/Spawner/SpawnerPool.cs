using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerPool : MonoBehaviour
{
    public float timeToSpawn = 5f;
    public float timeSinceSpawn;

    public int index;

    public GameObject playerPrefab;
    public static ObjectPool<GameObject> pool;

   

    private void Initialization()
    {
        pool = new ObjectPool<GameObject>
            (createFunc: () => Instantiate(playerPrefab,transform.position,Quaternion.identity),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false, defaultCapacity: 10, maxSize: 20);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        ControlSpawnTime();
    }

    public void ControlSpawnTime()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= timeToSpawn)
        {
            timeSinceSpawn = 0f;
            pool.Get();
            index++;
        }
    }
}
