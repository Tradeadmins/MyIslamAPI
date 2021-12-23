using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.MakeDuas
{
    public interface IMakeDuaRepository : IRepository<MakeDua>
    {
        IEnumerable<MakeDua> GetAllMakeDua();
        IEnumerable<MakeDua> GetAllMakeDuaByUser(string MakeDuaUser);
        bool AddMakeDua(MakeDua MakeDua);
        bool DeleteMakeDua(MakeDua makeDua);
        bool DeleteMakeDuaById(int makeDuaId);
        MakeDua GetMakeDuaById(int makeDuaId);
    }
}