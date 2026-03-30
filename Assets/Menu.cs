using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool SkipToGameplay;
    [SerializeField] private string SkipToGamemode;
    [SerializeField] private CurtainTransition curtain;
    [SerializeField] private OpacityControl opacityControl;
    [SerializeField] private CameraTransfer CamTransfer;
    [SerializeField] private GameObject playbutton;
    [SerializeField] private GameObject linearbutton;
    [SerializeField] private GameObject topviewbutton;
    [SerializeField] private GameObject LinearModeScene;
    [SerializeField] private GameObject TopDownScene;
    [SerializeField] private GameObject UnitSelection;
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject ModeSelection;
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private GameObject UI;
    [SerializeField] private UnitTab unitTab;
    [SerializeField] private GameObject LinearBase;
    [SerializeField] private GameObject TopDownBase;

    private void Update()
    {
        if (SkipToGameplay)
        {
            Title.SetActive(false);
            playbutton.SetActive(false);
            curtain.OpenCurtain();
            UnitSelection.SetActive(true);
            UI.SetActive(true);
            switch (SkipToGamemode)
            {
                case "Linear":
                    LinearModeScene.SetActive(true);
                    CamTransfer.index = 0;
                    CamTransfer.rotationPivot = new Vector3(30, -38, 0);
                    CamTransfer.TransferCamera();
                    unitTab.SpawnStartingPoint = LinearBase.transform;
                    break;
                case "TopDown":
                    // TopDownScene.SetActive(true);
                    break;
                default:
                    Debug.Log("Invalid gamemode specified for SkipToGameplay.");
                    break;
            }
            SkipToGameplay = false;
        }
    }

    public void MenuButton(string Button)
    {
        switch (Button)
        {
            case "Play":
                StartCoroutine(Play());
                break;
            case "TopDownMode":
                StartCoroutine(TopDownMode());
                break;
            case "LinearMode":
                StartCoroutine(LinearMode());
                break;
        }
    }

    IEnumerator Play()
    {
        playbutton.SetActive(false);
        opacityControl.index = 0;
        opacityControl.ToOpacityB();
        yield return new WaitForSeconds(1);
        Title.SetActive(false);
        ModeSelection.SetActive(true);
        opacityControl.index = 1;
        opacityControl.ToOpacityA();
        yield return new WaitForSeconds(0.5f);
        topviewbutton.SetActive(true);
        linearbutton.SetActive(true);
    }

    IEnumerator LinearMode()
    {
        topviewbutton.SetActive(false);
        linearbutton.SetActive(false);
        opacityControl.ToOpacityB();
        yield return new WaitForSeconds(1);
        LinearModeScene.SetActive(true);
        CamTransfer.index = 0;
        CamTransfer.rotationPivot = new Vector3(30, -38, 0);
        CamTransfer.TransferCamera();
        unitTab.SpawnStartingPoint = LinearBase.transform;
        StartCoroutine(BeforeGame());
    }

    IEnumerator TopDownMode()
    {
        topviewbutton.SetActive(false);
        linearbutton.SetActive(false);
        opacityControl.ToOpacityB();
        yield return new WaitForSeconds(1);
        TopDownScene.SetActive(true);
        CamTransfer.index = 0;
        CamTransfer.TransferCamera();
        unitTab.SpawnStartingPoint = TopDownBase.transform;
        StartCoroutine(BeforeGame());
    }

    IEnumerator BeforeGame()
    {
        Tutorial.SetActive(true);
        opacityControl.index = 2;
        opacityControl.ToOpacityA();
        yield return new WaitForSeconds(2);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        opacityControl.ToOpacityB();
        yield return new WaitForSeconds(1);
        Tutorial.SetActive(false);
        curtain.OpenCurtain();
        yield return new WaitForSeconds(0.5f);
        UI.SetActive(true);
        UnitSelection.SetActive(true);
    }
}
