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

namespace Fall.src.Entities
{
    public static class LeafSprites
    {
        public static Texture2D leaf;
        public static Vector2 leafSize = new Vector2(14, 19);

        public static void LoadContent(ContentManager Content)
        {
            leaf = Content.Load<Texture2D>("leaf");
        }
    }
    public class Leaf : Entity
    {
        //Variables
        float width;


        //Constructor(s)
        public Leaf(float x, float y) : base("leaf", x, y)
        {
            this.width = 50;
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
            float scale = 4 * Camera.gameScale;

            Rectangle DR = new Rectangle(
                (int)(x - LeafSprites.leafSize.X * scale / 2 - Camera.X),
                (int)(y - LeafSprites.leafSize.Y * scale / 2 - Camera.Y),
                (int)(LeafSprites.leafSize.X * scale),
                (int)(LeafSprites.leafSize.Y * scale));

            spriteBatch.Draw(LeafSprites.leaf,
                destinationRectangle: DR,
                color: Color.White);

            spriteBatch.End();
        }
    }
}
