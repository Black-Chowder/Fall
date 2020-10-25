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
        //static TextReader tr = new StreamReader(@"Levels.txt");
        //static string rawMapData = tr.ReadLine();

        public static void LoadLevel()
        {
            //Clear Entities
            EntityHandler.entities.Clear();

            EntityHandler.entities.Add(new Player(25, 0));
            EntityHandler.entities.Add(new Platform(-200, 400, 500, 100));

            //entities.Add(new Leaf(100, 0));
            //EntityHandler.entities.Add(new Branch(200, 300, 0, false));
            EntityHandler.entities.Add(new Bug(0, 100));
            EntityHandler.entities.Add(new Tree(0, 0));
            EntityHandler.entities.Add(new Tree(0, 0, false));
            EntityHandler.entities.Add(new Leaf(200, 200));

            EntityHandler.entities.Add(new Branch(0, 500, 1, true));


            EntityHandler.entities.Add(new GameMaster());
        }
    }
}
