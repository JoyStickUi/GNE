using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    public List<NeuralLayer> _layers;
    
    public NeuralNetwork(){
        _layers = new List<NeuralLayer>();
    }

    public void AddLayer(int n_inputs, int n_neurons){
        NeuralLayer layer = new NeuralLayer(n_inputs, n_neurons);
        _layers.Add(layer);
    }

    public List<float> FeedForward(List<float> inputs){
        foreach(NeuralLayer layer in _layers){
            inputs = layer.FeedForward(inputs);
        }

        return inputs;
    }

    public void LoadTrainedLayers(List<NeuralLayer> layers){
        for(int i = 0; i < layers.Count; ++i){
            _layers[i].SetNeurons(layers[i]._neurons);
        }
    }

    public void ClearLayerList(){
        _layers = new List<NeuralLayer>();
    }
}
