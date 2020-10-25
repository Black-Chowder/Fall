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
using Fall.src.Traits;

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


        //Constructor(s)
        public Leaf(float x, float y) : base("leaf", x, y)
        {
            width = 100;
            addTrait(new FallingCollision(this, false));
            //Create a branch for itself here
            //TODO
            if (x > 0)
            {
                EntityHandler.entities.Add(new Branch(Camera.Width/2-200, y+75, 3, true));
            }
            else
            {
                EntityHandler.entities.Add(new Branch(-Camera.Width / 2 + 200, y+75, 3, false));
            }
        }


        //Update
        public override void Update()
        {

        }

        //Draw
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Draw Hitbox
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            float scale = 8 * Camera.gameScale;

            Rectangle DR = new Rectangle(
                (int)(x - Camera.X),
                (int)(y - Camera.Y),
                (int)(LeafSprites.leafSize.X * scale),
                (int)(LeafSprites.leafSize.Y * scale));

            spriteBatch.Draw(LeafSprites.leaf,
                destinationRectangle: DR,
                color: Color.White);

            spriteBatch.Draw(texture, new Rectangle(
                (int)(x - Camera.X),
                (int)(y - Camera.Y),
                (int)(width),
                (int)(10)),
                Color.White);

            spriteBatch.End();

            texture.Dispose();
        }
    }
}
