using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscActivator : MonoBehaviour
{

    [SerializeField] private GameObject m_Disc0;
    [SerializeField] private GameObject m_Disc1;
    [SerializeField] private GameObject m_Disc2;
    [SerializeField] private GameObject m_Disc3;
    [SerializeField] private GameObject m_Cont0;
    [SerializeField] private GameObject m_Cont1;
    [SerializeField] private GameObject m_Cont2;
    [SerializeField] private GameObject m_Cont3;
    [SerializeField] private GameObject m_ContBase;
    [SerializeField] private DiscSpawner discSpawner;
    void Start()
    {
        StartCoroutine(ActivatorDisc());
        StartCoroutine(DisableIndicator());
    }

    private IEnumerator ActivatorDisc()
    {
        yield return new WaitForSeconds(8);
        m_Cont0.SetActive(true);
        yield return new WaitForSeconds(2);
        m_Disc0.SetActive(true);
        m_Cont0.SetActive(false);

        yield return new WaitForSeconds(8);
        m_Cont1.SetActive(true);
        yield return new WaitForSeconds (2);
        m_Disc1.SetActive (true);
        m_Cont1.SetActive(false);

        yield return new WaitForSeconds(8);
        m_Cont2.SetActive(true);
        yield return new WaitForSeconds(2);
        m_Disc2.SetActive (true);
        m_Cont2.SetActive(false);

        yield return new WaitForSeconds(8);
        m_Cont3.SetActive(true);
        yield return new WaitForSeconds(2);
        m_Disc3.SetActive (true);
        m_Cont3.SetActive(false);
    }

    private IEnumerator DisableIndicator()
    {
        yield return new WaitForSeconds(discSpawner.spawnMax);
        m_ContBase.SetActive(false);
    }

}
