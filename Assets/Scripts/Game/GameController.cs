using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public const int BEGIN_SCORE = 7000;
    public const int SCORE_DECAY_PER_SECOND = 2;
    public const float PORTAL_TEXT_FLASH_TIME = 0.4f;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text remainingCrystalsText;
    [SerializeField] private Text currentCrystalsText;
    [SerializeField] private Text portalOpennedText;

    private Crystal[] crystals;
    private float currentScore = 0;
    private bool isPostalTextAnimating;

    private void Start()
    {
        currentScore = BEGIN_SCORE;
        crystals = FindObjectsOfType<Crystal>();
    }

    private void Update()
    {
        currentScore -= SCORE_DECAY_PER_SECOND * Time.deltaTime;
        scoreText.text = ((int)currentScore).ToString();

        bool isCrystalsAllEnabled = CheckCrystals();
        if (isCrystalsAllEnabled && !isPostalTextAnimating)
        {
            StartCoroutine(FlashPortalText());
        }
        else if (!isCrystalsAllEnabled)
        {
            portalOpennedText.gameObject.SetActive(false);
        }
    }

    public bool CheckCrystals()
    {
        int count = 0;
        for (int i = 0; i < crystals.Length; i++)
        {
            if (crystals[i].IsRecivingLight())
            {
                count++;
            }
        }

        currentCrystalsText.text = count + "";
        remainingCrystalsText.text = crystals.Length + "";

        return count == crystals.Length;
    }

    public IEnumerator FlashPortalText()
    {
        isPostalTextAnimating = true;
        while (CheckCrystals())
        {
            portalOpennedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(PORTAL_TEXT_FLASH_TIME * 1.5f);
            portalOpennedText.gameObject.SetActive(false);
            yield return new WaitForSeconds(PORTAL_TEXT_FLASH_TIME);
        }
        portalOpennedText.gameObject.SetActive(false);
        isPostalTextAnimating = false;
    }
}
