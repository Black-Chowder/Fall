using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_Physics.Entities;
using Fall.src.Traits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fall.src.Entities
{
    public class GameMaster : Entity
    { 
        public int Score = 0;
        Player player;
        int shouldGenerateCounter = 0;

        public GameMaster() : base("gameMaster", 0, 0)
        {
            for (int i = 0; i < EntityHandler.entities.Count; i++)
            {
                Entity entity = EntityHandler.entities[i];
                if (entity.classId == "player")
                {
                    player = (Player)entity;
                }
            }
        }

        public override void Update()
        {
            var rand = new Random();
            //TRACK SCORE:
            if (player.y > -Score)
            {
                Score = (int)player.y;
                shouldGenerateCounter += 1;
            }

            //GENERATE LEVEL:
            //Console.WriteLine(shouldGenerateCounter);
            if (shouldGenerateCounter == 25)
            {
                EntityHandler.entities.Add(
                    new Leaf(rand.Next(-Camera.Width/2, Camera.Width/2), rand.Next((int)(player.y + Camera.Height), (int)(player.y + 2 * Camera.Height))));
                //Console.WriteLine("Newly Generated");
                shouldGenerateCounter = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //TODO: Display Score
        }
    }
}
