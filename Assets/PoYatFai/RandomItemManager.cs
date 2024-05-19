using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomItemManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text WaveText;
    [SerializeField]
    TMP_Text TimerText;
    [SerializeField]
    private int totalSpawnQuantity;
    float timer = 60;
    [SerializeField]
    List<GameObject> RandomItemSpawnPoint;
    [SerializeField]
    List<GameObject> RandomItem;
    [SerializeField]
    List<ItemMove> RandomItemMove;

    private Dictionary<GameObject, int> itemWeights = new Dictionary<GameObject, int>();
    int lastSpawnPointNum = -1;
    int spawnPointNum;
    GameObject spawnPoint;
    void Start()
    {
        itemWeights.Add(RandomItem[0], 40);
        itemWeights.Add(RandomItem[1], 10);
        itemWeights.Add(RandomItem[2], 10);
        itemWeights.Add(RandomItem[3], 10);
        itemWeights.Add(RandomItem[4], 5);
        itemWeights.Add(RandomItem[5], 5);
        //itemWeights.Add(RandomItem[7], 20);
        for (int i =0; i < RandomItem.Count; i++)
        {
            RandomItemMove[i] = RandomItem[i].GetComponent<ItemMove>();
        }
        StartCoroutine(SpawnWaveOne(totalSpawnQuantity, 15f));
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        TimerText.text = string.Format("{0:F2}", timer);
    }
    IEnumerator SpawnWaveOne(int quantity, float duration)
    {
        WaveText.text = "Wave1";
        float timeInterval = duration / quantity;

        for (int i = 0; i < quantity; i++)
        {
            GameObject itemToSpawn = GetRandomGameObject();
            Instantiate(itemToSpawn, GetRandomPosition(), Quaternion.identity);
            //Debug.Log(timeInterval);
            yield return new WaitForSeconds(timeInterval);
        }
        totalSpawnQuantity = 22;
        itemWeights[RandomItem[0]] = 26;
        itemWeights[RandomItem[1]] = 10;
        itemWeights[RandomItem[2]] = 10;
        itemWeights[RandomItem[3]] = 14;
        itemWeights[RandomItem[4]] = 7;
        itemWeights[RandomItem[5]] = 13;

        RandomItemMove[0].flowSpeed = 0.8f;
        RandomItemMove[1].flowSpeed = 0.8f;
        RandomItemMove[2].flowSpeed = 0.8f;
        RandomItemMove[3].flowSpeed = 0.6f;
        RandomItemMove[4].flowSpeed = 0.6f;
        RandomItemMove[5].flowSpeed = 0.7f;
        StartCoroutine(SpawnWaveTwo(totalSpawnQuantity, 15f));
    }
    IEnumerator SpawnWaveTwo(int quantity, float duration)
    {
        WaveText.text = "Wave2";
        float timeInterval = duration / quantity;

        for (int i = 0; i < quantity; i++)
        {
            GameObject itemToSpawn = GetRandomGameObject();
            Instantiate(itemToSpawn, GetRandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(timeInterval);
        }
        totalSpawnQuantity = 45;
        itemWeights[RandomItem[0]] = 26;
        itemWeights[RandomItem[1]] = 15;
        itemWeights[RandomItem[2]] = 15;
        itemWeights[RandomItem[3]] = 14;
        itemWeights[RandomItem[4]] = 7;
        itemWeights[RandomItem[5]] = 13;

        RandomItemMove[0].flowSpeed = 1.0f;
        RandomItemMove[1].flowSpeed = 1.0f;
        RandomItemMove[2].flowSpeed = 1.0f;
        RandomItemMove[3].flowSpeed = 0.8f;
        RandomItemMove[4].flowSpeed = 0.8f;
        RandomItemMove[5].flowSpeed = 0.9f;

        StartCoroutine(SpawnLastWave(totalSpawnQuantity, 30f));
    }
    IEnumerator SpawnLastWave(int quantity, float duration)
    {
        WaveText.text = "LastWave";
        float timeInterval = duration / quantity;

        for (int i = 0; i < quantity; i++)
        {
            GameObject itemToSpawn = GetRandomGameObject();
            Instantiate(itemToSpawn, GetRandomPosition(), Quaternion.identity);
            //Debug.Log(timeInterval);
            yield return new WaitForSeconds(timeInterval);
        }
    }
    GameObject GetRandomGameObject()
    {
        int totalWeight = 0;
        foreach (var item in itemWeights)
        {
            totalWeight += item.Value;
        }

        int randomNumber = Random.Range(0, totalWeight);
        foreach (var item in itemWeights)
        {
            if (randomNumber < item.Value)
            {
                return item.Key;
            }
            randomNumber -= item.Value;
        }

        return null; // Should never happen
    }

    Vector3 GetRandomPosition()
    {
        do
        {
            spawnPointNum = Random.Range(0, 14); 
        } while (spawnPointNum == lastSpawnPointNum);

        lastSpawnPointNum = spawnPointNum;
        spawnPoint = RandomItemSpawnPoint[spawnPointNum];
        return spawnPoint.transform.position;
    }
}
