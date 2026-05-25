namespace MPLib
{
    public class MPObject : IMPObject
    {
        public MPObject()
        {

        }

        public virtual void Initialize() { }

        public virtual void Tick() { }

        public virtual void FixedTick() { }

        public virtual void Dispose() { }
    }
}

