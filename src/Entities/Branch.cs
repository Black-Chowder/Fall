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
    public static class BranchSprites
    {
        public static Texture2D branch;
        public static Vector2 branchSize = new Vector2(60, 18);
        public static Rectangle[] branchSprites;

        public static void LoadContent(ContentManager Content)
        {
            branch = Content.Load<Texture2D>("branch");
            branchSprites = Utils.spriteSheetLoader((int)(branchSize.X), (int)(branchSize.Y), 4, 1);
        }
    }
    public class Branch : Entity
    {
        //Variables
        float width;
        int type;
        Boolean isOnRight;

        //Constructor(s)
        public Branch(float x, float y, int type, Boolean isOnRight) : base("bug", x, y)
        {
            this.width = 50;
            this.type = type;
            this.isOnRight = isOnRight;
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
                (int)(x - BranchSprites.branchSize.X * scale / 2 - Camera.X),
                (int)(y - BranchSprites.branchSize.Y * scale / 2 - Camera.Y),
                (int)(BranchSprites.branchSize.X * scale),
                (int)(BranchSprites.branchSize.Y * scale));

            spriteBatch.Draw(BranchSprites.branch,
                destinationRectangle: DR,
                sourceRectangle: BranchSprites.branchSprites[0],
                color: Color.White);

            spriteBatch.End();
        }
    }
}
