using System;
using System.Collections;
using System.Runtime.CompilerServices;



// Simple matrix library by Pawel Duplaga
// Its mediocre but its mine

/*=============================================================================
**
**
** Purpose: Matrix algebra ( especially for neural networks  )
**
**
=============================================================================*/


namespace MatrixAlgebraSpace
{
    public class Matrix<T> : IEnumerable<T> where T : struct, IConvertible, IComparable
    {

        private readonly T[,]? data;
        public int rows;
        public int columns;


        // Initializes a new instance of the Matrix class with the specified number of rows and columns
        // Throws an ArgumentException if the number of rows or columns is less than or equal to zero
        public Matrix(int rows,int columns) {
            if(rows <= 0 || columns<= 0) 
                throw new ArgumentException("Matrix dimensions must be positive");

            this.rows = rows;
            this.columns = columns;
            this.data = new T[rows, columns];
        }

        // Initializes a new instance of the Matrix class from a 2D array
        public Matrix(T[,] data)
        {
            this.data = (dynamic)data;
            this.rows = data.GetLength(0);
            this.columns = data.GetLength(1);
        }

        public T this[int row, int column]
        {
            get => data[row, column];
            set => data[row, column] = value;
        }



       
        public static Matrix<T> operator * (Matrix<T> left, Matrix<T> right)
        {
            if (left.columns != right.rows)
                throw new ArgumentException("Number of columns of the first matrix must be the same as number of rows of the second matrix");

            Matrix<T> result = new Matrix<T>(left.rows, right.columns);


            //rows == columns
            for (int i = 0; i < left.rows; i++)
            {
                for (int k = 0; k < right.columns; k++)
                {
                    T result_cell_value = default(T);
                    for (int j = 0; j < left.columns; j++)
                    {
                        result_cell_value += (dynamic)left[i,j] * right[j,k];
                    }
                    result[i, k] = result_cell_value;
                }
            }

            return result;
        }

        public static Matrix<T> operator * (Matrix<T> matrix, dynamic value)
        {
            Matrix<T> result = new Matrix<T>(matrix.rows, matrix.columns);

            for (int i = 0; i < result.rows; i++)
            {
                for (int k = 0; k < result.columns; k++)
                {
                    if(typeof(T) == typeof(int))
                    {
                        result[i, k] = Convert.ToInt32(Math.Round((dynamic)matrix[i, k] * (double)value));
                    }
                    else if(typeof(T) == typeof(long))
                    {
                        result[i, k] = Convert.ToInt64(Math.Round((dynamic)matrix[i, k] * (double)value));
                    }
                    else
                    {
                        result[i, k] = matrix[i, k] * value;
                    }

                }
            }
            return result;
        }

        public static Matrix<T> operator *(dynamic value, Matrix<T> matrix)
        {
            Matrix<T> result = new Matrix<T>(matrix.rows, matrix.columns);

            for (int i = 0; i < result.rows; i++)
            {
                for (int k = 0; k < result.columns; k++)
                {
                    if (typeof(T) == typeof(int))
                    {
                        result[i, k] = Convert.ToInt32(Math.Round((dynamic)matrix[i, k] * (double)value));
                    }
                    else if (typeof(T) == typeof(long))
                    {
                        result[i, k] = Convert.ToInt64(Math.Round((dynamic)matrix[i, k] * (double)value));
                    }
                    else
                    {
                        result[i, k] = matrix[i, k] * value;
                    }

                }
            }
            return result;
        }

        public static Matrix<T> operator + (Matrix<T> left, Matrix<T> right)
        {
            if (left.columns != right.columns || left.rows != right.rows)
                throw new ArgumentException("Numbers of columns and rows must be the same in both matrixes");

            Matrix<T> result = new Matrix<T>(left.rows, left.columns);

            for (int i = 0; i < left.columns; i++)
            {
                for(int k = 0; k < left.rows; k++)
                {
                    result[i, k] = (dynamic)left[i, k] + right[i, k];
                }
            }

            return result;
        }

        public static Matrix<T> operator - (Matrix<T> left, Matrix<T> right)
        {
            if (left.columns != right.columns || left.rows != right.rows)
                throw new ArgumentException("Numbers of columns and rows must be the same in both matrixes");

            Matrix<T> result = new Matrix<T>(left.rows, left.columns);

            for (int i = 0; i < left.columns; i++)
            {
                for (int k = 0; k < left.rows; k++)
                {
                    result[i, k] = (dynamic)left[i, k] - right[i, k];
                }
            }

            return result;
        }


        /// <summary>
        /// Returns a new matrix that is the transpose of this matrix.
        /// </summary>
        /// <returns>A new matrix that is the transpose of this matrix.</returns>
        public Matrix<T> transpose()
        {
            Matrix<T> result = new Matrix<T>(this.rows,this.columns);

            for (int i = 0; i < result.columns; i++)
            {
                for (int k = 0; k < result.rows; k++)
                {
                    result[i, k] = this[this.rows - i - 1, this.columns - k - 1];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a new matrix that is the inverse of this matrix.
        /// </summary>
        /// <returns>A new matrix that is the inverse of this matrix.</returns>
        public Matrix<T> Inverse() { return null; }


        public T Determinant2x2()
        {
            if (this.columns != 2 && this.rows != 2)
                throw new ArgumentException("this method is for 2x2 matrixes only");

            T a = this[0, 0];
            T b = this[0, 1];
            T c = this[1, 0];
            T d = this[1, 1];

            dynamic ad = (dynamic)a * d;
            dynamic bc = (dynamic)b * c;
            dynamic det = ad - bc;

            return det;
        }


        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        public T Determinant()
        {
            int size1 = this.rows;
            int size2 = this.columns;

            if (size1 == 1 && size2 == 1)
            {
                return this[0,0];
            }
            else if (size1 == 2 && size2 == 2)
            {
                return this.Determinant2x2();
            }
            else
            {
                double det = 0;

                for (int j = 0; j < size1; j++)
                {
                    T[,] submatrix = new T[size1 - 1, size2 - 1];

                    for (int i = 1; i < size1; i++)
                    {
                        for (int k = 0; k < size1; k++)
                        {
                            if (k < j)
                            {
                                submatrix[i - 1, k] = (dynamic)this[i, k];
                            }
                            else if (k > j)
                            {
                                submatrix[i - 1, k - 1] = (dynamic)this[i, k];
                            }
                        }
                    }

                    Matrix<T> subMatrixObj = new Matrix<T>(submatrix);
                    det += Math.Pow(-1, j) * (dynamic)this[0, j] * subMatrixObj.Determinant();
                }

                return (dynamic)det;
            }
        }


        /// <summary>
        /// Fills the matrix with random values between the specified minimum and maximum values.
        /// </summary>
        /// <param name="min">The minimum value for the random number generation.</param>
        /// <param name="max">The maximum value for the random number generation.</param>
        public void FillRandom(dynamic min, dynamic max)
        {
            Random rand = new Random();

            for(int i = 0; i < this.rows; i++)
            {
                for(int k = 0;k < this.columns; k++)
                {

                    dynamic randomValue = default(T);

                    if (typeof(T) == typeof(double))
                    {
                        randomValue = Helpers.DoubleRandom(min, max, rand);
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        randomValue = Helpers.FloatRandom(min, max, rand);
                    }
                    else if (typeof(T) == typeof(long))
                    {
                        randomValue = Helpers.LongRandom(min, max, rand);
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        randomValue = rand.Next(min, max);
                    }
                    else if (typeof(T) == typeof(byte))
                    {
                        throw new ArgumentException("this library doesnt operate on bytes");
                    }
                    else throw new ArgumentException("Ups, something went wrong");

                    this[i, k] = randomValue;
                }
            }
        }

        public void FillWithValue(T value)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int k = 0; k < this.columns; k++)
                {
                    this[i,k] = value;
                }
            }
        }


        public void PrintMatrix()
        {

            // Find the length of the longest element
            int maxLength = 0;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    int length = this[i, j].ToString().Length;
                    if (length > maxLength)
                    {
                        maxLength = length;
                    }
                }
            }

            // Print the array with even spacing
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    string element = this[i, j].ToString();
                    Console.Write(element.PadLeft(maxLength) + " ");
                }
                Console.WriteLine();
            }
        }



        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in data)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Creates a new matrix with the same dimensions and elements as this matrix.
        /// </summary>
        /// <returns>A copy of this matrix.</returns>
        public Matrix<T> Clone()
        {
            return (Matrix<T>)this.MemberwiseClone();
        }

        private static class Helpers{

            public static long LongRandom(long min, long max, Random rand)
            {
                byte[] buf = new byte[8];
                rand.NextBytes(buf);
                long longRand = BitConverter.ToInt64(buf, 0);

                return (Math.Abs(longRand % (max - min)) + min);
            }

            public static double DoubleRandom(double min, double max, Random rand)
            {
                return rand.NextDouble() * (max - min) + min;
            }

            public static float FloatRandom(float min, float max, Random rand)
            {
                return (float)rand.NextDouble() * (max - min) + min;
            }


        }



    }
}

