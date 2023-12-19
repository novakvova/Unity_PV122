using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject applePrefab; //Prefab - €блука
    public float speed = 1f; //Ўвидк≥сть перем≥щенн€
    public float leftAndRightEdge = 10f; //меж≥ перем≥щенн€ €блун≥
    public float chanceBetweenAppleDrops = 0.1f; //≤мов≥рн≥сть зм≥ни напр€мку руху
    public float secondsBetweenAppleDrops = 1f;//„астота скиданн€ €блук
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
    }
}
