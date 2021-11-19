using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxelDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*var model = new VoxelModel(20);
            //model.CreateCuboid(2, 5, 7, 16, 10, 6, new VoxelColor(128, 200, 128));
            model.CreateSphere(9, 9, 9, 7, new VoxelColor(128, 200, 128));*/
            var model = new VoxelModel(100);
            //model.CreateSphere(50, 50, 50, 40, new VoxelColor(128, 200, 128));
            model.CreateSphere(40, 50, 50, 15, new VoxelColor(128, 200, 128));
            model.CreateSphere(60, 50, 50, 15, new VoxelColor(128, 200, 128));
            model.CreateCuboid(40, 40, 20, 20, 20, 60, new VoxelColor(128, 200, 128));
            model.PrepareModel();

            //var renderer = new VoxelRenderer(model, 30, 0.8, 500, 0.1);
            //var renderer = new VoxelRenderer(model, 100, 0.2, 500, 0.1);
            var renderer = new VoxelRenderer(model, 110, 1, 500, 0.1);

            var form = new MainForm();
            form.Renderer = renderer;
            Application.Run(form);
        }
    }
}
