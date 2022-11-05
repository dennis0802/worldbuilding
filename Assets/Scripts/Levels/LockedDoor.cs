using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour{
    public bool locked = true;
    public List<Rigidbody> doors = new List<Rigidbody>();
}