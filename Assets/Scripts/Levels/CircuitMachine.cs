using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitMachine : MonoBehaviour
{
    public List<Renderer> wires = new List<Renderer>();
    public bool partOfCircuit, firstMachine, lastMachine, powerOn = false;
    public GameObject forceField, previousMachine;
    public Renderer lightSwitch;
}