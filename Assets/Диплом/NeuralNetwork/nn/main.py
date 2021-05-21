from nn import *

# example of XOR
train_dataset = [
            [(100, 100, 100), [1]],
            [(50, 50, 50), [0.5]],
            [(0, 0, 0), [0]]
            ]

nn = NeuralNetwork(learning_rate=0.01, debug=False)
nn.add_layer(n_inputs=3, n_neurons=10)
nn.add_layer(n_inputs=10, n_neurons=1)

nn.train(dataset=train_dataset, n_iterations=10000, print_error_report=True)

# test
test_dataset = [
    [(100, 100, 100), [1]],
    [(50, 50, 50), [0.5]],
    [(0, 0, 0), [0]]
]
nn.test(test_dataset)