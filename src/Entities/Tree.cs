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
    public static class TreeSprites
    {
        public static Texture2D tree;
        public static Vector2 treeSize = new Vector2(8, 100);

        public static void LoadContent(ContentManager Content)
        {
            tree = Content.Load<Texture2D>("tree");
        }
    }
    public class Tree : Entity
    {
        //Variables
        float width;


        //Constructor(s)
        public Tree(float x, float y) : base("tree", x, y)
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
            float scale = 10 * Camera.gameScale;

            Rectangle DR = new Rectangle(
                (int)(0),
                (int)(y - TreeSprites.treeSize.Y * scale / 2 - Camera.Y),
                (int)(TreeSprites.treeSize.X * scale),
                (int)(TreeSprites.treeSize.Y * scale));

            spriteBatch.Draw(TreeSprites.tree,
                destinationRectangle: DR,
                color: Color.White);

            spriteBatch.End();
        }
    }
}
