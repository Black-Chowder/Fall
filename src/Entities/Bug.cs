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


        //Constructor(s)
        public Bug(float x, float y) : base("bug", x, y)
        {
            this.width = 50;
            addTrait(new FallingCollision(this, false));
        }


        //Update
        public override void Update()
        {

        }

        //Draw
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Draw Hitbox
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //TODO (maybe)
            float scale = 8 * Camera.gameScale;

            Rectangle DR = new Rectangle(
                (int)(x - BugSprites.bugSize.X * scale / 2 - Camera.X),
                (int)(y - BugSprites.bugSize.Y * scale / 2 - Camera.Y),
                (int)(BugSprites.bugSize.X * scale),
                (int)(BugSprites.bugSize.Y * scale));

            spriteBatch.Draw(BugSprites.bug,
                destinationRectangle: DR,
                color: Color.White);

            spriteBatch.End();
        }

        public void JumpedOn()
        {
            Console.WriteLine("Bug was jumped on");
            exists = false;
        }
    }
}
