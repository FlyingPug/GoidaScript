namespace Interpritator
{
    public abstract class RPNAction
    {
        public abstract void Action();
    }

    public class Empty
    {
        public void Action() { }
    }
}