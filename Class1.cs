using System;
using System.Text.RegularExpressions;

namespace MLassignment1Csharp
{
    public class Person
    {
        public int[] xCategorical;
        public int[] xBinary;
        public double[] xReal;
        public int y;
        // (check size with .length [no ()], or iterate the usual way)

        public void fromString(String text)
        {
            String[] parts = Regex.Split(text, " ");
            xCategorical = new int[4];
            xBinary = new int[4];
            xReal = new double[6];

            xBinary[0] = int.Parse(parts[0]);
            xReal[0] = Double.Parse(parts[1]);
            xReal[1] = Double.Parse(parts[2]);
            xCategorical[0] = int.Parse(parts[3]);
            xCategorical[1] = int.Parse(parts[4]);
            xCategorical[2] = int.Parse(parts[5]);
            xReal[2] = Double.Parse(parts[6]);
            xBinary[1] = int.Parse(parts[7]);
            xBinary[2] = int.Parse(parts[8]);
            xReal[3] = Double.Parse(parts[9]);
            xBinary[3] = int.Parse(parts[10]);
            xCategorical[3] = int.Parse(parts[11]);
            xReal[4] = Double.Parse(parts[12]);
            xReal[5] = Double.Parse(parts[13]);
            y = int.Parse(parts[14]);
        }
        public String toString()
        {
            String personInfo = "x = (" +
                xBinary[0] + "; " +
                xReal[0] + "; " +
                xReal[1] + "; " +
                xCategorical[0] + "; " +
                xCategorical[1] + "; " +
                xCategorical[2] + "; " +
                xReal[2] + "; " +
                xBinary[1] + "; " +
                xBinary[2] + "; " +
                xReal[3] + "; " +
                xBinary[3] + "; " +
                xCategorical[3] + "; " +
                xReal[4] + "; " +
                xReal[5]
                + "); y = " + y;
            return personInfo;
        }
    }

}