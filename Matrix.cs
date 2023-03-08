using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixAlgebra
{
    internal class Matrix<T> where T : struct, IConvertible, IComparable
    {

        private T[,]? data;
        public int rows;
        public int columns;



        public Matrix(int rows,int columns) {
            if(rows <= 0 || columns<= 0) 
                throw new ArgumentException("Matrix dimensions must be positive");

            this.rows = rows;
            this.columns = columns;
            this.data = new T[rows, columns];
        }

        public T this[int row, int column]
        {
            get { return data[row, column]; }
            set { data[row, column] = value; }
        }

        public static Matrix<T> operator * (Matrix<T> left, Matrix<T> right)
        {



            return null;
        }


    }
}
