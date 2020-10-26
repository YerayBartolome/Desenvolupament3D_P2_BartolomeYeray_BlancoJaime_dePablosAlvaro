using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchController : MonoBehaviour
{

    [SerializeField] bool mInvertPitch;
    [SerializeField] bool mInvertYaw;


    public KeyCode mForwardKey = KeyCode.W;
    public KeyCode mLeftKey = KeyCode.A;
    public KeyCode mBackKey = KeyCode.S;
    public KeyCode mRightKey = KeyCode.D;
    public KeyCode mJumpKey = KeyCode.Space;
    public KeyCode mRunKey = KeyCode.LeftShift;



}
