using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;
    [Header("Set in Inpector")]
    public GameObject prefabProjectile;
    //Сила натяжки рогатки
    public float velocityMult = 8;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if(S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    private void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    
    private void OnMouseEnter()
    {
        //print("Навели мишку на обєкт");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        //print("Користувач забрав мишку");
        launchPoint.SetActive(false);
    }

    public void OnMouseDown()
    {
        //Включили режим анімації
        aimingMode = true;
        //Отримуємо сам об'єкт
        projectile = Instantiate(prefabProjectile);
        //Утанавлюємо позицію у центрі рогатки
        projectile.transform.position = launchPos;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        //Обєкт робимо кінематичним
        projectileRigidbody.isKinematic = true;
    }
    private void Update()
    {
        if(!aimingMode)
        {
            return;
        }
        //Отримуємо позицію мишки
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Пошук координат
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        //Не можемо виходити за меже радіусу Hallo
        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Переміщаємо снаряд на позицію
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if(Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }

    }
}
