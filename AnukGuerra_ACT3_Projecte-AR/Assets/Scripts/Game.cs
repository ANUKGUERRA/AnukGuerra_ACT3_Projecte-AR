using DG.Tweening;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

//TODO: Agregar seleccion de personaje
//TODO: Ocultar UI al començar partida
//TODO: easing de la barra de vida

public class Game : MonoBehaviour
{
    [SerializeField] private GameStart m_EventChannel;
    [SerializeField] Canvas canvas;
    [SerializeField] Button placeButton;
    [SerializeField] Button playButton;
    [SerializeField] GameObject cubePreview;
    [SerializeField] GameObject realCube;
    [SerializeField] GameObject hexGrid;
    [SerializeField] Material blueMat;
    [SerializeField] Material redMat;
    [SerializeField] GameObject sphereVolume;
    [SerializeField] Material ditherMat;

    public static bool gamePlaying;
    private team unitTeam;

    public static team Winner;

    private void Awake()
    {
        playButton.interactable = false;
        placeButton.interactable = false;
        placeButton.onClick.AddListener(addUnit);
        playButton.onClick.AddListener(playGame);
    }

    private void Update()
    {
        if (UnitsManager.Instance.units[team.red].Count > 0 &&
            UnitsManager.Instance.units[team.red].Count == UnitsManager.Instance.units[team.blue].Count)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
        if (cubePreview.activeSelf && hexGrid.activeSelf && detectPosition.DetectedHex)
        {
            if (detectPosition.hit.transform.gameObject.tag == "Mid")
            {
                //Debug.Log("MID");
                placeButton.interactable = false;
            }
            if(detectPosition.hit.transform.gameObject.tag == "Red")
            {
                //Debug.Log("Red");
                unitTeam = team.red;
                placeButton.interactable = true;
            }
            if (detectPosition.hit.transform.gameObject.tag == "Blue")
            {
                //Debug.Log("Blue");
                unitTeam = team.blue;
                placeButton.interactable = true;
            }
        }
        else
        {
            placeButton.interactable = false;
        }
    }


    void addUnit()
    {
        Quaternion rot = hexGrid.transform.rotation;
        GameObject instance = Instantiate(realCube, detectPosition.hit.point, (unitTeam == team.red) ? rot : rot *= Quaternion.Euler(0f, 180f, 0f));

        instance.transform.SetParent(detectPosition.hit.transform, true);
        

        UnitsManager.Instance.units[unitTeam].Add(instance);
        //instance.GetComponentInChildren<SkinnedMeshRenderer>().material = (unitTeam == team.red) ? redMat : blueMat;

        UnitBehaviour behaviour = instance.GetComponent<UnitBehaviour>();
        behaviour.unitTeam = unitTeam;
    }

    public void playGame()
    {

        float value = ditherMat.GetFloat("_StepThreshold");

        ditherMat
        .DOFloat(.6f, "_StepThreshold", 5f)
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            gamePlaying = true;
            m_EventChannel.SendEventMessage();
            sphereVolume.SetActive(false);
            canvas.gameObject.SetActive(false);
            ditherMat.SetFloat("_StepThreshold", value);
        });
    }

}
