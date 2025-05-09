using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontalvalue;
    int verticalValue;

    void Awake()
    {
        animator = GetComponent<Animator>();
        horizontalvalue = Animator.StringToHash("Horizontal");
        verticalValue = Animator.StringToHash("Vertical");

    }

    public void ChangeAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontalMovement;

        float snappedVerticalMovement;

        #region Snapped Horizontal

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontalMovement = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontalMovement = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontalMovement = -0.55f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontalMovement = -1f;
        }
        else
        {
            snappedHorizontalMovement = 0f;
        }
        #endregion

        #region Snapped Vertical

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVerticalMovement = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVerticalMovement = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVerticalMovement = -0.55f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVerticalMovement = -1f;
        }
        else
        {
            snappedVerticalMovement = 0f;
        }
        #endregion

        animator.SetFloat(horizontalvalue, snappedHorizontalMovement, 0.1f, Time.deltaTime); 
        animator.SetFloat(verticalValue, snappedVerticalMovement, 0.1f, Time.deltaTime);

    }
}
