using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Tienda : MonoBehaviour
{

    public static Tienda tienda;
    LevelManager manager;
    private int actuallvl;

    void Awake()
    {
        if (tienda == null)
        {
            DontDestroyOnLoad(gameObject);
            tienda = this;
        }
        else if (tienda != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.shop");
        Data data = new Data();

        data.level = 1;
        data.characterskin = 1;
        data.money = 0;

        binFor.Serialize(file, data);
        file.Close();
        Debug.Log("Datos guardados");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.shop"))
        {
            BinaryFormatter binFor = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/data.shop", FileMode.Open);
            Data dataLeido = (Data)binFor.Deserialize(file);
            file.Close();
        }
    }

    public void Skinbeach()
    {
        BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.shop");
        Data data = new Data();

        data.characterskin = 1;

        binFor.Serialize(file, data);
        file.Close();
        Debug.Log("Character Skin 1");
        /*BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/data.shop", FileMode.Open);
        Data dataLeido = (Data)binFor.Deserialize(file);

        dataLeido.characterskin = 1;
        binFor.Serialize(file, dataLeido);
        file.Close();
        Debug.Log("Character Skin 1");*/

    }

    public void SkinFire()
    {
        if (actuallvl >= 5)
        {
            BinaryFormatter binFor = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/data.shop");
            Data data = new Data();

            data.characterskin = 2;

            binFor.Serialize(file, data);
            file.Close();
            Debug.Log("Character Skin 2");
        }
        else
        {
            Debug.Log("No es posible cambiar");
        }
        /*BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.shop");
        Data data = new Data();

        data.characterskin = 2;

        binFor.Serialize(file, data);
        file.Close();
        Debug.Log("Character Skin 2");*/

        /*BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/data.shop", FileMode.Open);
        Data dataLeido = (Data)binFor.Deserialize(file);

        dataLeido.characterskin = 2;
        binFor.Serialize(file, dataLeido);
        file.Close();
        Debug.Log("Character Skin 2");*/
    }

    public void Skinspace()
    {
        if(actuallvl >= 10)
        {
            BinaryFormatter binFor = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/data.shop");
            Data data = new Data();

            data.characterskin = 3;

            binFor.Serialize(file, data);
            file.Close();
            Debug.Log("Character Skin 3");
        }
        else
        {
            Debug.Log("No es posible cambiar");
        }
        /*BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.shop");
        Data data = new Data();

        data.characterskin = 3;

        binFor.Serialize(file, data);
        file.Close();
        Debug.Log("Character Skin 3");*/

        /*BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/data.shop", FileMode.Open);
        Data dataLeido = (Data)binFor.Deserialize(file);

        dataLeido.characterskin = 3;
        binFor.Serialize(file, dataLeido);
        file.Close();
        Debug.Log("Character Skin 3");*/
    }

    // Solo de prueba, recordar borrar despues.
    
    public void Scenechange()
    {
        SceneManager.LoadScene("Testspawn");
        Debug.Log("Cambiar escena");
    }


    // Start is called before the first frame update
    void Start()
    {
        Save();
        actuallvl = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(actuallvl < manager.CurrentLevel)
        {
            actuallvl = manager.CurrentLevel;
        }
    }
}
[Serializable]
class Data
{
    public int money = 0;
    public int level = 1;
    public int characterskin = 1;
}
