using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject patientPrefab;

    public int numPatients;

    public int count;
    public int speedTime;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numPatients; i++)
        {
            Instantiate(patientPrefab, transform.position, Quaternion.identity);
            count++;
        }

        Invoke(nameof(SpawnPatient), 5);

        Time.timeScale = speedTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPatient()
    {
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
        Invoke(nameof(SpawnPatient), Random.Range(2, 10));
        count++;
    }
}
