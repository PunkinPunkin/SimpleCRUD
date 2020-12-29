using DTO.ReqInParm;
using LibServer.Repository;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryCore.Executor
{
    public class QueryByStoredProcedure<R, T> : ADbActionExecutor<GenTwoReqInParm<string, IEnumerable<IDataParameter>>, List<T>>, IQueryByStoredProcedure<R, T> where R : class
    {
        public override List<T> Execute(RetCode retCode, GenTwoReqInParm<string, IEnumerable<IDataParameter>> request)
        {
            string procedureName = request.Parm_01;
            try
            {
                var query = _unit.Repository<R>().StoredProcedure<T>(procedureName, request.Parm_02).ToList();
                return query;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                SetRetCode(retCode, CommonCode.Fail, new string[] { ex.Message });
                return new List<T>();
            }
        }
    }

    public class QueryForModelByStoredProcedure<T> : ADbActionExecutor<GenTwoReqInParm<string, IEnumerable<IDataParameter>>, List<T>>, IQueryForModelByStoredProcedure<T> where T : class
    {
        public override List<T> Execute(RetCode retCode, GenTwoReqInParm<string, IEnumerable<IDataParameter>> request)
        {
            string procedureName = request.Parm_01;
            try
            {
                return _unit.Repository<T>().StoredProcedureForModel(procedureName, request.Parm_02).ToList();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                SetRetCode(retCode, CommonCode.Fail, new string[] { ex.Message });
                return new List<T>();
            }
        }
    }
}