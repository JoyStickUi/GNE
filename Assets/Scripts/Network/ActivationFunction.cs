using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActivationFunction
{
    public static float Sigmoid(float x){
        return 1 / (1 + Mathf.Exp(-x));
    }

    public static float Dsigmoid(float x){
        return x * (1 - x);
    }
}
