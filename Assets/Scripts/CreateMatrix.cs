using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int rows = 2;
        int cols = 3;

        Matrix matrix = new Matrix(values, rows, cols);

        matrix = matrix + matrix;

        //Debug.Log(matrix.ToString());

        // Mult

        float[] values_1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int rows_1 = 4;
        int cols_1 = 3;        
        
        float[] values_2 = { 1, 2, 3, 4, 5, 6 };
        int rows_2 = 3;
        int cols_2 = 2;

        Matrix first = new Matrix(values_1, rows_1, cols_1);
        Matrix second = new Matrix(values_2, rows_2, cols_2);

        Matrix result = first * second;

        Debug.Log(first.ToString() + '\n' + second.ToString() + '\n' + result.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
