using System;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Solution;
public interface IMatrixStreamReader<TFloat>
where TFloat : unmanaged, INumber<TFloat>
{
    Matrix<TFloat> Read(Stream stream);
}
