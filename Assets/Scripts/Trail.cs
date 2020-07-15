using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private GameObject trail;

    private void Start()
    {
        StartCoroutine("ShowTrail");
    }

    IEnumerator ShowTrail()
    {
        yield return new WaitForSeconds(0.05f);
        Instantiate(trail, transform.position, Quaternion.identity);
        StartCoroutine("ShowTrail");

    }
}
