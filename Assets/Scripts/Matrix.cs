using System;

public class Matrix
{
    private float[] _values;
    private int _rows;
    private int _cols;


    public Matrix(float[] values, int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        _values = new float[_rows * _cols];

        Array.Copy(values, _values, _values.Length);
    }


    public Matrix(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        _values = new float[_rows * _cols];
    }

    public float GetValueAt(int row, int col)
    {
        return _values[row * _rows + col];
    }

    public override string ToString()
    {
        string matrixStr = "";

        for (int rowIndex = 0; rowIndex < _rows; ++rowIndex)
        {
            for (int colIndex = 0; colIndex < _cols; ++colIndex)
            {
                matrixStr += _values[rowIndex * _cols + colIndex].ToString() + ' ';
            }
            matrixStr += '\n';
        }

        return matrixStr;
    }

    static public Matrix operator +(Matrix first, Matrix second)
    {
        bool sameSize = (first._cols == second._cols) && (first._rows == second._rows);

        if (!sameSize)
            return null;


        int length = first._rows * first._cols;
        Matrix result = new Matrix(first._values, first._rows, first._cols);

        for (int index = 0; index < length; ++index)
        {
            result._values[index] += second._values[index];
        }

        return result;
    }

    static public Matrix operator *(Matrix first, Matrix second)
    {
        if (first._cols != second._rows)
            return null;


        int rows = first._rows;
        int cols = second._cols;

        Matrix result = new Matrix(rows, cols);

        // Пока текущий индекс строки первой м. меньше макс. кол. строк
        for (int first_rowIndex = 0; first_rowIndex < rows; ++first_rowIndex)
        {
            // Пока текущий индекс столпца второй м. меньше макс кол. столпцов
            for (int second_colIndex = 0; second_colIndex < cols; ++second_colIndex)
            {
                result._values[first_rowIndex * cols + second_colIndex] = MultMatrixRowCol(first, second, first_rowIndex, second_colIndex);
            }
        }

        return result;
    }

    private static float MultMatrixRowCol(Matrix first, Matrix second, int first_rowIndex, int second_colIndex)
    {
        if (first._cols != second._rows)
            return float.NaN;


        int sharedSize = first._cols;
        float result = 0f;

        // Пока текущий индекс суммирования меньше кол. столпцов первой м.
        for (int currentSumIndex = 0; currentSumIndex < sharedSize; ++currentSumIndex)
        {
            // Умножить и прибавить к результирующей матрице (матрица.значения[индекс строки первой м. * кол. столпцов + индекс столпца второй м.]) :
            // м1.значения [индекс строки первой м. * кол. столпцов первой м. + текущий индекс суммирования]
            // м2.значения[текущий индекс суммирования * кол. столпцов второй м. + индекс столпца второй м.]

            result += first._values[first_rowIndex * first._cols + currentSumIndex] * second._values[currentSumIndex * second._cols + second_colIndex];
        }

        return result;
    }

    public Coords AsCoords()
    {
        if(_cols == 1 && _rows == 4)
        {
            return new Coords(_values[0], _values[1], _values[2], _values[3]);
        }
        else
        {
            return null;
        }
    }
}
