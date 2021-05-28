using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerData
{
    public string[] neurons;

    public List<Neuron> FromJson(){
        List<Neuron> _neurons = new List<Neuron>();

        foreach(string str in neurons){
            NeuronData neuronData = JsonUtility.FromJson<NeuronData>(str);
            Neuron neuron = new Neuron(neuronData.weights.Length);
            neuron.SetWeightsAndBias(new List<float>(neuronData.weights), neuronData.bias);
            _neurons.Add(neuron);
        }

        return _neurons;
    }
}
