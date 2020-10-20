using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class triggeringEffects : MonoBehaviour
{
    public AudioClip triggeringClip_1, triggeringClip_2;

    public static UnityAction<bool> resetBtnHadHited;
    public static AudioSource triggeringSound;
    public static bool startTrigger = false;
    public static bool rightAnswerHitted = false;
    public static bool wrongAnswerHitted = false;


    private string Name = null;
    private string Tag = null;
    private string rightAnswer;
    private string distractor;
   
    private GameObject currentHittedObject = null;

    private bool Level3HadLoaded = false;
    private bool Level4HadLoaded = false;
    private bool soundHadPlay = false;
    private bool AnswerIstarget1 = false;
    private bool AnswerIstarget2 = false;
    private bool ResetBtnHited = false;


    private void Awake()
    {
        ControllerStatus.TriggerDown += checkTrigger;
        PointerStatus.OnPointerUpdateForObject += updateHittedObject;
        practiceForLevel3.Level34HadLoaded += UpdateLevel34Status;
        practiceForLevel3.answerIs += UpdateAnswer;
    }

    private void Start()
    {
        triggeringSound = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        ControllerStatus.TriggerDown -= checkTrigger;
        PointerStatus.OnPointerUpdateForObject -= updateHittedObject;
        practiceForLevel3.Level34HadLoaded -= UpdateLevel34Status;
        practiceForLevel3.answerIs -= UpdateAnswer;
    }

    // check trigger
    private void checkTrigger (bool rightTriggerDown, bool leftTriggerDown)
    {
        if(rightTriggerDown || leftTriggerDown)
        {
            startTrigger = true;
            if (!soundHadPlay)
            {
                PlaySounds();
                soundHadPlay = true;
            }
        }

        if (!triggeringSound.isPlaying)
        {
            startTrigger = false;
        }

        if (!rightTriggerDown && !leftTriggerDown)
        {
            soundHadPlay = false;
        }
    }
    // check hitted obj
    private void updateHittedObject(GameObject hitObject)
    {
        currentHittedObject = hitObject;
        Name = hitObject.name;
        Tag = hitObject.tag;
        // when trigger navObj
        if (startTrigger && !triggeringSound.isPlaying)
        {
            LoadScenes();
            resetScore();
            if (resetBtnHadHited != null)
                resetBtnHadHited(ResetBtnHited);
        }
        else
        {
            ResetBtnHited = false;
            if (resetBtnHadHited != null)
                resetBtnHadHited(ResetBtnHited);
        }
        // when trigger tarObj
        HitAnswer();
    }

    // play the triggering sound
    private void PlaySounds()
    {
        getAnswer();

        if (currentHittedObject.tag != rightAnswer)
            triggeringSound.PlayOneShot(triggeringClip_2);
        else
            triggeringSound.PlayOneShot(triggeringClip_1);

    }

    // when trigger navObj
    private void LoadScenes()
    { 
        if (Name != "Btn_reset")
        {
            SceneManager.LoadScene(Name);
        }    
    }
    private void resetScore()
    {
        if (Name == "Btn_reset")
            ResetBtnHited = true;
        else
            ResetBtnHited = false;
    }

    // when trigger tarObj
    private void UpdateLevel34Status(bool level3HadLoaded, bool level4HadLoaded)
    {
        Level3HadLoaded = level3HadLoaded;
        Level4HadLoaded = level4HadLoaded;
    }
    private void UpdateAnswer (bool answerIsTarget1, bool answerIsTarget2)
    {
        AnswerIstarget1 = answerIsTarget1;
        AnswerIstarget2 = answerIsTarget2;
    }
    private void getAnswer()
    {
        if (Level3HadLoaded || Level4HadLoaded)
        {
            if (AnswerIstarget1)
            {
                rightAnswer = "rightAnswer_1";
                distractor = "rightAnswer_2";
            }
            else if (AnswerIstarget2)
            {
                rightAnswer = "rightAnswer_2";
                distractor = "rightAnswer_1";
            }
        }
        else
        {
            rightAnswer = "rightAnswer";
            distractor = "distractor";
        }
    }
    private void HitAnswer()
    {
        getAnswer();
        if (Tag == rightAnswer)
        {
          rightAnswerHitted = true;
          wrongAnswerHitted = false;
        }
        else if (Tag == distractor || Tag == "distractor")
        {
          rightAnswerHitted = false;
          wrongAnswerHitted = true;
        }
        else
        {
          rightAnswerHitted = false;
          wrongAnswerHitted = false;
         }
    }


    public static void CleanTrigger()
    {
        startTrigger = false;
        rightAnswerHitted = false;
        wrongAnswerHitted = false;
    }

}
