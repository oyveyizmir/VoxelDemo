using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxelDemo
{
    public class VoxelRenderer
    {
        VoxelModel model;
        double cameraDistance;
        double viewSize;
        double screenDistance;
        double rotationStep;
        double angleX = 0;
        double angleY = 0;
        double angleZ = 0;
        double viewCenter;
        double modelCenter;
        double voxelSize;
        

        public VoxelRenderer(VoxelModel model, double cameraDistance, double viewAngle, double viewSize, double rotationStep)
        {
            this.model = model;
            this.cameraDistance = cameraDistance;
            this.viewSize = viewSize;
            this.rotationStep = rotationStep;

            screenDistance = viewSize / 2 / Math.Tan(viewAngle / 2);
            viewCenter = viewSize / 2;
            modelCenter = (model.ModelSize - 1) / 2.0;
            voxelSize = screenDistance / cameraDistance / 3;
        }

        public void Render(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, 0, 0, (float)viewSize, (float)viewSize);

            foreach (var voxel in model.Voxels)
            {
                Point3D point = ToSceneCoordinates(voxel);
                point = Rotate(point);
                double k = screenDistance / (cameraDistance - point.Z);
                double x = point.X * k;
                double y = point.Y * k;
                double distance = GetCameraDistance(point);
                double size = cameraDistance / distance;
                Draw(graphics, x, y, size);
            }
        }

        Point3D ToSceneCoordinates(Voxel voxel)
        {
            return new Point3D()
            {
                X = voxel.X - modelCenter,
                Y = voxel.Y - modelCenter,
                Z = voxel.Z - modelCenter,
            };
        }

        Point3D Rotate(Point3D p)
        {
            double cosA = Math.Cos(angleZ);
            double sinA = Math.Sin(angleZ);
            double cosB = Math.Cos(angleY);
            double sinB = Math.Sin(angleY);
            double cosG = Math.Cos(angleX);
            double sinG = Math.Sin(angleX);

            return new Point3D()
            {
                X = p.X * (cosA * cosB) + p.Y * (cosA * sinB * sinG - sinA * cosG) + p.Z * (cosA * sinB * cosG + sinA * sinG),
                Y = p.X * (sinA * cosB) + p.Y * (sinA * sinB * sinG + cosA * cosG) + p.Z * (sinA * sinB * cosG - cosA * sinG),
                Z = p.X * (-sinB) + p.Y * (cosB * sinB) + p.Z * (cosB * cosG)
            };
        }

        double GetCameraDistance(Point3D point)
        {
            double dz = cameraDistance - point.Z;
            return Math.Sqrt(dz * dz + point.X * point.X + point.Y * point.Y);
        }

        void Draw(Graphics graphics, double x, double y, double size)
        {
            //DrawDot(graphics, x, y, size);
            DrawRectangle(graphics, x, y, size);
        }

        void DrawDot(Graphics graphics, double x, double y, double size)
        {
            graphics.DrawRectangle(Pens.Green, (float)(viewCenter + x), (float)(viewCenter + y), 1, 1);
        }

        void DrawRectangle(Graphics graphics, double x, double y, double size)
        {
            size *= voxelSize;
            double halfSize = size / 2;
            graphics.FillRectangle(Brushes.Green, (float)(viewCenter + x - halfSize), (float)(viewCenter + y - halfSize), (float)size, (float)size);
        }

        public void RotateX(bool counterClockwize)
        {
            angleX += counterClockwize ? rotationStep : -rotationStep;
        }

        public void RotateY(bool counterClockwize)
        {
            angleY += counterClockwize ? rotationStep : -rotationStep;
        }

        public void RotateZ(bool counterClockwize)
        {
            angleZ += counterClockwize ? rotationStep : -rotationStep;
        }
    }
}
