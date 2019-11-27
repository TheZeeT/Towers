using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public GameObject Gates;
    public GameObject Castle;

    public GameObject Enemy;

    public int TotalNumberOfWaves;
    public int WaveNumber;

    int AmountOfMonstersInAWave;

    void Start()
    {
        AmountOfMonstersInAWave = 2;
    }

    void Update()
    {
        if(Input.GetKeyDown("n"))
        {
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        GameObject FreshEnemy = Instantiate(Enemy, Gates.transform.position + new Vector3(0, 0.5f, 0), Gates.transform.rotation);
        FreshEnemy.GetComponent<Enemy>().TargetTower = Castle;
        FreshEnemy.GetComponent<Enemy>().GoAndAttack();
    }
}
