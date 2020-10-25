using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_Physics.Traits;
using Fall.src.Entities;
using Fall.src;

namespace New_Physics.Entities
{
    public static class EntityHandler
    {
        public static List<Entity> entities;

        public static void Init()
        {
            entities = new List<Entity>();

            
            entities.Add(new Player(50, 0));
            entities.Add(new Platform(-200, 400, 500, 100));

            //entities.Add(new Leaf(100, 0));
            entities.Add(new Branch(200, 300, 0, false));
            entities.Add(new Bug(200, 100));
            entities.Add(new Tree(0, 0));
            entities.Add(new Leaf(0, 200));
        }

        public static void Update()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update();
            }
            //Console.WriteLine("time mod = " + entities[0].tm);
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(spriteBatch, graphicsDevice);
            }
        }

        //Set time modifier
        public static void setTm(float set)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].tm = set;
            }
        }

        //Modify/change time modifier by mod
        public static void modTm(float mod)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].tm + mod >= 0) entities[i].tm += mod;
                else entities[i].tm = 0;
            }
        }
    }
    public abstract class Entity
    {
        public List<Trait> traits;

        public string classId;

        public float x;
        public float y;

        public float dx = 0;
        public float dy = 0;

        public float repDx = 0;
        public float repDy = 0;


        public float width;
        public float height;

        public float tm = 1f;

        public Entity(string classId, float x, float y)
        {
            traits = new List<Trait>();
            this.classId = classId;
            this.x = x;
            this.y = y;
        }

        public void addTrait(Trait t)
        {
            traits.Add(t);
        }

        public Trait getTrait(string name)
        {
            for (int i = 0; i < traits.Count; i++)
            {
                if (traits[i].name == name)
                {
                    return traits[i];
                }
            }
            return null;
        }

        public Boolean hasTrait(string name)
        {
            for (int i = 0; i < traits.Count; i++)
            {
                if (traits[i].name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract void Update();

        protected void traitUpdate()
        {
            for (int i = 0; i < traits.Count; i++)
            {
                if (traits[i].name != "rigidbody") traits[i].Update();
                
            }
            repDx = dx * tm;
            repDy = dy * tm;
            for (int i = 0; i < traits.Count; i++)
            {
                if (traits[i].name == "rigidbody") traits[i].Update();
            }
            repDx = dx * tm;
            repDy = dy * tm;
            x += repDx;
            y += repDy;
        }

        public abstract void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
