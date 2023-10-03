using System.Diagnostics;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Solution;
GaussianSolver<double> solver1 = new();
GaussianZedelSolver<double> solver2 = new(0.00001);
using var matrixData = File.Open("matrix.txt", FileMode.Open, FileAccess.Read);
void Example()
{

    var matrixReader = new TextStreamMatrixReader<double>((i, j) => DenseMatrix.Create(i, j, 0), s => double.Parse(s));
    var m = matrixReader.Read(matrixData);
    System.Console.WriteLine(m);
    var res1 = solver1.Solve(m.SubMatrix(0, 3, 0, 3), m.Column(3));
    // System.Console.WriteLine("Gaussian solver\n" + res1);

    var I = m.Row(0);
    var II = m.Row(1);
    var III = m.Row(2);

    m.SetRow(0, I + III);
    m.SetRow(1, II - III);
    m.SetRow(2, III + 10 * (I * 2.1 - II * 1.7));

    System.Console.WriteLine(m);
    var res2 = solver2.Solve(m.SubMatrix(0, 3, 0, 3), m.Column(3));
    System.Console.WriteLine("Gaussian zedel solver\n" + res2);
}
void Task1()
{
    var m = DenseMatrix.Create(3, 3, (i, k) => i * 3 + k);
    m[0, 2] = 0;
    m[1, 2] = 0;
    m[2, 2] = 0;
    var v = DenseVector.Create(3, i => i);
    System.Console.WriteLine(m);
    System.Console.WriteLine(v);
    System.Console.WriteLine(solver1.Solve(m, v));
}
Example();