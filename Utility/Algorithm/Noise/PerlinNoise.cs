using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Utility.Algorithm.Noise
{
    public class PerlinNoise
    {
        private double[,] noiseValues;
        private float amplitude = 1;    // Max amplitude of the function
        private int frequency = 1;      // Frequency of the function

        public float Amplitude { get { return amplitude; } }
        public int Frequency { get { return frequency; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        public PerlinNoise(int freq, float _amp) : this(freq,_amp,System.Environment.TickCount)
        {
        }

        public PerlinNoise(int freq, float _amp, int _Seed)
        {
            System.Random rand = new System.Random(_Seed);
            noiseValues = new double[freq, freq];
            amplitude = _amp;
            frequency = freq;

            // Generate our noise values
            for (int i = 0; i < freq; i++)
            {
                for (int k = 0; k < freq; k++)
                {
                    noiseValues[i, k] = rand.NextDouble();
                }
            }
        }

        /// <summary>
        /// Get the interpolated point from the noise graph using cosine interpolation
        /// </summary>
        /// <returns></returns>
        public double getInterpolatedPoint(int _xa, int _xb, int _ya, int _yb, double x, double y)
        {
            double i1 = interpolateCosinus(
                noiseValues[_xa % Frequency, _ya % frequency],
                noiseValues[_xb % Frequency, _ya % frequency]
                , x);

            double i2 = interpolateCosinus(
                noiseValues[_xa % Frequency, _yb % frequency],
                noiseValues[_xb % Frequency, _yb % frequency]
                , x);

            return interpolateCosinus(i1, i2, y);
        }

        /// <summary>
        /// Get the interpolated point from the noise graph using cosine interpolation
        /// </summary>
        /// <returns></returns>
        private double interpolateCosinus(double a, double b, double x)
        {
            double ft = x * Math.PI;
            double f = (1 - Math.Cos(ft)) * .5;

            // Returns a Y value between 0 and 1
            return a * (1 - f) + b * f;
        }

        public static double[,] getHeightMap(Vector3 _Position, Vector3 _Size, int _MaxDistance, int _Seed)
        {
            // Erstelle mit geringer frequenz großse gleiche gebiete / kurven. z.b. Ozean gebiet oder Land gebeit ... mit großen freuzeuen die berge am besten :)
            List<Utility.Algorithm.Noise.PerlinNoise> var_Perlins = new List<Utility.Algorithm.Noise.PerlinNoise>();
            var_Perlins.Add(new Utility.Algorithm.Noise.PerlinNoise(50, 20, _Seed));
            var_Perlins.Add(new Utility.Algorithm.Noise.PerlinNoise(70, 20, _Seed));
            var_Perlins.Add(new Utility.Algorithm.Noise.PerlinNoise(100, 20, _Seed));
            var_Perlins.Add(new Utility.Algorithm.Noise.PerlinNoise(30, 20, _Seed));
            return getHeightMap(_Position, _Size, _MaxDistance, var_Perlins);
        }

        public static double[,] getHeightMap(Vector3 _Position, Vector3 _Size, int _MaxDistance, List<PerlinNoise> noiseFunctions)
        {
            double[,] summedValues = new double[(int)_Size.X, (int)_Size.Y];

            // Sum each of the noise functions
            for (int i = 0; i < noiseFunctions.Count; i++)
            {
                double x_step = (float)_MaxDistance / (float)noiseFunctions[i].Frequency;
                double y_step = (float)_MaxDistance / (float)noiseFunctions[i].Frequency;

                for (int x = 0; x < (int)(_Size.X); x++)
                {
                    for (int y = 0; y < (int)(_Size.Y); y++)
                    {
                        int var_X = x + (int)_Position.X;
                        int var_Y = y + (int)_Position.Y;

                        int a = (int)(var_X / x_step);
                        int b = a + 1;
                        int c = (int)(var_Y / y_step);
                        int d = c + 1;

                        double intpl_val = noiseFunctions[i].getInterpolatedPoint(a, b, c, d, (var_X / x_step) - a, (var_Y / y_step) - c);
                        summedValues[x, y] += intpl_val * noiseFunctions[i].Amplitude;
                    }
                }
            }
            return summedValues;
        }
    }
}
