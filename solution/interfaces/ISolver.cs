using MathNet.Numerics.LinearAlgebra;
namespace Solution;
public interface ISolver<TFloat>
where TFloat : struct,System.Numerics.INumber<TFloat>
{
    Vector<TFloat> Solve(Matrix<TFloat> m, Vector<TFloat> coefficients);    
}