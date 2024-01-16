using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; //посилання на об'єкт одиночка

    [Header("Set in Inspector")]
    public Text uitLevel; //Посилання на обєкт UIText_Level
    public Text uitShots; //Посилання на обєкт UIText_Shots
    public Text uitButton; //Посилання на дочіній об'єкт Text в UIButton_View
    public Vector3 castlePos; //Місце знаходження замка
    public GameObject[] castles; //Масив замків

    [Header("Set Dynamically")]
    public int level; //Поточний рівень
    public int levelMax; //Кількість рівнів
    public int shotsTaken;

    public GameObject castle; //Поточний замок
    public GameMode mode = GameMode.idle; //Режим очікування
    public string showing = "Show Slingshot"; //Режим FollowCam


    // Start is called before the first frame update
    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if(castle != null)
        {
            Destroy(castle);
        }

        //На початку гри видаляємо усі снаряди, які є в грі
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject go in gos)
        {
            Destroy(go);
        }

        //Створюємо новий замок
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //Ствмико камеру в початову позицію
        SwitchView("Show Both");
        ProjectileLine.S.Clear(); //стираємо лінію

        //Скинуть ціль
        Goal.goalMet = false;
        UpdateGUI();

        mode = GameMode.playing; //Розпочато групу
    }

    void UpdateGUI()
    {
        //показуємо інфомацію елемента
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken; //Кількість зроблених пострілів
    }

    public void SwitchView(string eView = "")
    {
        if(eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch(showing)
        {
            case "Show Slingshot":
                {
                    FollowCam.POI = null;
                    uitButton.text = "Show Castle";
                    break;
                }
            case "Show Castle":
                {
                    FollowCam.POI = S.castle;
                    uitButton.text = "Show Both";
                    break;
                }
            case "Show Both":
                {
                    FollowCam.POI = GameObject.Find("ViewBoth");
                    uitButton.text = "Show Slingshot";
                    break;
                }
        }
    }
    public static void ShotFired()
    {
        S.shotsTaken++;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        //Перевіряємо завершення рівня
        if((mode == GameMode.playing) && Goal.goalMet) //Якщо втрапили в зону враження
        {
            //Зміна режиму, 
            mode = GameMode.levelEnd;
            //Зменшуємо маштаб
            SwitchView("Show Both");
            //Починаємо новий рівень після 2 секунд
            Invoke("NextLevel", 2f);
        }
    }
    void NextLevel()
    {
        level++;
        //Доходимо до кінця і тоді починаємо з початку
        if(level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }
}
