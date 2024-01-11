using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; //Кількість хмаринок, яка буде згенерована
    public GameObject cloudPrefab; //Шаблон для хмаринок
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //Мінмальний маштаб хмаринки
    public float cloudScaleMax = 3; //Мксимальний маштаб кожної хмаринки
    public float cloudSpeedMult = 0.5f; //Коефіцієнт швикдості хмаринок

    private GameObject[] cloudInstances; //Самі хмаринки 


    // Start is called before the first frame update
    void Awake()
    {
        //створюємо хмаринки
        cloudInstances = new GameObject[numClouds];
        //Розташування наших хмаринок
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i<numClouds; i++)
        {
            cloud = Instantiate(cloudPrefab);
            //Встановлюємо місце розташування
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //Маштаблування облако
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //менша хмаринка ближче до землі
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //Менша хмаринка повина бути ближче до землі
            cPos.z = 100 - 90 * scaleU;
            //Застосовуємо отримані значення координат і маштаб до хмаринок
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //Робимо хмаринку дочірнім елементом до anchor
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject cloud in cloudInstances)
        {
            //Отримуємо маштаб і координати хмаринок
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Збільшуємо швидкість до найбижчої хмаринки - переміщаємо в ліво
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            if(cPos.x <= cloudPosMin.x)
            {
                //перемідаємо його в право, якщо воно далеко зайшло в ліво
                cPos.x = cloudPosMax.x;
            }
            //Встановлюємо позицію
            cloud.transform.position = cPos;

        }
    }
}
