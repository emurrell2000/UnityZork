namespace Zork
{
    public interface IOutputService
    {
        void Clear();

        void Write(object value);

        void WriteLine(object value);
    }
}
