
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORGate : Gate {
    protected override bool ComputeGate(GateInputPort[] inputPorts) {
        foreach(GateInputPort input in inputPorts) {
            if(input.IsActive) {
                return true;
            }
        }

        return false;
    }
}
