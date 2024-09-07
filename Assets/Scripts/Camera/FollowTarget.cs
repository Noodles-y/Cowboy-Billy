using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
  public Transform target = null;
  public Transform cameraTf = null;
  //public float speed = 10;
  

  // Update is called once per frame
  void Update()
  {
    if(target) {
      transform.position = new Vector3(target.position.x, 0, target.position.z);
      cameraTf.LookAt(target);
    }
  }
}
