using DTO.DB;
using DTO.Enum;
using DTO.ReqInParm;
using LibServer.Repository;
using System.Collections.Generic;
using System.Data;

namespace RepositoryCore.Executor
{
    public interface IQueryByStoredProcedure<R, T> : IRepositoryAction<GenTwoReqInParm<string, IEnumerable<IDataParameter>>, List<T>> where R : class { }

    public interface IQueryForModelByStoredProcedure<T> : IRepositoryAction<GenTwoReqInParm<string, IEnumerable<IDataParameter>>, List<T>> where T : class { }
}