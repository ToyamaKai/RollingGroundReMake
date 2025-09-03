namespace MPLib
{
    public interface IMPObject : System.IDisposable
    {
        void Initialize();

        void Tick();
    }
}