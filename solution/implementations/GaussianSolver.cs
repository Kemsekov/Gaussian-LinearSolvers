using System;
using System.Data;
using MathNet.Numerics.LinearAlgebra;

namespace Solution;

public class GaussianSolver<TFloat> : ISolver<TFloat>
where TFloat : struct, System.Numerics.INumber<TFloat>
{
    /// <summary>
    /// Does gaussian elimination on matrix and it's vector part
    /// </summary>
    /// <param name="row">Where to start elimination</param>
    /// <param name="column">Where to start elimination</param>
    void Elimination(Matrix<TFloat> m, Vector<TFloat> v,int row = 0,int column = 0){
        if(m[row,column]==TFloat.Zero) return;
        if(row>=m.RowCount || column>=m.ColumnCount) return;
        for(int i = row+1;i<m.RowCount;i++){
            var coeff = m[i,column]/m[row,column];
            v[i]-=coeff*v[column];
            for(int k = column;k<m.ColumnCount;k++){
                m[i,k]-=coeff*m[row,k];
            }
        }
    }
    public Vector<TFloat> Solve(Matrix<TFloat> m, Vector<TFloat> coefficients)
    {
        Utils.ThrowIfNotLinearSolvable(m,coefficients);
        m = m.Clone();
        coefficients = coefficients.Clone();

        int[] pivots = Enumerable.Range(0,coefficients.Count).ToArray();

        int row = 0;
        int column = 0;
        for(int i = 0;i<Math.Min(m.ColumnCount,m.RowCount);i++){
            SwapPivot(m,coefficients,pivots, row+i,column+i);
            Elimination(m,coefficients,row+i,column+i);
        }
        return m.UForwardSubstitution(coefficients);
    }

   

    /// <summary>
    /// Chooses biggest elements on selected row from given column, and swaps it, storing new position into pivot
    /// </summary>
    private void SwapPivot(Matrix<TFloat> m, Vector<TFloat> v, int[] pivots, int row, int column)
    {
        TFloat maxValue = TFloat.Zero;
        int maxElementRow = row;
        for(int i = row;i<m.RowCount;i++){
            var element = TFloat.Abs(m[i,column]);
            if(element>maxValue){
                maxValue = element;
                maxElementRow = i; 
            }
        }
        (pivots[row],pivots[maxElementRow]) = (pivots[maxElementRow],pivots[row]);
        (v[row],v[maxElementRow]) = (v[maxElementRow],v[row]);

        for(int k = column;k<m.ColumnCount;k++){
            (m[row,k],m[maxElementRow,k])=(m[maxElementRow,k],m[row,k]);
        }
        
    }
}
