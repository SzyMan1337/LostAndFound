namespace LostAndFound.ChatService.Core.Extensions
{
    public static class GuidExtensions
    {
        public const int ByteCount = 16;

        public static Guid MungeTwoGuids(this Guid guid1, Guid guid2)
        {
            var destByte = new byte[ByteCount];
            var guid1Byte = guid1.ToByteArray();
            var guid2Byte = guid2.ToByteArray();

            for (int i = 0; i < ByteCount; i++)
            {
                destByte[i] = (byte)(guid1Byte[i] ^ guid2Byte[i]);
            }

            return new Guid(destByte);
        }
    }
}
