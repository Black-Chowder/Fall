using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using New_Physics.Traits;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using New_Physics.Entities;
using Fall.src;
using Fall.src.Traits;


namespace Fall.src.Entities
{
    public static class BugSprites
    {
        public static Texture2D bug;
        public static Vector2 bugSize = new Vector2(13, 12);

        public static void LoadContent(ContentManager Content)
        {
            bug = Content.Load<Texture2D>("bug");
        }
    }
    public class Bug : Entity
    {
        //Variables
        Boolean isRight;
        Player player;

        //Constructor(s)
        public Bug(float x, float y) : base("bug", x, y)
        {
            this.width = 100;
            addTrait(new FallingCollision(this, false));
            isRight = new Random().NextDouble() < .5 ? true : false;

            //Find Player
            for (int i = 0; i < EntityHandler.entities.Count; i++)
            {
                if (EntityHandler.entities[i].classId == "player") player = (Player)EntityHandler.entities[i];
            }
        }


        //Update
        public override void Update()
        {
            if (y < player.y - Camera.Height * 2)
            {
                exists = false;
            }
        }

        //Draw
        [Obsolete]
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Draw Hitbox
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //TODO (maybe)
            float scale = 8 * Camera.gameScale;

            Rectangle DR = new Rectangle(
                (int)(x - Camera.X),
                (int)(y - BugSprites.bugSize.Y * scale / 2 - Camera.Y),
                (int)(BugSprites.bugSize.X * scale),
                (int)(BugSprites.bugSize.Y * scale));


            if (isRight) spriteBatch.Draw(BugSprites.bug,
                destinationRectangle: DR,
                color: Color.White);
            else spriteBatch.Draw(BugSprites.bug,
                destinationRectangle: DR,
                effects: SpriteEffects.FlipHorizontally,
                color: Color.White);

            //spriteBatch.Draw(texture, new Rectangle(
            //    (int)(x - Camera.X),
            //    (int)(y - Camera.Y),
            //    (int)(width),
            //    (int)(10)),
            //    Color.White);

            spriteBatch.End();
            texture.Dispose();
        }

        public void JumpedOn()
        {
            //Console.WriteLine("Bug was jumped on");
            for (int i = 0; i < EntityHandler.entities.Count; i++)
            {
                Entity entity = EntityHandler.entities[i];
                if (entity.classId == "gameMaster")
                {
                    ((GameMaster)entity).addScore();
                }
            }
            exists = false;

        }
    }
}
