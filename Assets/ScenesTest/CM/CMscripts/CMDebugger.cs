using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CMDebugger : MonoBehaviour
{
    [SerializeField]
    Text debugText, debugText1, debugText2, debugText3, debugText4, debugText5;

    [SerializeField]
    Slider sliderDragOneLive, sliderDragTwoLives, sliderAng, sliderForceY, sliderForceYOneLive, sliderForceYTwoLives, sliderForceX;

    [SerializeField]
    GameObject playerStandarRB, playerFixedRB, playerTransform;

    [SerializeField]
    GameObject panelTools;

    [SerializeField]
    bool toolsActive = false;

    [SerializeField]
    Text livestxt;

    public Slider SliderDragOneLive { get => sliderDragOneLive; set => sliderDragOneLive = value; }
    public Slider SliderDragTwoLives { get => sliderDragTwoLives; set => sliderDragTwoLives = value; }
    public Slider SliderForceYOneLive { get => sliderForceYOneLive; set => sliderForceYOneLive = value; }
    public Slider SliderForceYTwoLives { get => sliderForceYTwoLives; set => sliderForceYTwoLives = value; }
    public Slider SliderForceX { get => sliderForceX; set => sliderForceX = value; }
    public Text DebugText1 { get => debugText1; set => debugText1 = value; }
    public Text DebugText2 { get => debugText2; set => debugText2 = value; }
    public Text DebugText3 { get => debugText3; set => debugText3 = value; }
    public Text DebugText4 { get => debugText4; set => debugText4 = value; }
    public Text DebugText5 { get => debugText5; set => debugText5 = value; }

    private void Update()
    {
        panelTools.SetActive(toolsActive);
    }

    public void ActiveTools()
    {
        if (toolsActive)
        {
            toolsActive = false;
        }
        else
        {
            toolsActive = true;
        }
    }

    public void StandarRB()
    {
        DiasableAllModes();
        playerStandarRB.SetActive(true);
    }
    public void FixedRB()
    {
        DiasableAllModes();
        playerFixedRB.SetActive(true);
    }
    public void TransformRB()
    {
        DiasableAllModes();
        playerTransform.SetActive(true);
    }

    private void DiasableAllModes()
    {
        playerStandarRB.SetActive(false);
        playerFixedRB.SetActive(false);
        playerTransform.SetActive(false);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("Level CompleteWithBonus", LoadSceneMode.Single);
    }

    public void SetOneLive()
    {
        TouchMovement tm = GameObject.FindObjectOfType<TouchMovement>();
        tm.Lives = 1;
        livestxt.text = "Lives: " + 1;
    }

    public void SetTwoLives()
    {
        TouchMovement tm = GameObject.FindObjectOfType<TouchMovement>();
        tm.Lives = 2;
        livestxt.text = "Lives: " + 2;
    }
}
