using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSpawner : MonoBehaviour
{

    [SerializeField] private DiscController m_discPrefab;
    [SerializeField] private float m_spawnFrequency = 2;
    [SerializeField] private Timer timer;
    [SerializeField] public float spawnMax;

    void Start()
    {
        StartCoroutine(C_Spawn());
    }

    private void SpawnDisc()
    {
        DiscController spawnedDisc = Instantiate(m_discPrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator C_Spawn()
    {
        yield return new WaitForSeconds(m_spawnFrequency);
        SpawnDisc();
        if(timer.elapsedTime < spawnMax)
            StartCoroutine(C_Spawn());
    }
}
