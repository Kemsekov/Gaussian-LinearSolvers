using System;
using System.Numerics;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Solution;
public class TextStreamMatrixReader<TFloat> : IMatrixStreamReader<TFloat>
where TFloat : unmanaged, INumber<TFloat>
{
    public TextStreamMatrixReader(Func<int, int, Matrix<TFloat>> emptyMatrixFactory, Func<string,TFloat> parser)
    {
        this.MatrixFactory = emptyMatrixFactory;
        this.Parse = parser;
    }

    public Func<int, int, Matrix<TFloat>> MatrixFactory { get; }
    public Func<string, TFloat> Parse { get; }
    public Matrix<TFloat> Read(string text){
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        return Read(stream);
    }
    public Matrix<TFloat> Read(Stream stream)
    {
        if (!stream.CanRead)
        {
            throw new ArgumentException("Cannot read from stream.");
        }
        var reader = new StreamReader(stream);
        var data = 
            (reader.ReadToEnd() ?? throw new ArgumentException("Stream does not contains matrix sizes in first line"))
            .Split(" ")
            .SelectMany(x=>x.Split("\n"))
            .SelectMany(x=>x.Split("\r"))
            .Where(x=>x.Any(char.IsDigit))
            .Select(Parse)
            .ToArray();
        if(data.Length<2)
            throw new ArgumentException("Stream must contain: rows columns <matrix data> --- where separators can be spaces or new lines");
        var rows = (int)(data[0] as dynamic);
        var cols = (int)(data[1] as dynamic);
        var mat = MatrixFactory(rows,cols);
        for(int i = 0;i<data.Length-2;i++){
            mat[i/cols,i%cols] = data[i+2];
        }
        return mat;
    }
}
