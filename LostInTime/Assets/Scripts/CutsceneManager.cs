using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public bool activateUponLoadingScene = false;
    public string cutsceneKeyId;
    public List<string> dialogues;
    public List<Transform> cameraTransforms;
    public GameObject cutsceneCanvas;
    public TextMeshProUGUI narrationText;
    public Image spacebarAlert;

    private CinemachineFreeLook camController;
    private Camera cam;
    private bool ableToProgressText;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        camController = FindObjectOfType<CinemachineFreeLook>();
        cam = Camera.main;
        cutsceneCanvas.SetActive(false);

        if (GameManager.instance.GetFlag(cutsceneKeyId))
        {
            gameObject.SetActive(false);
            return;
        }

        if (activateUponLoadingScene)
        {
            StartCutscene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inCutscene && ableToProgressText)
        {
            // If able to progress text, flash the spacebar alert to let the player know they can press space to progress text
            spacebarAlert.enabled = ((int)elapsedTime) % 2 == 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.inCutscene && ableToProgressText)
        {
            if (dialogues.Count == 0 || cameraTransforms.Count == 0)
            {
                EndCutscene();
            }
            else
            {
                ProgressCutscene();
            }
        }
        elapsedTime += Time.deltaTime;
    }

    private void ProgressCutscene()
    {
        narrationText.text = dialogues[0];
        dialogues.RemoveAt(0);
        Transform nextCamTransform = cameraTransforms[0];
        cameraTransforms.RemoveAt(0);
        cam.transform.position = nextCamTransform.position;
        cam.transform.rotation = nextCamTransform.rotation;
        ableToProgressText = false;
        spacebarAlert.enabled = false;
        Invoke("MakeTextProgressable", 1.5f);
    }

    public void StartCutscene()
    {
        camController.enabled = false;
        cutsceneCanvas.SetActive(true);
        GameManager.instance.inCutscene = true;
        ProgressCutscene();
    }

    private void EndCutscene()
    {
        cutsceneCanvas.SetActive(false);
        GameManager.instance.inCutscene = false;
        camController.enabled = true;
        GameManager.instance.SetFlag(cutsceneKeyId);
    }

    private void MakeTextProgressable()
    {
        ableToProgressText = true;
    }
}
