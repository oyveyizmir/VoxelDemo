using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxelDemo
{
    public class VoxelModel
    {
        readonly int modelSize;
        readonly VoxelColor[,,] model;
        List<Voxel> voxels;

        Point3DInt[] surfaceSteps = new Point3DInt[] {
            new Point3DInt(0, 0, -1),
            new Point3DInt(1, 0, 0),
            new Point3DInt(0, 1, 0),
            new Point3DInt(-1, 0, 0),
            new Point3DInt(0, -1, 0),
            new Point3DInt(0, 0, 1),
        };

        public List<Voxel> Voxels=> voxels;

        public int ModelSize => modelSize;

        public VoxelModel(int modelSize)
        {
            this.modelSize = modelSize;
            model = new VoxelColor[modelSize, modelSize, modelSize];
        }

        public void CreateCuboid(int x, int y, int z, int sizeX, int sizeY, int sizeZ, VoxelColor color)
        {
            int maxX = x + sizeX;
            int maxY = y + sizeY;
            int maxZ = z + sizeZ;

            for (int k = z; k < maxZ; k++)
                for (int j = y; j < maxY; j++)
                    for (int i = x; i < maxX; i++)
                        model[i, j, k] = color;
        }

        public void CreateSphere(int x, int y, int z, int radius, VoxelColor color)
        {
            int minX = x - radius;
            int minY = y - radius;
            int minZ = z - radius;

            int maxX = x + radius;
            int maxY = y + radius;
            int maxZ = z + radius;

            for (int k = minZ; k <= maxZ; k++)
                for (int j = minY; j <= maxY; j++)
                    for (int i = minX; i <= maxX; i++)
                    {
                        int dx = i - x;
                        int dy = j - y;
                        int dz = k - z;
                        double r = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                        if (r <= radius)
                            model[i, j, k] = color;
                    }
        }

        public void PrepareModel()
        {
            voxels = new List<Voxel>();

            for (int k = 0; k < modelSize; k++)
                for (int j = 0; j < modelSize; j++)
                    for (int i = 0; i < modelSize; i++)
                        if (CanBeVisible(i, j, k))
                        {
                            var voxel = new Voxel { X = i, Y = j, Z = k, Color = model[i, j, k] };
                            voxels.Add(voxel);
                        }

            Debug.WriteLine($"Voxel count: {voxels.Count}");
        }

        bool CanBeVisible(int x, int y, int z)
        {
            if (model[x, y, z] == null)
                return false;

            foreach (Point3DInt step in surfaceSteps)
            {
                int newX = x + step.X;
                int newY = y + step.Y;
                int newZ = z + step.Z;
                if (model[newX, newY, newZ] == null)
                    return true;
            }

            return false;
        }
    }
}
