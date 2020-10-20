using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResults : MonoBehaviour
{
    public Text[] ScoreOfLevel1;
    public Text[] ScoreOfLevel2;
    public Text[] ScoreOfLevel3;
    public Text[] ScoreOfLevel4;

    private string[] scoreOflevel1;
    private string[] scoreOflevel2;
    private string[] scoreOflevel3;
    private string[] scoreOflevel4;
    private string zero = "5/0";
    private bool resetBtnPressed = false;

    private void Awake()
    {
        triggeringEffects.resetBtnHadHited += UpdateResetedBtn;
    }
    private void Start()
    {
        PrintScores();
    }

    private void OnDestroy()
    {
        triggeringEffects.resetBtnHadHited -= UpdateResetedBtn;
    }

    private void UpdateResetedBtn(bool resetBtnHited)
    {
        resetBtnPressed = resetBtnHited;
    }

    private void Update()
    {
        if (resetBtnPressed)
        {
            practiceForLevel12.scoreOfLevel1_1 = zero;
            practiceForLevel12.scoreOfLevel1_2 = zero;
            practiceForLevel12.scoreOfLevel2_1 = zero;
            practiceForLevel12.scoreOfLevel2_2 = zero;
            practiceForLevel3.scoreOfLevel3_1 = zero;
            practiceForLevel3.scoreOfLevel3_2 = zero;
            practiceForLevel3.scoreOfLevel4_1 = zero;
            practiceForLevel3.scoreOfLevel4_2 = zero;
        }
        PrintScores();
    }
    private void PrintScores()
    {
        // Level1
        if (practiceForLevel12.scoreOfLevel1_1 != null)
            ScoreOfLevel1[0].text = practiceForLevel12.scoreOfLevel1_1;
        else
            ScoreOfLevel1[0].text = zero;

        if (practiceForLevel12.scoreOfLevel1_2 != null)
            ScoreOfLevel1[1].text = practiceForLevel12.scoreOfLevel1_2;
        else
            ScoreOfLevel1[1].text = zero;

        // Level2
        if (practiceForLevel12.scoreOfLevel2_1 != null)
            ScoreOfLevel2[0].text = practiceForLevel12.scoreOfLevel2_1;
        else
            ScoreOfLevel2[0].text = zero;

        if (practiceForLevel12.scoreOfLevel2_2 != null)
            ScoreOfLevel2[1].text = practiceForLevel12.scoreOfLevel2_2;
        else
            ScoreOfLevel2[1].text = zero;

        // Level3
        if (practiceForLevel3.scoreOfLevel3_1 != null)
            ScoreOfLevel3[0].text = practiceForLevel3.scoreOfLevel3_1;
        else
            ScoreOfLevel3[0].text = zero;

        if (practiceForLevel3.scoreOfLevel3_2 != null)
            ScoreOfLevel3[1].text = practiceForLevel3.scoreOfLevel3_2;
        else
            ScoreOfLevel3[1].text = zero;

        // Level4
        if (practiceForLevel3.scoreOfLevel4_1 != null)
            ScoreOfLevel4[0].text = practiceForLevel3.scoreOfLevel4_1;
        else
            ScoreOfLevel4[0].text = zero;

        if (practiceForLevel3.scoreOfLevel4_2 != null)
            ScoreOfLevel4[1].text = practiceForLevel3.scoreOfLevel4_2;
        else
            ScoreOfLevel4[1].text = zero;
    }
}
