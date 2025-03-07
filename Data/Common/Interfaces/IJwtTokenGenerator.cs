namespace Data.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string cpfcnpj);
    }
}
