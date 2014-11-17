using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Fractals
{
    public class Fractal 
    {

        private uint N_COLOURS = 256;
        private const int MAGNITUDE_CUTOFF = 200;


        private Color[] m_colours;

        private double m_realAxisStart;
        private double m_realAxisEnd;

        private double m_imaginaryAxisStart;
        private double m_imaginaryAxisEnd;

        private Size m_size;


        private double m_widthFactor;
        private double m_heightFactor;

        public Fractal()
        {
            Array knownColors = Enum.GetValues(typeof(System.Drawing.KnownColor));

            uint nColour = 0;

            N_COLOURS = (uint)knownColors.Length;
            m_colours = new Color[N_COLOURS];

            foreach (KnownColor k in knownColors)
            {
                m_colours[nColour++] = System.Drawing.Color.FromKnownColor(k);
            }

        }


        public Bitmap RenderFractal(double realAxisStart, double realAxisEnd,
                                    double imaginaryAxisStart, double imaginaryAxisEnd,
                                    Size renderedSize)
        {

            m_realAxisStart = realAxisStart;
            m_realAxisEnd = realAxisEnd;

            m_imaginaryAxisStart = imaginaryAxisStart;
            m_imaginaryAxisEnd = imaginaryAxisEnd;

            m_size = renderedSize;

            m_widthFactor = (m_realAxisEnd - m_realAxisStart) / m_size.Width;
            m_heightFactor = (m_imaginaryAxisEnd - m_imaginaryAxisStart) / m_size.Height;


            Bitmap bitmap = new Bitmap(m_size.Width, m_size.Height);

            int nCols = bitmap.Palette.Entries.Length;

            for (int y = 0; y < m_size.Height; y++)
            {
                for (int x = 0; x < m_size.Width; x++)
                {
                    uint pixelValue = GeneratePixel(x, y, N_COLOURS);


                    bitmap.SetPixel(x, y, m_colours[pixelValue]);

                }
            }

            return bitmap;
        }


        private uint GeneratePixel(int x, int y, uint nColours)
        {
            double p, q;
            double r, i, prev_r, prev_i;
            uint n;

            p = (((double)x) * m_widthFactor) + m_realAxisStart;
            q = (((double)y) * m_heightFactor) + m_imaginaryAxisStart;

            prev_i = prev_r = 0;
            for (n = 0; n < nColours; n++)
            {
                r = prev_r * prev_r - prev_i * prev_i + p;
                i = 2 * prev_r * prev_i + q;
                if ((r * r + i * i) < MAGNITUDE_CUTOFF)
                {
                    prev_r = r;
                    prev_i = i;
                }
                else
                {
                    return n;
                }
            }
            return (uint)nColours - 1;

        }
    }
}
