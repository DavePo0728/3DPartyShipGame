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
    float timer = 0;
    [SerializeField]
    List<GameObject> RandomItemSpawnPoint;
    [SerializeField]
    List<GameObject> RandomItem;
    [SerializeField]
    List<ItemMove> RandomItemMove;
    [SerializeField]
    private int totalSpawnQuantity;
    private Dictionary<GameObject, int> itemWeights = new Dictionary<GameObject, int>();

    void Start()
    {
        // Initialize the dictionary with prefabs and their corresponding weights
        itemWeights.Add(RandomItem[0], 40);
        itemWeights.Add(RandomItem[1], 10);
        itemWeights.Add(RandomItem[2], 10);
        itemWeights.Add(RandomItem[3], 5);
        itemWeights.Add(RandomItem[4], 5);
        itemWeights.Add(RandomItem[5], 5);
        itemWeights.Add(RandomItem[6], 5);
        itemWeights.Add(RandomItem[7], 20);
        for (int i =0; i < RandomItem.Count; i++)
        {
            RandomItemMove[i] = RandomItem[i].GetComponent<ItemMove>();
        }
        // Call the spawn function
        StartCoroutine(SpawnWaveOne(totalSpawnQuantity, 15f));
    }
    private void Update()
    {
        
        timer += Time.deltaTime;
        TimerText.text = ((int)timer).ToString();
    }
    IEnumerator SpawnWaveOne(int quantity, float duration)
    {
        WaveText.text = "Wave1";
        float timeInterval = duration / quantity;

        for (int i = 0; i < quantity; i++)
        {
            GameObject itemToSpawn = GetRandomGameObject();
            Instantiate(itemToSpawn, GetRandomPosition(), Quaternion.identity);
            Debug.Log(timeInterval);
            yield return new WaitForSeconds(timeInterval);
        }
        totalSpawnQuantity = 30;
        itemWeights[RandomItem[0]] = 26;
        itemWeights[RandomItem[1]] = 10;
        itemWeights[RandomItem[2]] = 10;
        itemWeights[RandomItem[3]] = 7;
        itemWeights[RandomItem[4]] = 7;
        itemWeights[RandomItem[5]] = 13;
        itemWeights[RandomItem[6]] = 7;
        itemWeights[RandomItem[7]] = 20;
        RandomItemMove[0].flowSpeed = 0.8f;
        RandomItemMove[1].flowSpeed = 0.8f;
        RandomItemMove[2].flowSpeed = 0.8f;
        RandomItemMove[3].flowSpeed = 0.6f;
        RandomItemMove[4].flowSpeed = 0.6f;
        RandomItemMove[5].flowSpeed = 0.7f;
        RandomItemMove[6].flowSpeed = 0.7f;
        RandomItemMove[7].flowSpeed = 0.8f;
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
            Debug.Log(timeInterval);
            yield return new WaitForSeconds(timeInterval);
        }
        totalSpawnQuantity = 60;
        itemWeights[RandomItem[0]] = 26;
        itemWeights[RandomItem[1]] = 10;
        itemWeights[RandomItem[2]] = 10;
        itemWeights[RandomItem[3]] = 7;
        itemWeights[RandomItem[4]] = 7;
        itemWeights[RandomItem[5]] = 13;
        itemWeights[RandomItem[6]] = 7;
        itemWeights[RandomItem[7]] = 20;
        RandomItemMove[0].flowSpeed = 1.0f;
        RandomItemMove[1].flowSpeed = 1.0f;
        RandomItemMove[2].flowSpeed = 1.0f;
        RandomItemMove[3].flowSpeed = 0.8f;
        RandomItemMove[4].flowSpeed = 0.8f;
        RandomItemMove[5].flowSpeed = 0.9f;
        RandomItemMove[6].flowSpeed = 0.9f;
        RandomItemMove[7].flowSpeed = 1.0f;
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
            Debug.Log(timeInterval);
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
        GameObject spawnPoint = RandomItemSpawnPoint[Random.Range(0, 13)];
        return spawnPoint.transform.position;
    }
}
