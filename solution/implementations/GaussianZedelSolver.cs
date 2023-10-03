using MathNet.Numerics.LinearAlgebra;
using Solution.Exceptions;

namespace Solution;

public class GaussianZedelSolver<TFloat> : ISolver<TFloat>
where TFloat : struct, System.Numerics.INumber<TFloat>
{
    public TFloat ConvergenceEpsilon { get; }
    /// <summary>
    /// Count of iterations algorithm took to converge result with given precision
    /// </summary>
    public int Iterations{get;protected set;}

    /// <param name="convergenceEpsilon">Convergence epsilon, if values changes less than this value, iteration process stops</param>
    public GaussianZedelSolver(TFloat? convergenceEpsilon = null){
        ConvergenceEpsilon = convergenceEpsilon ?? (TFloat)(0.0001 as dynamic);
    }
    /// <returns>True if matrix have diagonal dominance</returns>
    bool DiagonalDominance(Matrix<TFloat> m){
        for(int i = 0;i<m.RowCount;i++){
            var diagonalSum = m.Row(i).PointwiseAbs().Sum()-m[i,i];
            if(diagonalSum>m[i,i])
                return false;
        }
        return true;
    }
    bool Converges(Vector<TFloat> v1,Vector<TFloat> v2){
        var change = (TFloat)((v1/v2-TFloat.One).L2Norm() as dynamic);
        return change<ConvergenceEpsilon;
    }
    public Vector<TFloat> Solve(Matrix<TFloat> m, Vector<TFloat> coefficients)
    {
        Utils.ThrowIfNotLinearSolvable(m,coefficients);
        if(!DiagonalDominance(m))
            throw new DiagonalMatrixException("Matrix m must be diagonal dominant");
        Iterations = 0;   
        var solution = coefficients.Map(x=>x);
        var prevSolution = coefficients.Map(x=>x);

        do{
            prevSolution = solution;
            solution = m.ForwardSubstitution(coefficients,prevSolution);
            Iterations++;
        }
        while(!Converges(solution,prevSolution));
        return solution;
    }
}
