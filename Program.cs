using System;
using System.IO;
using System.Text;

namespace Terain3D
{
  public static class Helpers
  {
    public static byte[] Reverse(this byte[] b)
    {
      Array.Reverse(b);
      return b;
    }

    public static Int16 ReadInt16BE(this BinaryReader binRdr)
    {
      return BitConverter.ToInt16(binRdr.ReadBytes(sizeof(Int16)).Reverse(), 0);
    }

    public static int getElement(int[] data, int elementsInRow, int[] coords)
    {
      return data[(coords[1] * elementsInRow) + coords[1]];
    }

    public static Tuple<int[], int> getData(string filePath)
    {
      Console.WriteLine("{0}", filePath);

      using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      {
        int fsLength = (int)new FileInfo(filePath).Length;
        int[] heights = new int[fsLength / 2];
        int totalRows = (int)Math.Sqrt(fsLength / 2);

        BinaryReader reader = new BinaryReader(fs, Encoding.BigEndianUnicode);
        for (int i = 0; i < fsLength / 2; i++)
        {
          heights[i] = ReadInt16BE(reader);
        }

        return Tuple.Create(heights, totalRows);
      }
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      string hgtPath = "N50E017.hgt";
      Tuple<int[], int> dataHgt = Helpers.getData(hgtPath);
      Console.WriteLine("x:21, y:37, z:{0}", Helpers.getElement(dataHgt.Item1, dataHgt.Item2, new int[] { 21, 37 }));
    }
  }
}