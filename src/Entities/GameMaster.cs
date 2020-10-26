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
        Random rand = new Random();
        float gate;

        public GameMaster() : base("gameMaster", 0, 0)
        {
            gate = Camera.Height;
            for (int i = 0; i < EntityHandler.entities.Count; i++)
            {
                Entity entity = EntityHandler.entities[i];
                if (entity.classId == "player")
                {
                    player = (Player)entity;
                }
            }

            //Initial Generation
            //Section 1
            EntityHandler.entities.Add(
                new Leaf(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next(0, Camera.Height)));
            EntityHandler.entities.Add(
                new Branch(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next(0, Camera.Height), 
                rand.NextDouble() > .5 ? 0 : 1, rand.NextDouble() > .5 ? false : true));

            //Section 2
            EntityHandler.entities.Add(
                new Leaf(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next(Camera.Height, 2*Camera.Height)));
            //EntityHandler.entities.Add(
            //    new Branch(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next(Camera.Height, 2*Camera.Height),
            //    rand.NextDouble() > .5 ? 0 : 1, rand.NextDouble() > .5 ? false : true));


        }

        public override void Update()
        {
            //TRACK SCORE:
            if (player.y > Score)
            {
                Score = (int)player.y;
            }

            //GENERATE LEVEL:
            if (player.y > gate)
            {
                //Console.WriteLine("Passed Gate: player.y = " + player.y + " gate = " + gate);   
                gate = gate + Camera.Height;
                EntityHandler.entities.Add(
                    new Leaf(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next((int)(player.y + Camera.Height), (int)(player.y + 2 * Camera.Height))));
                EntityHandler.entities.Add(
                    new Branch(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next((int)(player.y + Camera.Height), (int)(player.y + 2 * Camera.Height)),
                    rand.NextDouble() > .5 ? 0 : 1, rand.NextDouble() > .5 ? false : true));
                EntityHandler.entities.Add(
                    new Bug(rand.Next(-Camera.Width / 2, Camera.Width / 2), rand.Next((int)(player.y + Camera.Height), (int)(player.y + 2 * Camera.Height))));
            
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //spriteBatch.Draw(texture, new Rectangle(0, (int)gate, Camera.Width, 10), Color.White);
            spriteBatch.End();
            texture.Dispose();
            //TODO: Display Score
        }

        public void addScore()
        {
            Score += 1;
        }
    }
}
