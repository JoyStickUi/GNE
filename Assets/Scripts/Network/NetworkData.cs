using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkData
{
    public string[] layers;

    public List<NeuralLayer> FromJson(){
        List<NeuralLayer> _layers = new List<NeuralLayer>();

        foreach(string str in layers){
            List<Neuron> neurons = JsonUtility.FromJson<LayerData>(str).FromJson();
            NeuralLayer neuralLayer = new NeuralLayer(1, neurons.Count);
            neuralLayer.SetNeurons(neurons);
            _layers.Add(neuralLayer);
        }

        return _layers;
    }
}
