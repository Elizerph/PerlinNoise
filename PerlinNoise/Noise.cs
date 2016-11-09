using System;
using System.Collections.Generic;
using System.Linq;
using InterpolationLib;
using RandomExtension;

namespace PerlinNoise
{
    public static class Noise
    {
        public static Func<int, float> DefaultTopBound => d => 1;
        public static Func<int, float> DefaultBottomBound => d => -1;

        public static Func<int, float> TopNoiseBound { get; set; } = DefaultTopBound;
        public static Func<int, float> BottomNoiseBound { get; set; } = DefaultBottomBound;

        public static float[] Create1DNoise(int smoothness, 
            int nodesCount, 
            int segmentSize, 
            int seed = 0)
        {
            List<float[]> nodesLayers = new List<float[]>();
            for (int i = 0; i < smoothness; i++)
                nodesLayers.Add(RandomContainer.GetDoubleArray(BottomNoiseBound(i), TopNoiseBound(i), nodesCount, seed).Select(e => (float)e).ToArray());
            return SequenceInterpolation.MultiDerpSequenceByNodes(nodesLayers, segmentSize);
        }

        public static float[,] Create2DNoise(int smoothness, 
            int nodesWidthCount, 
            int nodesHeightCount, 
            int segmentSize, 
            int seed = 0)
        {
            List<float[,]> nodesLayers = new List<float[,]>();
            for (int i = 0; i < smoothness; i++)
            {
                float[,] layer = new float[nodesWidthCount, nodesHeightCount];
                for (int j = 0; j < nodesWidthCount; j++)
                    for (int k = 0; k < nodesHeightCount; k++)
                        layer[j, k] = (float)RandomContainer.GetDouble(BottomNoiseBound(i), TopNoiseBound(i), seed);
                nodesLayers.Add(layer);
            }
            return SurfaceInterpolation.MultiDerpByNodes(nodesLayers, segmentSize);
        }

        public static float[,,] Create3DNoise(int smoothness, 
            int nodesWidthCount, 
            int nodesHeightCount, 
            int nodesDepthCount, 
            int segmentSize, 
            int seed = 0)
        {
            List<float[,,]> nodesLayers = new List<float[,,]>();
            for (int i = 0; i < smoothness; i++)
            {
                float[,,] layer = new float[nodesWidthCount, nodesHeightCount, nodesDepthCount];
                for (int j = 0; j < nodesWidthCount; j++)
                    for (int k = 0; k < nodesHeightCount; k++)
                        for (int l = 0; l < nodesDepthCount; l++)
                        layer[j, k, l] = (float)RandomContainer.GetDouble(BottomNoiseBound(i), TopNoiseBound(i), seed);
                nodesLayers.Add(layer);
            }
            return VolumeInterpolation.MultiDerpByNodes(nodesLayers, segmentSize);
        }
    }
}
