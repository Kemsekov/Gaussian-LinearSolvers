using MathNet.Numerics.LinearAlgebra.Double;
using Solution;

namespace tests;

public class TextStreamMatrixReaderTests{
    [Fact]
    public void LoadsData(){
        var reader = new TextStreamMatrixReader<double>((i,j)=>DenseMatrix.Create(i,j,0),s=>double.Parse(s));
        var m = reader.Read(
            """
            4 6
            1 2 3 4
            5 6 7 8
            9 10 11 12
            13 14 15 16
            17 18 19 20
            21 22 23 24
            """
        );
        Assert.Equal(4,m.RowCount);
        Assert.Equal(6,m.ColumnCount);

        for(int i = 0;i<m.RowCount;i++){
            for(int j = 0;j<m.ColumnCount;j++){
                Assert.Equal(i*m.ColumnCount+j,m[i,j]-1);
            }
        }
    }
}
