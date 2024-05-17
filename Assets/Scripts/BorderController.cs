using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private GameObject disc_01;
    [SerializeField] private GameObject disc_02;
    [SerializeField] private GameObject pan1;
    [SerializeField] private GameObject pan2;

    private void Start()
    {
        StartCoroutine(BorderActivation());
    }

    private IEnumerator BorderActivation()
    {
        yield return new WaitForSeconds(40f);
        pan1.SetActive(true);
        pan2.SetActive(true);

        yield return new WaitForSeconds(2f);
        pan1.SetActive(false);
        pan2.SetActive(false);
        disc_01.SetActive(true);
        disc_02.SetActive(true);
    }
}
