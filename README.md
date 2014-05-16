# MT RANDOM

MT random main class.
In Unity3d there is already a Random number generator based on the platform-specific random generator.
Here we present an alternative Random library for Unity3d designed to generate uniform Pseudo-Random deviates.
The library use a fast PRNG (Mersenne-Twister) to generate: Floating Number in range [0-1] and in range [n-m], Vector2 and Vector3 data types.
Which kind of transformations I can apply to the random uniform deviates?
The uniform deviates can be transformed with the distributions: Standard Normal Distribution and Power-Law.
In addition is possible to generate floating random deviates coming from other distributions: Poisson, Exponential and Gamma.
