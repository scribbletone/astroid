using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class Ship : MonoBehaviour {

  void Animate(string activeAnimationState){
    string[] animationStates = new string[] {"thrustingForward", "rotatingRight", "rotatingLeft", "exploding", "idle"};
    foreach (string animationState in animationStates){
      animator.SetBool(animationState, false);    
    }
    animator.SetBool(activeAnimationState, true);
  }

  void AnimateFuel(bool shouldAnimate) {
    refuelingAnimator.SetBool("refueling", shouldAnimate);
  }

}