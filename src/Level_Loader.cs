using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using New_Physics.Entities;
using Fall.src;
using Fall.src.Entities;

namespace Frogs.src
{
    public static class Level_Loader
    {
        static TextReader tr = new StreamReader(@"Levels.txt");
        static string rawMapData = tr.ReadLine();

        public static void LoadLevel()
        {
            //Clear Entities
            EntityHandler.entities.Clear();

            EntityHandler.entities.Add(new Player(0, Camera.Width/2));

        }
    }
}
