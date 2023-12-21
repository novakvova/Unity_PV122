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
    public float chanceToChangeDirection = 0.1f; //≤мов≥рн≥сть зм≥ни напр€мку руху
    public float secondsBetweenAppleDrops = 1f;//„астота скиданн€ €блук
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropApple", 2f);
    }
    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = this.transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); //«м≥нчЇмо напр€мок руху в право
        }
        else if(pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); //зм≥нЇмо швидк≥сть на в≥дЇмну в л≥во
        }
    }
    private void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirection)
        {
            speed *= -1;
        }
    }
}
