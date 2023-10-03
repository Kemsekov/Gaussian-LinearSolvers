using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Solution;

namespace tests;
public class GaussianSolverTests
{
    [Fact]
    public void Solve()
    {
        for (int i = 0; i < 10; i++)
        {
            var size = Random.Shared.Next(100) + 1;
            var mat = DenseMatrix.Create(size, size, (_, _) => Random.Shared.Next(100) - 50);
            var vec = DenseVector.Create(size, Random.Shared.Next(100));
            var expected = mat.Solve(vec);
            var actual = new GaussianSolver<double>().Solve(mat, vec);
            var error = (1 - expected / actual).Sum(Math.Abs);
            Assert.True(error < 1e-8);
        }
    }
}