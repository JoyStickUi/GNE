using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public List<float> _weights;
    public float _output = 0.0f;
    public float _bias = 1;

    private List<float> _inputs;
    private int _n_weights;

    public Neuron(int n_weights){
        _weights = new List<float>();
        _inputs = new List<float>();
        _n_weights = n_weights;

        for(int i = 0; i < _n_weights; ++i){
            _weights.Add(Random.Range(0.0f, 1.0f));
        }
    }

    public float CalculateOutput(List<float> inputs){
        if (inputs.Count != _n_weights){
            throw new System.Exception("Wrong inputs number");
        }

        float output = 0.0f;

        for(int i = 0; i < _n_weights; ++i){
            output += inputs[i] * _weights[i];
        }

        float a_output = ActivationFunction.Sigmoid(output + _bias);

        _inputs = inputs;
        _output = a_output;

        return a_output;
    }

    public void SetWeightsAndBias(List<float> weights, float bias){
        _weights = weights;
        _bias = bias;
    }
}
