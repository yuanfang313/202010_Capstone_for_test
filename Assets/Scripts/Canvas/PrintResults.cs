using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResults : MonoBehaviour
{
    public toggleCanvas canvas;
    public Text[] ScoreOfLevel1;
    public Text[] ScoreOfLevel2;

    private string zero = "5/0";
    private bool resetBtnPressed = false;
    
    private void Awake()
    {
        triggeringEffects.resetBtnHadHited += UpdateResetedBtn;
    }
    private void Start()
    {
        cleanScores();
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
        if (canvas.canvasHadOpened)
        {
            if (resetBtnPressed)
            {
                PlayerPrefs.DeleteAll();
            }
         cleanScores();
        }
        PrintScores();
    }
    private void PrintScores()
    {
        int thisScene = PlayerPrefs.GetInt("thisScene");

        if (!PlayerPrefs.HasKey("thisScene"))
        {
            return;
        }

        if(thisScene <= 6)
        {
            GetScores(ScoreOfLevel1, "scoreOfLevel1-");
        }
        else if(thisScene > 6)
        {
            GetScores(ScoreOfLevel2, "scoreOfLevel2-");
        }
                
        void GetScores(Text[] ScoreOfLevel, string scoreOfLevel)
        {
            int scene = thisScene;
            if (thisScene > 6)
            {
                scene = thisScene - 6;
            }

            string levelName_0 = scoreOfLevel + scene.ToString() + "_0";
            string levelName_1 = scoreOfLevel + scene.ToString() + "_1";

            string score_0 = PlayerPrefs.GetString(levelName_0);
            string score_1 = PlayerPrefs.GetString(levelName_1);

            ScoreOfLevel[scene * 2 - 2].text = score_0;
            ScoreOfLevel[scene * 2 - 1].text = score_1;       
        }                          
    }

    private void cleanScores()
    {
        clean(ScoreOfLevel1, 0);
        clean(ScoreOfLevel1, 1);
        clean(ScoreOfLevel1, 2);
        clean(ScoreOfLevel1, 3);
        clean(ScoreOfLevel1, 4);
        clean(ScoreOfLevel1, 5);
        clean(ScoreOfLevel1, 6);
        clean(ScoreOfLevel1, 7);
        clean(ScoreOfLevel1, 8);
        clean(ScoreOfLevel1, 9);
        clean(ScoreOfLevel1, 10);
        clean(ScoreOfLevel1, 11);

        clean(ScoreOfLevel2, 0);
        clean(ScoreOfLevel2, 1);
        clean(ScoreOfLevel2, 2);
        clean(ScoreOfLevel2, 3);
        clean(ScoreOfLevel2, 4);
        clean(ScoreOfLevel2, 5);

        void clean( Text[] scoreOfLevel, int i )
        {
            if (!resetBtnPressed)
            {
                if (scoreOfLevel == ScoreOfLevel1)
                {
                    keepScores(ScoreOfLevel1, "scoreOfLevel1-");
                }
                else if (scoreOfLevel == ScoreOfLevel2)
                {
                    keepScores(ScoreOfLevel2, "scoreOfLevel2-");
                }
            } else
            {
                scoreOfLevel[i].text = zero;
            }

            void keepScores(Text[] scoreOflevel, string levelStr)
            {
                string index = "_0";
                if (i % 2 != 0)
                {
                    index = "_1";
                }
                else
                {
                    index = "_0";
                }
                string levelName = levelStr + (Mathf.Floor(i / 2) + 1).ToString() + index;
                if (PlayerPrefs.HasKey(levelName))
                    scoreOflevel[i].text = PlayerPrefs.GetString(levelName);
                else
                    scoreOfLevel[i].text = zero;

            }
        }
    }      
}
