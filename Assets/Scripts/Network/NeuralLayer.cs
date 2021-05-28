using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralLayer
{
    public List<Neuron> _neurons;
    
    public NeuralLayer(int n_inputs, int n_neurons){
        _neurons = new List<Neuron>();

        for(int i = 0; i < n_neurons; ++i){
            _neurons.Add(new Neuron(n_inputs));
        }
    }

    public List<float> FeedForward(List<float> inputs){
        List<float> neuronOutputs = new List<float>();

        for (int i = 0; i < _neurons.Count; ++i){
            neuronOutputs.Add(_neurons[i].CalculateOutput(inputs));
        }

        return neuronOutputs;
    }

    public void SetNeurons(List<Neuron> neurons){
        for(int i = 0; i < neurons.Count; ++i){
            _neurons[i].SetWeightsAndBias(neurons[i]._weights, neurons[i]._bias);
        }
    }
}
