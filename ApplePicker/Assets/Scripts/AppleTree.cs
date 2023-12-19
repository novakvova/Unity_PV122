using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject applePrefab; //Prefab - ������
    public float speed = 1f; //�������� ����������
    public float leftAndRightEdge = 10f; //��� ���������� ������
    public float chanceBetweenAppleDrops = 0.1f; //���������� ���� �������� ����
    public float secondsBetweenAppleDrops = 1f;//������� �������� �����
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
