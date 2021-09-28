using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UILevelSelector : Singleton<UILevelSelector>
{
    [SerializeField] GameObject panelLevelSelector;
    [SerializeField] GameObject containerLevelSelector;

    [SerializeField] GameObject imageFramePrefab;
    [SerializeField] GameObject containerPrefab;
    [SerializeField] Sprite defaultImagesBackGround;

    [SerializeField] int levelToLoad;

    protected override void Awake()
    {
        //asi no se borran los serializados
    }

    private void OnEnable()
    {
        ClearPrefabs();
        InstantiateImageFramePrefabs();
    }

    private void ClearPrefabs()
    {
        Destroy(containerLevelSelector);
        GameObject newContainer = Instantiate(containerPrefab, panelLevelSelector.transform);
        containerLevelSelector = newContainer;
    }

    private void InstantiateImageFramePrefabs()
    {
        for (int i = 0; i < GameManager.Instance.LastLevel; i++)
        {
            GameObject imageFrameLevel = Instantiate(imageFramePrefab, containerLevelSelector.transform);

            //Texture2D myTexture = LoadPNG(Application.persistentDataPath + "/pic.png");
            
            Texture2D myTexture = LoadPNG(Application.persistentDataPath + "/pic" + (i + 1) + ".png");
            if (myTexture != null)
            {
                Sprite sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                imageFrameLevel.GetComponent<Image>().sprite = sprite;
            }
            else
            {
                imageFrameLevel.GetComponent<Image>().sprite = defaultImagesBackGround;
            }
            


            imageFrameLevel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();

            //imageFrameLevel.GetComponentInChildren<Button>().onClick.AddListener(TaskOnClick);
            int level = i + 1;
            imageFrameLevel.GetComponentInChildren<Button>().onClick.AddListener(delegate { TaskOnClick(level); });
        }
    }

    void TaskOnClick(int level)
    {
        //Debug.Log("Frame Level: " + level);
        GuardadoSerializado.Instance.CargarLevel(level);
        //GuardadoSerializado.Instance.CargarLevel2();
    }

    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
