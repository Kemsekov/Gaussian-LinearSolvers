using MathNet.Numerics.LinearAlgebra.Double;
using Solution;

namespace tests;
public class TriangularSolversTests
{
    [Fact]
    public void USolverWorks()
    {
        for (int i = 0; i < 10; i++)
        {
            var r = new Random();
            var size = r.Next(100) + 1;
            var u = DenseMatrix.Create(size, size, (i, k) => r.NextDouble()).UpperTriangle();
            var v = DenseVector.Create(size, i => r.NextDouble());

            var mult = r.NextDouble()*2-1;
            var expected = (mult * u).Solve(v);
            var actual = u.UForwardSubstitution(v, mult);
            var error = (1-expected/actual).Sum(Math.Abs);
            Assert.True(error < 1e-8);
        }
    }
    [Fact]
    public void LSolverWorks()
    {
        for (int i = 0; i < 10; i++)
        {
            var r = new Random();
            var size = r.Next(100) + 1;
            var l = DenseMatrix.Create(size, size, (i, k) => r.NextDouble()).LowerTriangle();
            var v = DenseVector.Create(size, i => r.NextDouble());

            var mult = 1;
            var expected = (mult * l).Solve(v);
            var actual = l.LForwardSubstitution(v, mult);
            var error = (1-expected/actual).Sum(Math.Abs);
            // I have no idea why it is not working. The implementation is exact same
            // as U forward substitution, but it just not works.
            Assert.True(error < 1e-8);
        }
    }
}
