using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolisticMath
{
    static public Coords GetNormal(Coords vector)
    {
        float length = Distance(new Coords(0, 0, 0), vector);
        vector.x /= length;
        vector.y /= length;
        vector.z /= length;

        return vector;
    }

    static public Coords Translate(Coords position, Coords vector)
    {
        Matrix positionMatrix = new Matrix(position.AsFloats(), 4, 1);

        float[] movingVectorValues =
        {
            1, 0, 0, vector.x,
            0, 1, 0, vector.y,
            0, 0, 1, vector.z,
            0, 0, 0, 1
        };

        Matrix movingMatrix = new Matrix(movingVectorValues, 4, 4);

        return (movingMatrix * positionMatrix).AsCoords();
    }

    static public Coords Rotate(
        Coords position,
        float rotationX, bool clockX,
        float rotationY, bool clockY,
        float rotationZ, bool clockZ
        )
    {
        // Check if angles must be clockwise and configure them

        rotationX = !clockX ? rotationX : 2 * Mathf.PI - rotationX;
        rotationY = !clockY ? rotationY : 2 * Mathf.PI - rotationY;
        rotationZ = !clockZ ? rotationZ : 2 * Mathf.PI - rotationZ;

        Matrix positionMatrix = new Matrix(position.AsFloats(), 4, 1);


        // X Roll

        float[] rollValuesX =
            {
            1, 0, 0, 0,
            0, Mathf.Cos(rotationX), -Mathf.Sin(rotationX), 0,
            0, Mathf.Sin(rotationX), Mathf.Cos(rotationX), 0,
            0, 0, 0, 1
        };

        Matrix rollMatrixX = new Matrix(rollValuesX, 4, 4);        
        

        // Y Roll
        
        float[] rollValuesY =
            {
            Mathf.Cos(rotationY), 0, Mathf.Sin(rotationY), 0,
            0, 1, 0, 0,
            -Mathf.Sin(rotationY), 0, Mathf.Cos(rotationY), 0,
            0, 0, 0, 1
        };

        Matrix rollMatrixY = new Matrix(rollValuesY, 4, 4);        
        
        
        // Z Roll
        
        float[] rollValuesZ =
            {
            Mathf.Cos(rotationZ), -Mathf.Sin(rotationZ), 0, 0,
            Mathf.Sin(rotationZ), Mathf.Cos(rotationZ), 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        };

        Matrix rollMatrixZ = new Matrix(rollValuesZ, 4, 4);

        // Result

        return (rollMatrixX * rollMatrixY * rollMatrixZ * positionMatrix).AsCoords();
    }

    static public Coords Scale(Coords position, float scaleX, float scaleY, float scaleZ)
    {
        float[] scaleMatrixValues =
            {
            scaleX, 0, 0, 0,
            0, scaleY, 0, 0,
            0, 0, scaleZ, 0,
            0, 0, 0, 1
        };

        Matrix scaleMatrix = new Matrix(scaleMatrixValues, 4, 4);
        Matrix positionMatrix = new Matrix(position.AsFloats(), 4, 1);

        return (scaleMatrix * positionMatrix).AsCoords();
    }

    static public Coords Shear(Coords position, float shearX, float shearY, float shearZ)
    {
        float[] shearMatrixValues =
            {
            1, shearY, shearZ, 0,
            shearX, 1, shearZ, 0,
            shearX, shearY, 1, 0,
            0, 0, 0, 1
        };

        Matrix shearMatrix = new Matrix(shearMatrixValues, 4, 4);
        Matrix positionMatrix = new Matrix(position.AsFloats(), 4, 1);

        return (shearMatrix * positionMatrix).AsCoords();
    }

    static public float Distance(Coords point1, Coords point2)
    {
        float diffSquared = Square(point1.x - point2.x) +
                            Square(point1.y - point2.y) +
                            Square(point1.z - point2.z);
        float squareRoot = Mathf.Sqrt(diffSquared);
        return squareRoot;

    }

    static public Coords Lerp(Coords A, Coords B, float t)
    {
        t = Mathf.Clamp(t, 0, 1);
        Coords v = new Coords(B.x - A.x, B.y - A.y, B.z - A.z);
        float xt = A.x + v.x * t;
        float yt = A.y + v.y * t;
        float zt = A.z + v.z * t;

        return new Coords(xt, yt, zt);
    }

    static public float Square(float value)
    {
        return value * value;
    }

    static public float Dot(Coords vector1, Coords vector2)
    {
        return (vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z);
    }

    static public float Angle(Coords vector1, Coords vector2)
    {
        float dotDivide = Dot(vector1, vector2) /
                    (Distance(new Coords(0, 0, 0), vector1) * Distance(new Coords(0, 0, 0), vector2));
        return Mathf.Acos(dotDivide); //radians.  For degrees * 180/Mathf.PI;
    }

    static public Coords LookAt2D(Coords forwardVector, Coords position, Coords focusPoint)
    {
        Coords direction = new Coords(focusPoint.x - position.x, focusPoint.y - position.y, position.z);
        float angle = HolisticMath.Angle(forwardVector, direction);
        bool clockwise = false;
        if (HolisticMath.Cross(forwardVector, direction).z < 0)
            clockwise = true;

        Coords newDir = HolisticMath.Rotate(forwardVector, angle, clockwise);
        return newDir;
    }

    static public Coords Rotate(Coords vector, float angle, bool clockwise) //in radians
    {
        if (clockwise)
        {
            angle = 2 * Mathf.PI - angle;
        }

        float xVal = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float yVal = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
        return new Coords(xVal, yVal, 0);
    }

    static public Coords Translate(Coords position, Coords facing, Coords vector)
    {
        if (HolisticMath.Distance(new Coords(0, 0, 0), vector) <= 0) return position;
        float angle = HolisticMath.Angle(vector, facing);
        float worldAngle = HolisticMath.Angle(vector, new Coords(0, 1, 0));
        bool clockwise = false;
        if (HolisticMath.Cross(vector, facing).z < 0)
            clockwise = true;

        vector = HolisticMath.Rotate(vector, angle + worldAngle, clockwise);

        float xVal = position.x + vector.x;
        float yVal = position.y + vector.y;
        float zVal = position.z + vector.z;
        return new Coords(xVal, yVal, zVal);
    }

    static public Coords Cross(Coords vector1, Coords vector2)
    {
        float xMult = vector1.y * vector2.z - vector1.z * vector2.y;
        float yMult = vector1.z * vector2.x - vector1.x * vector2.z;
        float zMult = vector1.x * vector2.y - vector1.y * vector2.x;
        Coords crossProd = new Coords(xMult, yMult, zMult);
        return crossProd;
    }
}
