Matrix Algebra Library in C#
This library is designed for matrix algebra, especially for neural networks. The implementation is done using C# programming language.

# Getting Started
### Prerequisites
To use this library, you need the following software installed on your computer:

.NET 5
Installation
1. Clone the repository or download the source code
2. Build the project using your favorite IDE or by running dotnet build command in the project directory
3. Reference the resulting DLL file in your own project


# Usage 
### Creating a Matrix 
#### To create a new matrix, use the following code:
```csharp
Matrix<int> m = new Matrix<int>(3, 3);
This creates a new 3x3 matrix with integer values.
```

#### You can also create a new matrix from an existing 2D array:
```csharp
int[,] data = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
Matrix<int> m = new Matrix<int>(data);
```
### Accessing Matrix Elements 
#### You can access matrix elements using the indexing operator:
```csharp
int element = m[0, 0];
```
### Matrix Arithmetic 
#### The library supports basic arithmetic operations on matrices, including addition, subtraction, and multiplication. Here are some examples:
```csharp
Matrix<int> a = new Matrix<int>(2, 2);
Matrix<int> b = new Matrix<int>(2, 2);
```

### Matrix addition
```csharp
Matrix<int> c = a + b;
```

### Matrix  subtraction
```csharp
Matrix<int> d = a - b;
```

### Matrix  multiplication
```csharp
Matrix<int> e = a * b;
```

### Matrix Transpose 
#### You can also calculate the transpose of a matrix:
```csharp
Matrix<int> a = new Matrix<int>(2, 3);
Matrix<int> b = a.Transpose();
```

### Matrix Determinant
#### Finally, you can calculate the determinant of a 2x2 matrix and determinant of custom size matrixes as well:
```csharp
Matrix<int> a = new Matrix<int>(2, 2);
int determinant = a.Determinant2x2();
```

### Authors <br>
## Pawel Duplaga - <a href="https://github.com/PawelDuplaga" target="_new">GitHub</a>