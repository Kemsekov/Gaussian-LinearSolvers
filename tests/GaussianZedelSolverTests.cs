using MathNet.Numerics.LinearAlgebra.Double;
using Solution;
using Solution.Exceptions;

namespace tests;

public class GaussianZedelSolverTests
{
    [Fact]
    public void Solve_ThrowsWhenNotDiagonalDominant()
    {
        for (int c = 0; c < 10; c++)
        {
            var size = Random.Shared.Next(100) + 2;
            var m = DenseMatrix.Create(size, size, (i, j) => Random.Shared.NextDouble());
            var v = DenseVector.Create(size, 1);

            var i = Random.Shared.Next(size);
            var j = Random.Shared.Next(size);

            m[i, j] = m.Row(i).PointwiseAbs().Sum() / 2;

            Assert.Throws<DiagonalMatrixException>(() => new GaussianZedelSolver<double>().Solve(m, v));
        }
    }
    [Fact]
    public void Solve_Works()
    {
        for (int c = 0; c < 10; c++)
        {
            var size = Random.Shared.Next(100) + 2;
            var m = DenseMatrix.Create(size, size, (i, j) => Random.Shared.NextDouble());
            var v = DenseVector.Create(size, _ => Random.Shared.NextDouble());

            for (int i = 0; i < size; i++)
            {
                var rowSum = m.Row(i).PointwiseAbs().Sum();
                m[i, i] = rowSum * 2;
            }
            var solver = new GaussianZedelSolver<double>(0.00001);
            var expected = m.Solve(v);
            var actual = solver.Solve(m, v);

            var diff = (1 - expected / actual).PointwiseAbs().Sum();
            Assert.True(diff < 0.00001);
        }
    }
}
