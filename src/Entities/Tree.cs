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
        Boolean isLeft;


        //Constructor(s)
        public Tree(float x, float y, Boolean isLeft = true) : base("tree", x, y)
        {
            width = 50;
            height = Camera.Height;
            this.isLeft = isLeft;
        }


        //Update
        public override void Update()
        {
            if (y - Camera.Y < 0)
            {
                EntityHandler.entities.Add(new Tree(0, y + Camera.Height, isLeft));
                exists = false;
            }
        }

        //Draw
        [Obsolete]
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Draw Hitbox
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //TODO (maybe)
            float scale = 8 * Camera.gameScale;

            Rectangle DR = new Rectangle(
                isLeft ? (int)(0) : (int)(Camera.Width-width),
                (int)(y - TreeSprites.treeSize.Y * scale - Camera.Y),
                (int)(TreeSprites.treeSize.X * scale),
                (int)(TreeSprites.treeSize.Y * scale));

            if (isLeft)
            {
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    color: Color.White);

                DR.Y = (int)(y - TreeSprites.treeSize.Y * scale + TreeSprites.treeSize.Y * scale - Camera.Y);
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    color: Color.White);

                DR.Y = (int)(y - TreeSprites.treeSize.Y * scale + 2 * TreeSprites.treeSize.Y * scale - Camera.Y);
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    color: Color.White);

                DR.Y = (int)(y - TreeSprites.treeSize.Y * scale - TreeSprites.treeSize.Y * scale - Camera.Y);
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    color: Color.White);
            }
            else
            {
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    effects: SpriteEffects.FlipHorizontally,
                    color: Color.White);

                DR.Y = (int)(y - TreeSprites.treeSize.Y * scale + TreeSprites.treeSize.Y * scale - Camera.Y);
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    effects: SpriteEffects.FlipHorizontally,
                    color: Color.White);

                DR.Y = (int)(y - TreeSprites.treeSize.Y * scale + 2 * TreeSprites.treeSize.Y * scale - Camera.Y);
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    effects: SpriteEffects.FlipHorizontally,
                    color: Color.White);

                DR.Y = (int)(y - TreeSprites.treeSize.Y * scale - TreeSprites.treeSize.Y * scale - Camera.Y);
                spriteBatch.Draw(TreeSprites.tree,
                    destinationRectangle: DR,
                    effects: SpriteEffects.FlipHorizontally,
                    color: Color.White);
            }


            spriteBatch.End();
        }
    }
}
