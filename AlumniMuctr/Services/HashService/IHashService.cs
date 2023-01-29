namespace AlumniMuctr.Services.HashService
{
    public interface IHashService
    {
        public string HashPassword(string password);

        public bool VerifyHashedPassword(string hashedPassword, string password);

        public bool ByteArraysEqual(byte[] b1, byte[] b2);
    }
}
