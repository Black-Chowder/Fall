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
        int type;
        Boolean isOnRight;
        float drawX = 0;
        float drawY = 0;
        Player player;

        //Constructor(s)
        public Branch(float x, float y, int type, Boolean isOnRight) : base("branch", x, y)
        {
            if (!isOnRight && type == 0)
            {
                this.x = -Camera.Width/2;
                base.addTrait(new FallingCollision(this, false));
                width = 300;
            }
            else if (isOnRight && type == 0)
            {
                this.x = Camera.Width / 2-300;
                width = 300;
                base.addTrait(new FallingCollision(this, false));
            }
            else if (!isOnRight && type == 1)
            {
                this.x = -Camera.Width / 2+250;
                base.addTrait(new FallingCollision(this, false));
                width = 200;
            }
            else if (isOnRight && type == 1)
            {
                this.x = Camera.Width / 2 - 450;
                base.addTrait(new FallingCollision(this, false));
                width = 200;
            }
            this.type = type;
            this.isOnRight = isOnRight;

            if (isOnRight)
            {
                drawX = Camera.Width / 2 - 200;
            }
            else
            {
                drawX = -Camera.Width / 2 + 200;
            }
            drawY = y;

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
                (int)(drawX - BranchSprites.branchSize.X * scale / 2 - Camera.X),
                (int)(drawY - BranchSprites.branchSize.Y * scale / 2 - Camera.Y),
                (int)(BranchSprites.branchSize.X * scale),
                (int)(BranchSprites.branchSize.Y * scale));

            if (isOnRight)
            {
                spriteBatch.Draw(BranchSprites.branch,
                    destinationRectangle: DR,
                    sourceRectangle: BranchSprites.branchSprites[type],
                    color: Color.White);

            }
            else
            {
                spriteBatch.Draw(BranchSprites.branch,
                    destinationRectangle: DR,
                    sourceRectangle: BranchSprites.branchSprites[type],
                    effects: SpriteEffects.FlipHorizontally,
                    color: Color.White);
            }

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
