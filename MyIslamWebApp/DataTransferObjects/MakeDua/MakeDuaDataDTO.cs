using System;

namespace MyIslamWebApp.DataTransferObjects.MakeDua
{
    public class MakeDuaDTO
    {
        public int MakeDuaId { get; set; }
        public int MakeDuaPrayerRequestId { get; set; }
        public string MakeDuaByUserId { get; set; }
    }
}