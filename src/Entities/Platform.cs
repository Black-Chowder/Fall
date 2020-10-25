using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using New_Physics.Traits;
using New_Physics.Entities;
using Fall.src;
using Microsoft.Xna.Framework.Content;

namespace New_Physics.Entities
{
    public static class PlatformSprites
    {
        public static Texture2D ss;
        public static Rectangle[] sprites;

        public static void LoadContent(ContentManager Content)
        {
            ss = Content.Load<Texture2D>("Platform");

            sprites = Utils.spriteSheetLoader(5, 5, 9, 1);
        }
    }
    public class Platform : Entity
    {
        public Boolean sRight = false;
        public Boolean sLeft = false;
        public Boolean sTop = false;
        public Boolean sBottom = false;

        public Platform(float x, float y, float width, float height,
            Boolean sRight = true, Boolean sLeft = true, Boolean sTop = true, Boolean sBottom = true) : base("platform", x, y)
        {
            Init(x, y, width, height, sRight, sLeft, sTop, sBottom);
        }

        private void Init(float x, float y, float width, float height,
            Boolean sRight, Boolean sLeft, Boolean sTop, Boolean sBottom)
        {
            base.width = width;
            base.height = height;

            addTrait(new Rigidbody(this, true));

            this.sRight = sRight;
            this.sLeft = sLeft;
            this.sTop = sTop;
            this.sBottom = sBottom;
        }


        public override void Update()
        {

            traitUpdate();
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Draw Hitbox
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { new Color(51,44,80) });
            spriteBatch.Begin();
            //spriteBatch.Draw(texture, new Rectangle((int)(x - Camera.X), (int)(y - Camera.Y), (int)width, (int)height), Color.White);
            spriteBatch.End();
            texture.Dispose();
        }
    }
}
