using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class practiceForTutorial : MonoBehaviour
{
    public PlayVoice playVoice;
    public GenItems genItems;

    #region publicTimers
    [Header("TIMERS")]

    [Tooltip("welcomeTimer is the timer before 'welcome clip'")]
    [SerializeField] private float welcomeTimer = 0;

    [Tooltip("intervalTimer is the timer between 'welcome clip' and 'ready-touch clip'")]
    [SerializeField] private float intervalTimer = 0;

    [Tooltip("rightAnswerTimer is the timer between 'goodJob clip' and 'tryAnotherRound clip' ")]
    [SerializeField] private float rightAnswerTimer = 0;

    [Tooltip("voicePTimer1 is the first timer of the frequency of voice prompt")]
    [SerializeField] private float voicePTimer1 = 0;

    [Tooltip("voicePTimer2 is the second timer of the frequency of voice prompt")]
    [SerializeField] private float voicePTimer2 = 0;

    [Tooltip("visualPromptTimer1_f is the timer before the first visual prompt appearing")]
    [SerializeField] private float visualPromptTimer1_f = 0;

    [Tooltip("visualPromptTimer2_f is the timer between the visual prompts")]
    [SerializeField] private float visualPromptTimer2_f = 0;

    [Tooltip("visualPromptTimer1_d is the first timer of the duration of the visual prompts")]
    [SerializeField] private float visualPromptTimer1_d = 0;

    [Tooltip("visualPromptTimer2_d is the second timer of the duration of the visual prompts")]
    [SerializeField] private float visualPromptTimer2_d = 0;
    #endregion

    #region privateTimers
    private float _welcomeTimer = 0;
    private float _intervalTimer = 0;
    private float _rightAnswerTimer = 0;
    private float _voicePTempTimer = 0;
    private float _visualPTempTimer_f = 0;
    private float _visualPTempTimer_d = 0;
    #endregion

    #region counters
    private int rightAnswerCount = 0;
    private int voicePromptCounter = 0;
    private int visualPromptCounter = 0;
    #endregion

    #region privateBool
    private bool[] hadPlay1 = new bool[5];
    private bool[] hadPlay3 = new bool[3];
   
    private bool generated = false;
    private bool answered = false;
    private bool answerIsCorrect = false;
    private bool startPrompts = false;
    private bool startTrail = false;
    private bool roundHadFinished = false;
    
    #endregion

    private int thisScene;
    //private int nextSceneToLoad;
    private GameObject talkingObject;

    private void Start()
    {
        thisScene = SceneManager.GetActiveScene().buildIndex;
        talkingObject = GameObject.FindGameObjectWithTag("talkingObject");

        cleanBoolArray(hadPlay1);
        cleanBoolArray(hadPlay3);
        CleanTimerCounter();
    }

    private void Update()
    {
        SetupQuestion();
        voicePromt();
        visualPrompt();
        AnswerIsRightEventHandler();
        AnswerIsWrongEventHandlder();

        WelcomeTimer();
        IntervalTimer();
        voicePromptTimer();
        visualPromptTimer_fd();
        RightAnswerTimer();

        LoadSceneToNextLevel();

        if (answered && answerIsCorrect)
            AnswerIsRight();
        else
            return;   
    }

    private void SetupQuestion()
    {
        // play welcome clip
        if(!hadPlay1[0] && _welcomeTimer <= 0)
        {
            playVoice.playVoice_1(0);
            hadPlay1[0] = true;
            roundHadFinished = true;
        }

        // play readyTouch clip
        if(!hadPlay1[1] && _intervalTimer <= 0 && roundHadFinished && rightAnswerCount < 5)
        {
            talkingObject.SetActive(false);
            playVoice.playVoice_1(1);
            hadPlay1[1]= true;
        }

        // generate items randomly
        if(hadPlay1[1] && roundHadFinished && !generated)
        {
            genItems.GenerateTargetsForTutorial();
            generated = true;
            roundHadFinished = false;
        }
    }

    private void AnswerIsRightEventHandler()
    {
        if(triggeringEffects.rightAnswerHitted && !triggeringEffects.wrongAnswerHitted && triggeringEffects.startTrigger)
        {
            answered = true;
            answerIsCorrect = true;
        }
    }

    private void AnswerIsWrongEventHandlder()
    {
        if(!triggeringEffects.rightAnswerHitted && triggeringEffects.wrongAnswerHitted && triggeringEffects.startTrigger)
        {
            answered = true;
            answerIsCorrect = false;
        }
    }

    private void WelcomeTimer()
    {
        if (hadPlay1[0])
            _welcomeTimer = welcomeTimer;
        else
            _welcomeTimer -= Time.deltaTime;
    }

    private void IntervalTimer()
    {
        if (!PlayVoice.voiceAudioSource.isPlaying && roundHadFinished)
            _intervalTimer -= Time.deltaTime;
        else
            _intervalTimer = intervalTimer;
    }

    private void RightAnswerTimer()
    {
        if (answerIsCorrect && answered && !PlayVoice.voiceAudioSource.isPlaying && !triggeringEffects.triggeringSound.isPlaying)
            _rightAnswerTimer -= Time.deltaTime;
    }

    private void CleanTimerCounter()
    {
        _welcomeTimer = welcomeTimer;
        _intervalTimer = intervalTimer;
        _rightAnswerTimer = rightAnswerTimer;
        _voicePTempTimer = voicePTimer1;
        _visualPTempTimer_f = visualPromptTimer1_f;
        _visualPTempTimer_d = visualPromptTimer1_d;
        visualPromptCounter = 0;
        voicePromptCounter = 0;
    }

    private void cleanBoolArray(bool[] boolArray)
    {
        for (int i = 0; i < boolArray.Length; i++)
        {
            boolArray[i] = false;
        }
    }


    private void voicePromt()
    {
        if (_voicePTempTimer <= 0 && !hadPlay1[3])
        {
            playVoice.playVoice_1(3);
            hadPlay1[3] = true;
        }
            
        if(hadPlay1[3])
        {
            voicePromptCounter = voicePromptCounter + 1;

            if (voicePromptCounter <= 5)
                _voicePTempTimer = voicePTimer1;
            else if (voicePromptCounter > 5)
                _voicePTempTimer = voicePTimer2;

            hadPlay1[3] = false;
            answered = false;
            answerIsCorrect = false;
            triggeringEffects.CleanTrigger();
            generated = true;
            roundHadFinished = false;
        }
    }

    private void voicePromptTimer()
    {
        if (generated && !answered && !PlayVoice.voiceAudioSource.isPlaying)
            _voicePTempTimer -= Time.deltaTime;
    }

    private void visualPrompt()
    {
        if(_visualPTempTimer_f <= 0 && !startPrompts && !answered)
        {
            switch (visualPromptCounter)
            {
                case 0:
                    genItems.GenerateRingObject1();
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    break;
                case 1:
                    genItems.GenerateRingObject2();
                    GenItems.ringParticleInScene1.Play();
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    break;
                case 2:
                    GenItems.ringParticleInScene1.Play();
                    GenItems.ringParticleInScene2.Play();
                    GenItems.deerAnimator.SetBool("isWalking", true);
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    break;
                case 3:
                    GenItems.ringParticleInScene1.Play();
                    GenItems.ringParticleInScene2.Play();
                    GenItems.deerAnimator.SetBool("isWalking", true);
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    startTrail = true;
                    break;
                default:
                    GenItems.ringParticleInScene1.Play();
                    GenItems.ringParticleInScene2.Play();
                    GenItems.deerAnimator.SetBool("isWalking", true);
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    startTrail = true;
                    break;
            }   
        }

        if (startTrail)
            genItems.GenerateTrailObject();
        else if (!startTrail)
            OVRInput.SetControllerVibration(0, 0, GenItems.controllerMask);

        if(_visualPTempTimer_d <= 0 && startPrompts && !answered)
        {
            if (GenItems.ringParticleInScene1 != null)
                GenItems.ringParticleInScene1.Stop();

            if (GenItems.ringParticleInScene2 != null)
                GenItems.ringParticleInScene2.Stop();

            if (GenItems.deerAnimator != null)
                GenItems.deerAnimator.SetBool("isWalking", false);

            if (visualPromptCounter < 4)
                _visualPTempTimer_d = visualPromptTimer1_d;
            else if (visualPromptCounter >= 4)
                _visualPTempTimer_d = visualPromptTimer2_d;

            visualPromptCounter += 1;
            startTrail = false;
            startPrompts = false;
        }
    }

    private void visualPromptTimer_fd()
    {
        if (generated && !answered)
        {
            if (!startPrompts)
                _visualPTempTimer_f -= Time.deltaTime;
            else if (startPrompts)
                _visualPTempTimer_d -= Time.deltaTime;
        }
    }

    private void AnswerIsRight()
    {
        if (!hadPlay1[2] && !triggeringEffects.triggeringSound.isPlaying)
        {
            playVoice.playVoice_1(2);
            rightAnswerCount += 1;
            hadPlay1[2] = true;
        }
        
        rightAnswerEffects();

        if(_rightAnswerTimer <= 0 && hadPlay1[2])
        {
            if (!hadPlay3[0] && rightAnswerCount < 5)
            {
                playVoice.playVoice_3(0);
                hadPlay3[0] = true;
            } else if (!hadPlay1[4] && rightAnswerCount == 5)
            {
                talkingObject.SetActive(true);
                playVoice.playVoice_1(4);
                hadPlay1[4] = true;
            }        
        }

        if (hadPlay3[0])
            cleanField();
        else if (hadPlay1[4] && !PlayVoice.voiceAudioSource.isPlaying)
            cleanField();
    }

    private void cleanField()
    {
        PlaySoundToNextLevel();
        genItems.CleanGenItems_t();
        CleanTimerCounter();

        if (hadPlay1[4])
        {
            genItems.GenerateBubbles();
            genItems.GenerateFallingBalloons();
        }

        hadPlay1[1] = false;
        hadPlay1[2] = false;
        hadPlay1[3] = false;
        hadPlay1[4] = false;
        hadPlay3[0] = false;
        hadPlay3[1] = false;
        startTrail = false;
        startPrompts = false;
        answered = false;
        answerIsCorrect = false;
        triggeringEffects.CleanTrigger();
        generated = false;
        roundHadFinished = true;
    }

    private void rightAnswerEffects()
    {
        if (hadPlay1[2] && !hadPlay3[1] && !PlayVoice.voiceAudioSource.isPlaying)
        {
            playVoice.playVoice_3(1);
            hadPlay3[1] = true;
        }
        if(hadPlay3[1])
            genItems.GenerateBubbles();

        if (GenItems.ringInScene1 != null)
            GenItems.ringParticleInScene1.Stop();

        if (GenItems.ringInScene2 != null)
            GenItems.ringParticleInScene2.Stop();

        if (startTrail)
            startTrail = false;

        if (GenItems.deerAnimator != null)
            GenItems.deerAnimator.SetBool("isWalking", true);

        genItems.GenerateRingObject0();
    }


    // to Next Level
    private void PlaySoundToNextLevel()
    {
        if (hadPlay1[4])
        {
            if (!hadPlay3[2] && !PlayVoice.voiceAudioSource.isPlaying)
            {
                playVoice.playVoice_3(2);
                hadPlay3[2] = true;
            }
        }
    }

    private void LoadSceneToNextLevel()
    {
        if (hadPlay3[2] && !PlayVoice.voiceAudioSource.isPlaying)
        {
            SceneManager.LoadScene("Level_1-1");
        }
    }
}
