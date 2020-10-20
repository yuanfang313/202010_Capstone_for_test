using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenItems : MonoBehaviour
{
    public static GameObject ringInScene0, ringInScene1, ringInScene2;
    public static ParticleSystem ringParticleInScene1, ringParticleInScene2;
    public static Animator deerAnimator;
    public static Animator targetAnimator;
    public static OVRInput.Controller controllerMask;

    [Header("Models")]
    public GameObject[] prefabTarget3 = new GameObject[4];
    public List<GameObject> prefabTargets = new List<GameObject>();
    // public region
    [Header("Position Anchor")]
    public List<Transform> target_p = new List<Transform>();
    public Transform bubbleTransformAnchor;
    public Transform fallingBalloonTransformAnchor;
    public Transform raisingBalloonTransformAnchor;
    [Header("Particle Effects")]
    public GameObject ringEffect_0;
    public GameObject ringEffect_1; 
    public GameObject ringEffect_2;
    public GameObject bubbleParticles;
    public GameObject balloonFallingParticles;
    public GameObject balloonRaisingParticles;
    public GameObject prefabTrailObject;
    

    // private region
    private GameObject target1InScene;
    private GameObject target2InScene;
    private GameObject target3InScene;
    private GameObject trailsInScene;
    private GameObject bubbleInScene;
    private GameObject fallingBalloonInScene;
    private GameObject raisingBalloonInScene;
    
    
    private ParticleSystem[] ringParticleInScene0;
    
    private Transform rightAnswerTransformForRings;
 
    private bool startRing0 = false;
    private bool startRing1 = false;
    private bool startRing2 = false;
    private bool startBubbles = false;
    private bool startBalloons = false;
    private bool AnswerIsTarget1 = false;
    private bool AnswerIsTarget2 = false;

    public float startTimeBtwShots;
    private float timeBtwShots;

    private Transform controllerTransform;

    private void Awake()
    {
        ControllerStatus.OnControllerSource += UpdateOrigin;
        practiceForLevel3.answerIs += UpdateTargetForLevel3;
    }
    private void OnDestroy()
    {
        ControllerStatus.OnControllerSource -= UpdateOrigin;
        practiceForLevel3.answerIs -= UpdateTargetForLevel3;
    }
    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        controllerTransform = controllerObject.transform;
        controllerMask = controller;
    }

    public void GenerateTargetsForTutorial()
    {
        int positions = Random.Range(1, 3);

        if (positions == 1)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[0].position, Quaternion.AngleAxis(230, Vector3.up));
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings").transform;
        }
        else if (positions == 2)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[1].position, Quaternion.AngleAxis(130, Vector3.up));
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings").transform;
        }

        deerAnimator = target1InScene.GetComponent<Animator>();
    }

    public void GenerateTargetsForLevel12()
    {
        int positions = Random.Range(1, 3);

        if (positions == 1)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[0].position, Quaternion.AngleAxis(230, Vector3.up));
            target2InScene = Instantiate(prefabTargets[1], target_p[1].position, Quaternion.AngleAxis(130, Vector3.up));
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings").transform;
        }
        else if (positions == 2)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[1].position, Quaternion.AngleAxis(130, Vector3.up));
            target2InScene = Instantiate(prefabTargets[1], target_p[0].position, Quaternion.AngleAxis(230, Vector3.up));
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings").transform;
        }

        targetAnimator = target1InScene.GetComponent<Animator>();
    }

    public void GenerateTargetsForLevel3()
    {
        int position = Random.Range(1, 3);

        if (position == 1)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[0].position, Quaternion.AngleAxis(230, Vector3.up));
            target2InScene = Instantiate(prefabTargets[1], target_p[1].position, Quaternion.AngleAxis(130, Vector3.up));
            GetComponentsForLevel3();
        }
        else if (position == 2)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[1].position, Quaternion.AngleAxis(130, Vector3.up));
            target2InScene = Instantiate(prefabTargets[1], target_p[0].position, Quaternion.AngleAxis(230, Vector3.up));
            GetComponentsForLevel3();
        }
    }

    public void GenerateTargetsForLevel4()
    {
        int position = Random.Range(1, 7);
        int index = (int)Random.Range(0, 4);

        if (position == 1)
        {
            genTarget(0, 1, 2);
        }
        else if (position == 2)
        {
            genTarget(0, 2, 1);
        }
        else if (position == 3)
        {
            genTarget(1, 0, 2);
        }
        else if (position == 4)
        {
            genTarget(1, 2, 0);
        }
        else if (position == 5)
        {
            genTarget(2, 1, 0);
        }
        else if (position == 6)
        {
            genTarget(2, 0, 1);
        }

        
        void genTarget(int i_0, int i_1, int i_2)
        {
            int[] angles = {230, 130, 150};

            target1InScene = Instantiate(prefabTargets[0], target_p[i_0].position, Quaternion.AngleAxis(angles[i_0], Vector3.up));
            target2InScene = Instantiate(prefabTargets[1], target_p[i_1].position, Quaternion.AngleAxis(angles[i_1], Vector3.up));
            target3InScene = Instantiate(prefabTarget3[index], target_p[i_2].position, Quaternion.AngleAxis(angles[i_2], Vector3.up));
            GetComponentsForLevel3();
        }
    }

    private void UpdateTargetForLevel3(bool answerIsTarget1, bool answerIsTarget2)
    {
        AnswerIsTarget1 = answerIsTarget1;
        AnswerIsTarget2 = answerIsTarget2;
    }

    private void GetComponentsForLevel3()
    {
        if (AnswerIsTarget1)
        {
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings_1").transform;
            targetAnimator = target1InScene.GetComponent<Animator>();
        }
        else if (AnswerIsTarget2)
        {
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings_2").transform;
            targetAnimator = target2InScene.GetComponent<Animator>();
        }
    }


    public void GenerateTrailObject()
    {
        if (timeBtwShots <= 0)
        {
            trailsInScene = Instantiate(prefabTrailObject, controllerTransform.position, Quaternion.identity);
            OVRInput.SetControllerVibration(0.5f, 0.5f, controllerMask);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            OVRInput.SetControllerVibration(0.1f, 0.1f, controllerMask);
        }
    }
    // generate ring1 + ring2
    public void GenerateRingObject0()
    {
        if (!startRing0)
        {
            ringInScene0 = Instantiate(ringEffect_0, rightAnswerTransformForRings.position, Quaternion.AngleAxis(0, Vector3.left));
            ringParticleInScene0 = ringInScene0.GetComponentsInChildren<ParticleSystem>();
            startRing0 = true;
        }
    }
    // generate ring1
    public void GenerateRingObject1()
    {
        if (!startRing1)
        {
            ringInScene1 = Instantiate(ringEffect_1, rightAnswerTransformForRings.position, Quaternion.AngleAxis(-90, Vector3.left));
            ringParticleInScene1 = ringInScene1.GetComponent<ParticleSystem>();
            startRing1 = true;
        }
    }
    // generate ring2
    public void GenerateRingObject2()
    {
        if (!startRing2)
        {
            ringInScene2 = Instantiate(ringEffect_2, rightAnswerTransformForRings.position, Quaternion.AngleAxis(-90, Vector3.left));
            ringParticleInScene2 = ringInScene2.GetComponent<ParticleSystem>();
            startRing2 = true;
        }
    }
    // generate bubbles
    public void GenerateBubbles()
    {
        if (!startBubbles)
        {
            bubbleInScene = Instantiate(bubbleParticles, bubbleTransformAnchor.position, Quaternion.AngleAxis(-90, Vector3.left));
            startBubbles = true;
        }
    }
    // generate balloons
    public void GenerateFallingBalloons()
    {
        if (!startBalloons)
        {
            fallingBalloonInScene = Instantiate(balloonFallingParticles, fallingBalloonTransformAnchor.position, Quaternion.AngleAxis(90, Vector3.left));
            startBalloons = true;
        }
    }

    public void GenerateRaisingBalloons()
    {
        if (!startBalloons)
        {
            raisingBalloonInScene = Instantiate(balloonRaisingParticles, raisingBalloonTransformAnchor.position, Quaternion.identity);
            startBalloons = true;
            Destroy(raisingBalloonInScene, 9.0f);
        }
    }

    public void CleanGenItems_t()
    {
        if (deerAnimator != null)
            deerAnimator.SetBool("isWalking", false);
        else if (targetAnimator != null)
            targetAnimator.SetBool("isWalking", false);

        OVRInput.SetControllerVibration(0, 0, controllerMask);

        if (ringInScene0 != null)
            Destroy(ringInScene0);

        if (ringInScene1 != null)
            Destroy(ringInScene1);

        if (ringInScene2 != null)
            Destroy(ringInScene2);

        if (bubbleInScene != null)
            Destroy(bubbleInScene);

        if (raisingBalloonInScene != null)
            Destroy(raisingBalloonInScene);
        
        if (target1InScene != null)
            Destroy(target1InScene);

        if (target2InScene != null)
            Destroy(target2InScene);

        if (target3InScene != null)
            Destroy(target3InScene);

            
        startRing0 = false;
        startRing1 = false;
        startRing2 = false;
       
        startBubbles = false;
        startBalloons = false;
    }

}
