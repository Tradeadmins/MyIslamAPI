using MyIslamWebApp.DataTransferObjects.MakeDua;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public interface IMakeDuaService
    {
        //MakeDua CRUD Operations
        IEnumerable<MakeDuaDTO> GetAllMakeDua();
        IEnumerable<MakeDuaDTO> GetAllMakeDuaByUser(string MakeDuaUser);
        bool AddMakeDua(MakeDuaDTO MakeDua, string userId);
    }
}