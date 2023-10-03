using System;
using MathNet.Numerics.LinearAlgebra;

namespace Solution;
public static class Utils
{
    public static void ThrowIfNotLinearSolvable<TFloat>(Matrix<TFloat> m, Vector<TFloat> coefficients)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        ThrowIfEmptyMatrix(m);
        ThrowIfNotSquare(m);
        ThrowIfNotMatch(m,coefficients);
    }
    
    public static void ThrowIfNotMatch<TFloat>(Matrix<TFloat> m, Vector<TFloat> coefficients)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        if (m.RowCount != coefficients.Count)
        {
            throw new ArgumentException("Matrix sizes does not match coefficients size");
        }
    }

    public static void ThrowIfNotSquare<TFloat>(Matrix<TFloat> m)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        if(m.RowCount!=m.ColumnCount){
            throw new ArgumentException("Matrix should be square");
        }
    }

    public static void ThrowIfEmptyMatrix<TFloat>(Matrix<TFloat> m)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        if(m.RowCount<2 || m.ColumnCount<2){
            throw new ArgumentException("Matrix should be at least 2 x 2");
        }
    }
}
