using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; //ʳ������ ��������, ��� ���� �����������
    public GameObject cloudPrefab; //������ ��� ��������
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //̳�������� ������ ��������
    public float cloudScaleMax = 3; //����������� ������ ����� ��������
    public float cloudSpeedMult = 0.5f; //���������� �������� ��������

    private GameObject[] cloudInstances; //��� �������� 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
