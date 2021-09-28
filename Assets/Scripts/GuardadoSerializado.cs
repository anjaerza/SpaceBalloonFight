using System.Net.Mime;
using System.Data;
using System.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuardadoSerializado : Singleton<GuardadoSerializado>
{
    bool isFirstTime = true;

    public bool IsFirstTime { get => isFirstTime; set => isFirstTime = value; }

    protected override void Awake()
    {
        if (SceneManager.GetActiveScene().name == "CMProvitionalMenu")
        {

            GameManager.Instance.PleaseAwake();
            CargarLastLevel();

        }
    }

    private void Start()
    {
        
    }

    public void Guardar(int curLevel, float posx, float posy, float posz)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/level" + curLevel + ".save");
        LevelData levelData = new LevelData
        {
            myCurrentLevel = curLevel,
            myXPos = posx,
            myYPos = posy,
            myZPos = posz
        };

        bf.Serialize(fs, levelData);
        fs.Close();

    }

    public void GuardarLastLevel(int curLevel)
    {
        if (curLevel > GameManager.Instance.LastLevel)
        {
            print("Save Last Level");

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/lastLevel.save");
            LevelData levelData = new LevelData
            {
                myCurrentLevel = curLevel,
                myXPos = 0,
                myYPos = 0,
                myZPos = 0
            };

            bf.Serialize(fs, levelData);
            fs.Close();
        }
    }

    public void CargarLevel(int level)
    {
        if (File.Exists(Application.persistentDataPath + "/level" + level + ".save") && level > 1)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/level" + level + ".save");
            LevelData myLevelData = new LevelData();

            myLevelData = bf.Deserialize(fs) as LevelData;

            GameManager.Instance.CurrentLevelGameManager = myLevelData.myCurrentLevel;
            GameManager.Instance.PosInicial = new Vector3(myLevelData.myXPos, myLevelData.myYPos, myLevelData.myZPos);

            //print(GameManager.Instance.PosInicial);

            //if (level == 1)
            //{
            //    GameManager.Instance.CurrentLevelGameManager = 1;
            //    GameManager.Instance.PosInicial = new Vector3(0, -12.22f, -0.52f);

            //    SceneManager.LoadScene("CMCompleteWorld 2", LoadSceneMode.Single);
            //}
            //else
            //{
            //    SceneManager.LoadScene("CMCompleteWorld 2", LoadSceneMode.Single);
            //}

            SceneManager.LoadScene("CMCompleteWorld 2", LoadSceneMode.Single);
        }
        else
        {
            print("No se encontro archivo");

            GameManager.Instance.CurrentLevelGameManager = 1;
            GameManager.Instance.PosInicial = new Vector3(0, -12.22f, -0.52f);

            SceneManager.LoadScene("CMCompleteWorld 2", LoadSceneMode.Single);
        }
    }

    public void CargarLastLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/lastLevel.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/lastLevel.save");
            LevelData myLevelData = new LevelData();

            myLevelData = bf.Deserialize(fs) as LevelData;

            GameManager.Instance.LastLevel = myLevelData.myCurrentLevel;

            //SceneManager.LoadScene("CMCompleteWorld 1", LoadSceneMode.Additive);
        }
        else
        {
            print("No se encontro archivo");

            //GameManager.Instance.CurrentLevelGameManager = 1;
            //GameManager.Instance.PosInicial = new Vector3(0, -12.22f, -0.52f);

            //SceneManager.LoadScene("CMCompleteWorld 1", LoadSceneMode.Additive);
        }
    }

    public void CargarLevel2()
    {

        //if (File.Exists(Application.persistentDataPath + "/lastLevel.save"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream fs = File.OpenRead(Application.persistentDataPath + "/lastLevel.save");
        //    LevelData myLevelData = new LevelData();

        //    myLevelData = bf.Deserialize(fs) as LevelData;

        //    GameManager.Instance.LastLevel = myLevelData.myCurrentLevel;

        //    //SceneManager.LoadScene("CMCompleteWorld 1", LoadSceneMode.Single);
        //}
        //else
        //{
        //    print("No se encontro archivo");

        //    //GameManager.Instance.CurrentLevelGameManager = 1;
        //    //GameManager.Instance.PosInicial = new Vector3(0, -12.22f, -0.52f);

        //    //SceneManager.LoadScene("CMCompleteWorld 1", LoadSceneMode.Additive);
        //}



        SceneManager.LoadScene("CMCompleteWorld 1", LoadSceneMode.Single);
    }
}

[Serializable]
public class LevelData
{
    public int myCurrentLevel = 0;

    public float myXPos = 0;
    public float myYPos = 0;
    public float myZPos = -0.52f;
}