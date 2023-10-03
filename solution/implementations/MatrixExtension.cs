using System;
using MathNet.Numerics.LinearAlgebra;

namespace Solution;
public static class MatrixExtensions
{
    /// <summary>
    /// Makes forward substitution for matrix of previous solution, and returns next solution.<br/>
    /// Basically it finds x for equation: m*x+coefficients=previousSolution
    /// </summary>
    /// <param name="m">Matrix</param>
    /// <param name="coefficients">coefficients</param>
    /// <param name="matrixMultiplier">How values of matrix should be treated. It is useful for case when you need to do forward substitution of -U matrix for some vector X, just pass here value -1 and do not create any new matrices!</param>
    public static Vector<TFloat> ForwardSubstitution<TFloat>(this Matrix<TFloat> m, Vector<TFloat> coefficients,Vector<TFloat> previousSolution, TFloat? matrixMultiplier = null)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        var mul = matrixMultiplier ?? TFloat.One;
        var result = previousSolution.Map(x=>x);
        for (int i = 0; i <m.RowCount; i++)
        {
            TFloat accumulator = TFloat.Zero;
            for (int c = 0; c < m.ColumnCount; c++)
            {
                accumulator += result[c] * m[i, c];
            }
            accumulator -= result[i] * m[i, i];

            result[i] = (coefficients[i]/mul - accumulator) / m[i, i];
        }
        return result;
    }
    /// <summary>
    /// Makes forward substitution for upper triangular matrix.
    /// </summary>
    /// <param name="m">Upper triangular matrix</param>
    /// <param name="coefficients">coefficients</param>
    /// <param name="matrixMultiplier">How values of matrix should be treated. It is useful for case when you need to do forward substitution of -U matrix for some vector X, just pass here value -1 and do not create any new matrices!</param>
    public static Vector<TFloat> UForwardSubstitution<TFloat>(this Matrix<TFloat> m, Vector<TFloat> coefficients, TFloat? matrixMultiplier = null)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        var mul = matrixMultiplier ?? TFloat.One;
        var result = coefficients.Map(x => TFloat.Zero);
        for (int i = m.RowCount - 1; i >= 0; i--)
        {
            TFloat accumulator = TFloat.Zero;
            for (int c = m.ColumnCount - 1; c > i; c--)
            {
                accumulator += result[c] * m[i, c];
            }
            result[i] = (coefficients[i]/mul - accumulator) / m[i, i];
        }
        return result;
    }
    /// <summary>
    /// Makes forward substitution for lower triangular matrix.
    /// </summary>
    ///<inheritdoc cref="UForwardSubstitution"/>
    public static Vector<TFloat> LForwardSubstitution<TFloat>(this Matrix<TFloat> m, Vector<TFloat> coefficients,TFloat? matrixMultiplier = null)
    where TFloat : struct, System.Numerics.INumber<TFloat>
    {
        var mul = matrixMultiplier ?? TFloat.One;
        var result = coefficients.Map(x => TFloat.Zero);
        for (int i = 0; i <m.RowCount; i++)
        {
            TFloat accumulator = TFloat.Zero;
            for (int c = 0; c < i; c++)
            {
                accumulator += result[c] * m[i, c];
            }
            result[i] = (coefficients[i]/mul - accumulator) / m[i, i];
        }
        return result;
    }
}
