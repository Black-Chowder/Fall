using Microsoft.Xna.Framework.Graphics;
using New_Physics.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;
using System.Linq.Expressions;
using New_Physics.Entities;
using Fall.src;
using Frogs.src;
using Fall.src.Traits;
using Microsoft.Xna.Framework.Audio;

namespace New_Physics.Entities
{
    public static class PlayerSprites
    {
        public static Texture2D cursor;

        public static Texture2D acornSpriteSheet;
        public static Vector2 acornSize = new Vector2(12, 12);
        public static Rectangle[] acornSprites;

        public static Rectangle[] tongueBody;
        public static Rectangle[] tongueStuck;
        public static Rectangle[] tongueEnd;

        //Audio Variables
        public static SoundEffect jumpsfx;
        public static SoundEffect outTongue;
        public static SoundEffect fall;

        public static void LoadContent(ContentManager Content)
        {
            cursor = Content.Load<Texture2D>("Cursor");

            acornSpriteSheet = Content.Load<Texture2D>("acorn");
            acornSprites = Utils.spriteSheetLoader(12, 12, 8, 1);

            //JumpDustSprites.LoadContent(Content);
            //PlayerSmashSprites.LoadContent(Content);

            //Import Audio
            //jumpsfx = Content.Load<SoundEffect>("Jump");
            //outTongue = Content.Load<SoundEffect>("outTongue2");
            //fall = Content.Load<SoundEffect>("Fall");
        }
    }


    public class Player : Entity
    {
        // <Misc Variables>
        private float speed = 2.2f * Camera.gameScale;
        MouseState mouse;

        Boolean isFacingRight = true;

        float maxYVel;
        float fallTimer = 0;
        float fallTimerMax = 180;
        public float angle = 0;
        public float angleChange = 0;
        // </Misc Variables>

        // <Animation Variables>
        String animation = "neutral";
        int animator = 0;

        float aniMod = 10;

        // </Animation Variables>

        private Boolean swingInit = true;

        // </Swing Variables>


        //Testing Variables
        private Boolean showHitbox = true;

        public Player(float x, float y) : base("player", x, y)
        {
            width = 50 * Camera.gameScale;
            height = 50 * Camera.gameScale;
            addTrait(new Gravity(this, 1f * Camera.gameScale));
            addTrait(new FallingCollision(this, true));
            //addTrait(new Friction(this, (float)1.5, (float)1.02));
            //addTrait(new Timer(this, "timer", 300));

            //List<Hitbox> hitboxes = new List<Hitbox>();
            //hitboxes.Add(new Hitbox(width / 2, height / 2, width, height));
            //hitboxes.Add(new Hitbox(-25, 56, this.width, 5));

            //addTrait(new Rigidbody(this, hitboxes, false));

            maxYVel = 20;// * Camera.gameScale;
        }

        public override void Update()
        {
            animation = "neutral";
            KeyboardState keys = Keyboard.GetState();
            mouse = Mouse.GetState();



            if (Math.Abs(dx) > 10)
            {
                if (dx < 0) dx = -10;
                else dx = 10;
            }

            x = mouse.X + Camera.X;

            //Sling Handling
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                
            }
            else if (mouse.LeftButton != ButtonState.Pressed)
            {
                
            }


            //Swing Handling
            if (mouse.RightButton == ButtonState.Pressed && swingInit)
            {

            }
            else if (mouse.RightButton != ButtonState.Pressed) swingInit = true;

            //Handle Angle
            angle += angleChange;
            if (angle > 360) { angle = 0; }
            if (angle < 0) { angle = 360; }

            //Regulates Gravitational Speed
            //Console.WriteLine("Max Y Vel = " + maxYVel + " | dy = " + dy);
            if (dy >= maxYVel)//<= MIGHT TAKE AWAY LATER
            {
                //Console.WriteLine("Happening");
                dy = maxYVel;
                
            }

            //Collision With Sides
            sideCollision();


            //  UPDATE TRAITS  //
            base.traitUpdate();  //  <<<======  UPDATE TRAITS
            //  UPDATE TRAITS  //


            //Pretty self explanatory
            prepAnimation();

            Camera.SudoGoTo(-Camera.Width/2, this.y - Camera.Height / 2);
        }

        private void prepAnimation()
        {
            animator++;
            //TODO: Change if statements to switch statemets
            if (animator < 0)
            {
                animator = 0;
            }
            if (animation == "neutral" && animator != 0)
            {
                animator = 0;
            }
            /*
            else if (animation == "jump" && animator >= PlayerSprites.jump.Count() * aniMod)
            {
                animator = PlayerSprites.jump.Count() * (int)aniMod - 1;
            }
            else if (animation == "openMouth" && animator / aniMod >= PlayerSprites.openMouth.Count())
            {
                animator = PlayerSprites.openMouth.Count() - 1;
            }
            else if (animation == "swing" && animator / aniMod >= PlayerSprites.swing.Count())
            {
                animator = PlayerSprites.swing.Count() - 1;
            }
            */
        }

        [Obsolete]
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Draw Hitbox
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            /*
            if (showHitbox) for (int i = 0; i < ((Rigidbody)getTrait("rigidbody")).hitboxes.Count; i++)
                {
                    Hitbox hitbox = ((Rigidbody)getTrait("rigidbody")).hitboxes[i];
                    spriteBatch.Draw(texture, new Rectangle((int)(hitbox.x - Camera.X), (int)(hitbox.y - Camera.Y), (int)(hitbox.width), (int)(hitbox.height)), Color.White);
                }
            */


            //Console.WriteLine(Camera.gameScale);
            float scale = 8* Camera.gameScale;

            Rectangle DR = new Rectangle(
                (int)(x - PlayerSprites.acornSize.X * scale / 2 - Camera.X),
                (int)(y - PlayerSprites.acornSize.Y * scale / 2 - Camera.Y),
                (int)(PlayerSprites.acornSize.X * scale),
                (int)(PlayerSprites.acornSize.Y * scale));

            int angleToIndex = (int)(angle / 359 * 7);
            Console.WriteLine(angleToIndex);

            spriteBatch.Draw(PlayerSprites.acornSpriteSheet, 
                destinationRectangle: DR,
                sourceRectangle: PlayerSprites.acornSprites[angleToIndex],
                color: Color.White);
            spriteBatch.Draw(texture, new Rectangle(
                (int)(x - Camera.X),
                (int)(y - Camera.Y),
                (int)(10),
                (int)(10)),
                Color.White);

            int trueAnimator = (int)(animator / aniMod);

            spriteBatch.End();

            texture.Dispose();
        }
        // </Draw>

        private void setAnimation(String animation)
        {
            this.animation = animation;
            animator = 0;
        }


        private void sideCollision()
        {
            if (x < -Camera.Width/2+80)
            {
                x = -Camera.Width/2+ 80;
            }
            if (x > Camera.Width/2 - 80)
            {
                x = Camera.Width/2 - 80;
            }
        }
    }
}