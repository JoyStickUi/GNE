import random as rand
import numpy as np
import sys

def average(lst):
    return sum(lst) / len(lst)

def sumSynapses(synArr, bias):
    sumValue = 0
    for syn in synArr:
        sumValue += ((syn.weight * syn.inputNeuron.value) + bias)
    return sumValue

class NeuralNetwork:
    def __init__(self, inputSize, hiddenSize, outputSize, numHiddenLayers = 1, learnRate = None, momentum = None):
        self.learnRate = learnRate if learnRate != None else 0.4
        self.momentum = momentum if momentum != None else 0.9
        self.inputLayer = []
        self.hiddenLayers = []
        self.outputLayer = []

        for i in range(0, inputSize):
            self.inputLayer.append(Neuron())

        for i in range(0, numHiddenLayers):
            self.hiddenLayers.append([])
            for j in range(0, hiddenSize):
                self.hiddenLayers[i].append(Neuron(self.inputLayer if i == 0 else self.hiddenLayers[i - 1]))

        for i in range(0, outputSize):
            self.outputLayer.append(Neuron(self.hiddenLayers[numHiddenLayers - 1]))

    def TrainEps(self, dataSets, numEpochs):
        for i in range(0, numEpochs):
            for dataSet in self.dataSets:
                self.ForwardPropagate(dataSet.values)
                self.BackPropagate(dataSet.targets)

    def TrainErr(self, dataSets, minimumError):
        error = 1.0
        numEpochs = 0
        #sys.maxsize
        while error > minimumError and numEpochs < sys.maxsize:
            errors = []
            for dataSet in dataSets:
                self.ForwardPropagate(dataSet.values)
                self.BackPropagate(dataSet.targets)                
                errors.append(self.CalculateError(dataSet.targets))
            error = average(errors)
            #if numEpochs % 1000 == 0:
            #    print("-------")
            #    print(self.inputLayer[0].value, " ", self.inputLayer[1].value, " ", self.inputLayer[2].value)
            #    print(self.outputLayer[0].value, " ", self.outputLayer[1].value, " ", self.outputLayer[2].value)
            #    print(errors)
            #    print("av", error)
            numEpochs += 1

    def ForwardPropagate(self, inputs):
        i = 0
        for a in self.inputLayer:
            a.value = inputs[i]
            i += 1
        for layer in self.hiddenLayers:
            for a in layer:
                a.СalculateValue()
        for a in self.outputLayer:
            a.СalculateValue()

    def BackPropagate(self, targets):
        i = 0
        for a in self.outputLayer:
            a.CalculateGradient(targets[i])
            i += 1
        self.hiddenLayers.reverse()
        for layer in self.hiddenLayers:
            for a in layer:
                a.CalculateGradient()
                a.UpdateWeights(self.learnRate, self.momentum)
        #self.hiddenLayers.reverse()
        for a in self.outputLayer:
            a.UpdateWeights(self.learnRate, self.momentum)
    
    def Compute(self, inputs):
        self.ForwardPropagate(inputs)
        
        result = []
        for a in self.outputLayer:
            result.append(a.value)
        return result

    def CalculateError(self, targets):
        i = 0
        sumRes = 0
        for a in self.outputLayer:
            sumRes += np.absolute(a.CalculateError(targets[i]))
            i += 1
        print(sumRes)
        return np.square(sumRes) / 2

class Neuron:
    def __init__(self, inputNeurons = None):
        self.inputSynapses = []
        self.outputSynapses = []
        self.bias = rand.random()
        self.biasDelta = 0.0
        self.gradient = 0.0
        self.value = 0.0

        self.error = 0.0

        if inputNeurons != None:
            for inputNeuron in inputNeurons:
                syn = Synapse(inputNeuron, self)
                inputNeuron.outputSynapses.append(syn)
                self.inputSynapses.append(syn)

    def СalculateValue(self):
        self.value = Sigmoid.Output(sumSynapses(self.inputSynapses, self.bias))
        return self.value

    def CalculateError(self, target):
        #return target - self.value
        self.error = target - self.value
        return self.error
    
    def CalculateGradient(self, target = None):
        if target == None:
            sumG = 0.0
            for a in self.outputSynapses:
                sumG += (a.outputNeuron.gradient * a.weight)
            self.gradient = sumG * Sigmoid.Derivative(self.value)
            return self.gradient

#       self.gradient = self.CalculateError(target.value) * Sigmoid.Derivative(self.value)
        self.gradient = self.CalculateError(target) * Sigmoid.Derivative(self.value)
        return self.gradient

    def UpdateWeights(self, learnRate, momentum):
        prevDelta = self.biasDelta
        self.biasDelta = -learnRate * self.gradient
        self.bias += (self.biasDelta + momentum * prevDelta)
        
        for syn in self.inputSynapses:
            prevDelta = syn.weightDelta
            syn.weightDelta = -learnRate * self.gradient * syn.inputNeuron.value
            syn.weight += syn.weightDelta + momentum * prevDelta

class Synapse:
    def __init__(self, inputNeuron, outputNeuron):
        self.inputNeuron = inputNeuron
        self.outputNeuron = outputNeuron
        self.weight = rand.random()
        self.weightDelta = 0.0

class Sigmoid:
    @staticmethod
    def Output(x):
        return 0.0 if x < -45 else 1.0 if x > 45 else (1.0 / (1.0 + np.exp(-x)))

    @staticmethod
    def Derivative(x):
        return x * (1 - x)

class DataSet:
    def __init__(self, values, targets):
        self.values = values
        self.targets = targets


def main():
    #network = NeuralNetwork(3, 3, 3)
    #network.TrainErr([
    #    DataSet([100, 100, 100], [1, 0, 0]),
    #    DataSet([50, 50, 50], [0, 1, 0]),
    #    DataSet([0, 0, 0], [0, 0, 1])
    #], 0.1)
    #for i in range(0, 10):
    #    x = rand.randint(0, 100)
    #    y = rand.randint(0, 100)
    #    z = rand.randint(0, 100)
    #    print(x, " ", y, " ", z)
    #    print(network.Compute([x, y, z]))
    #print(100, " ", 100, " ", 100)
    #print(network.Compute([100, 100, 100]))
    #print(50, " ", 50, " ", 50)
    #print(network.Compute([50, 50, 50]))
    #print(0, " ", 0, " ", 0)
    #print(network.Compute([0, 0, 0]))
    network = NeuralNetwork(2, 3, 1)
    network.TrainErr([
        DataSet([1, 0], [1]),
        DataSet([0, 0], [0]),
        DataSet([0, 1], [1]),
        DataSet([1, 1], [0])
    ], 0.1)
    print(1, " ", 0)
    print(network.Compute([1, 0]))
    print(0, " ", 0)
    print(network.Compute([0, 0]))
    print(0, " ", 1)
    print(network.Compute([0, 1]))
    print(1, " ", 1)
    print(network.Compute([1, 1]))

main()