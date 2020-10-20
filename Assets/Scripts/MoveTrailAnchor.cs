using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrailAnchor : MonoBehaviour
{
    public practiceForLevel3 p_Level3;
    public Transform originalTrailAnchorTransform;
    private Transform targetTransformForTrails;
    private bool AnswerIsTarget1 = false;
    private bool AnswerIsTarget2 = false;

    private void Awake()
    {
        practiceForLevel3.answerIs += UpdateTarget;
    }
    void Start()
    {
        targetTransformForTrails = originalTrailAnchorTransform;
    }
    private void OnDestroy()
    {
        practiceForLevel3.answerIs -= UpdateTarget;
    }

    private void UpdateTarget(bool answerIsTarget1, bool answerIsTarget2)
    {
        AnswerIsTarget1 = answerIsTarget1;
        AnswerIsTarget2 = answerIsTarget2;
    }

    // Update is called once per frame
    void Update()
    {
        if (AnswerIsTarget1 && !AnswerIsTarget2)
        {
            targetTransformForTrails = GameObject.FindGameObjectWithTag("rightAnswerPosition_1").transform;
            transform.position = targetTransformForTrails.position;
        }
        else if (AnswerIsTarget2 && !AnswerIsTarget1)
        {
            targetTransformForTrails = GameObject.FindGameObjectWithTag("rightAnswerPosition_2").transform;
            transform.position = targetTransformForTrails.position;
        }
        else if (!AnswerIsTarget1 && !AnswerIsTarget2)
        {
            targetTransformForTrails = originalTrailAnchorTransform;
            transform.position = targetTransformForTrails.position;
        }
    }
}
