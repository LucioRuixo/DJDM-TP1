using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public float interval = 1.2f;
    float timeInTutorial = 0;
    int currentStage = 0;

    public Texture2D[] tutorialImages;
    public Texture2D tutorialFinishedImage;
    Renderer textureRenderer;

    public TutorialController tutorialController;

    void Awake()
    {
        textureRenderer = GetComponent<Renderer>();
        textureRenderer.material.mainTexture = tutorialImages[currentStage];
    }

    void Update()
    {
        switch (tutorialController.EstAct)
        {
            case TutorialController.Estados.Tutorial:
                timeInTutorial += Time.deltaTime;
                if (timeInTutorial >= interval)
                {
                    timeInTutorial = 0;

                    if (currentStage + 1 < tutorialImages.Length) currentStage++;
                    else currentStage = 0;

                    textureRenderer.material.mainTexture = tutorialImages[currentStage];
                }
                break;
            case TutorialController.Estados.Finalizado:
                textureRenderer.material.mainTexture = tutorialFinishedImage;
                break;
        }
    }
}