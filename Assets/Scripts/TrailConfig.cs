using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailConfig : MonoBehaviour
{
    private Color changeColor;
    private SpriteRenderer color;
    private Vector3 changeScale;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GameObject.Find("Player").GetComponent<SpriteRenderer>().color;
        color = gameObject.GetComponent<SpriteRenderer>();
        changeColor = new Color(0, 0, 0, 0.05f);
        changeScale = new Vector3(0.02f, 0.02f, 0);
        StartCoroutine("Death");
    }

    private void FixedUpdate()
    {
        color.color -= changeColor;
        transform.localScale -= changeScale;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
