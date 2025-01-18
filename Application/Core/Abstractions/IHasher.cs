namespace Application.Core.Abstractions
{
    public interface IHasher
    {
        Task<string> Hash(Stream stream);
        Task<string> Hash(string url);
    }
}