using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CharacterSkinSel : MonoBehaviour
{

    public GameObject[] skins;
    LevelManager manager;
    public GameObject spawn;


    // Start is called before the first frame update
    void Start()
    {
        //manager = GameObject.Find("GameManager").GetComponent<Manager>();
        //spawn = manager.SpawnPoint;

        BinaryFormatter binFor = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/data.shop", FileMode.Open);
        Data dataLeido = (Data)binFor.Deserialize(file);

        if(dataLeido.characterskin == 1)
        {
            // Instanciar la skin 1
            GameObject player = Instantiate(skins[0], spawn.transform.position, Quaternion.identity) as GameObject;
            file.Close();
        }

        if(dataLeido.characterskin == 2)
        {
            // Instanciar la skin 2
            GameObject player = Instantiate(skins[1], spawn.transform.position, Quaternion.identity) as GameObject;
            file.Close();
        }

        if (dataLeido.characterskin == 3)
        {
            // Instanciar la skin 3
            GameObject player = Instantiate(skins[2], spawn.transform.position, Quaternion.identity) as GameObject;
            file.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
