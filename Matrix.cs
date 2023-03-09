using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


// Simple matrix library by Pawel Duplaga
// Its mediocre but its mine

/*=============================================================================
**
**
** Purpose: Matrix mathematics 
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



        public Matrix(int rows,int columns) {
            if(rows <= 0 || columns<= 0) 
                throw new ArgumentException("Matrix dimensions must be positive");

            this.rows = rows;
            this.columns = columns;
            this.data = new T[rows, columns];
        }

        public Matrix(T[,] data)
        {
            this.data = data;
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


        public Matrix<T> transpose()
        {
            Matrix<T> result = new Matrix<T>(this.rows,this.columns);

            for (int i = 0; i < result.columns; i++)
            {
                for (int k = 0; k < result.rows; k++)
                {
                    result[i, k] = this[result.rows - i - 1, result.columns - i - 1];
                }
            }




            return this;
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

    }
}

