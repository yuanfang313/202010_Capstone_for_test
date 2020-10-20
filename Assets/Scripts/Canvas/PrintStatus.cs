using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrintStatus : MonoBehaviour
{
    public practiceForLevel12 p_Level12;
    public practiceForLevel3 p_Level3;

    public Text voicePCounter_t;
    public Text visualPCounter_t;
    public Text roundCounter_t;
    public Text visualPTimer_f;
    public Text visualPTimer_d;
    public Text boolOfTesting_t;
    public Text boolOfTeaching_t;
    public Text testingScores1_t;
    public Text testingScores2_t;
    public Text teachingScore_t;

    private bool Level12HadLoaded = false;
    private bool Level3HadLoaded = false;
    private bool Level4HadLoaded = false;

    private void Awake()
    {
        practiceForLevel12.Level12HadLoaded += UpdateLevel12Status;
        practiceForLevel3.Level34HadLoaded += UpdateLevel34Status;

    }
    private void Start()
    {
        PrintVoicePrompts();
        PrintVisualPrompts();
        PrintRounds();
        PrintVisualPrompts_f();
        PrintVisualPrompts_d();
        PrintScores();
    }
    private void OnDestroy()
    {
        practiceForLevel12.Level12HadLoaded -= UpdateLevel12Status;
        practiceForLevel3.Level34HadLoaded -= UpdateLevel34Status;
    }

    private void UpdateLevel12Status(bool level12HadLoaded)
    {
        Level12HadLoaded = level12HadLoaded;
    }

    private void UpdateLevel34Status(bool level3HadLoaded, bool level4HadLoaded)
    {
        Level3HadLoaded = level3HadLoaded;
        Level4HadLoaded = level4HadLoaded;
    }

    /* printing data */
    public void PrintVoicePrompts()
    {
        if(Level3HadLoaded || Level4HadLoaded)
            voicePCounter_t.text = p_Level3.voicePromptCounter.ToString();
        else if (Level12HadLoaded)
            voicePCounter_t.text = p_Level12.voicePromptCounter.ToString();
    }

    public void PrintVisualPrompts()
    {
        if (Level3HadLoaded || Level4HadLoaded)
            visualPCounter_t.text = p_Level3.visualPromptCounter.ToString();
        else if (Level12HadLoaded)
            visualPCounter_t.text = p_Level12.visualPromptCounter.ToString();
    }

    public void PrintRounds()
    {
        if(Level3HadLoaded || Level4HadLoaded)
            roundCounter_t.text = p_Level3.roundCount.ToString();
        else if (Level12HadLoaded)
            roundCounter_t.text = p_Level12.roundCount.ToString();
    }

    public void PrintVisualPrompts_f()
    {
        //int_visualPTempTimer_f = (int)visualPTempTimer_f;
        if(Level3HadLoaded || Level4HadLoaded)
            visualPTimer_f.text = p_Level3._visualPTempTimer_f.ToString("F1");
        else if (Level12HadLoaded)
            visualPTimer_f.text = p_Level12._visualPTempTimer_f.ToString("F1");
    }

    public void PrintVisualPrompts_d()
    {
        //int_visualPTempTimer_d = (int)visualPTempTimer_d;
        if(Level3HadLoaded || Level4HadLoaded)
            visualPTimer_d.text = p_Level3._visualPTempTimer_d.ToString("F1");
        else if (Level12HadLoaded)
            visualPTimer_d.text = p_Level12._visualPTempTimer_d.ToString("F1");
    }

    public void PrintScores()
    {
        if (Level3HadLoaded || Level4HadLoaded)
        {
            teachingScore_t.text = p_Level3.teachingScores.ToString();
            testingScores1_t.text = p_Level3.testingScores1.ToString();
            testingScores2_t.text = p_Level3.testingScores2.ToString();
        }
        else if (Level12HadLoaded)
        {
            teachingScore_t.text = p_Level12.teachingScores.ToString();
            testingScores1_t.text = p_Level12.testingScores1.ToString();
            testingScores2_t.text = p_Level12.testingScores2.ToString();
        }

    }
}
