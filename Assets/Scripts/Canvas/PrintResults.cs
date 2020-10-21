using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResults : MonoBehaviour
{
    public Text[] ScoreOfLevel1_1;
    public Text[] ScoreOfLevel1_2;
    public Text[] ScoreOfLevel1_3;
    public Text[] ScoreOfLevel1_4;
    public Text[] ScoreOfLevel1_5;
    public Text[] ScoreOfLevel1_6;

    private string zero = "5/0";
    private bool resetBtnPressed = false;

    private void Awake()
    {
        triggeringEffects.resetBtnHadHited += UpdateResetedBtn;
    }
    private void Start()
    {

        ScoreOfLevel1_1[0].text = zero;
        ScoreOfLevel1_2[0].text = zero;
        ScoreOfLevel1_3[0].text = zero;
        ScoreOfLevel1_4[0].text = zero;
        ScoreOfLevel1_5[0].text = zero;
        ScoreOfLevel1_6[0].text = zero;

        ScoreOfLevel1_1[1].text = zero;
        ScoreOfLevel1_2[1].text = zero;
        ScoreOfLevel1_3[1].text = zero;
        ScoreOfLevel1_4[1].text = zero;
        ScoreOfLevel1_5[1].text = zero;
        ScoreOfLevel1_6[1].text = zero;
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
        PrintScores();
        if (resetBtnPressed)
        {
            for (int i = 0; i < 6; i++)
            {
                practiceForLevel12.scoreOfLevel1[i, 0] = zero;
                practiceForLevel12.scoreOfLevel1[i, 1] = zero;
            }
        }  
    }

    private void PrintScores()
    {
        if (practiceForLevel12.scoreOfLevel1[0, 0] != null)
            ScoreOfLevel1_1[0].text = practiceForLevel12.scoreOfLevel1[0, 0];
        else
            ScoreOfLevel1_1[0].text = zero;

        if (practiceForLevel12.scoreOfLevel1[0, 1] != null)
            ScoreOfLevel1_1[1].text = practiceForLevel12.scoreOfLevel1[0, 1];
        else
            ScoreOfLevel1_1[1].text = zero;

    }


    private void StoreScores(int i)
    {
        if (practiceForLevel12.scoreOfLevel1[i, 0] != null)
            ScoreOfLevel1_1[0].text = practiceForLevel12.scoreOfLevel1[i, 0];
        else
            ScoreOfLevel1_1[0].text = zero;

        if (practiceForLevel12.scoreOfLevel1[i, 1] != null)
            ScoreOfLevel1_1[1].text = practiceForLevel12.scoreOfLevel1[i, 1];
        else
            ScoreOfLevel1_1[1].text = zero;
    }
}
      

     

