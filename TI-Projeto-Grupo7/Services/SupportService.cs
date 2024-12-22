using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Services
{
    public class SupportService
    {
        private readonly MyOptions _myOptions;

        public SupportService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }

        public ExecutionResult<List<SupportDTO>> Get(int? id_ticket = null)
        {
            List<SupportDTO> lists = new List<SupportDTO>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id_ticket", id_ticket, DbType.Int32, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                lists = conn.Query<SupportDTO>(Constants.SP_SUPPORT_GET, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return new ExecutionResultFactory<List<SupportDTO>>().GetSuccessExecutionResult(lists, string.Empty);

        }

        public ExecutionResult<SupportDTO> Insert(SupportDTO dto, string user)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@supName", dto.supName, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
            parameters.Add("@subject", dto.subject, DbType.String, ParameterDirection.Input);
            parameters.Add("@message", dto.message, DbType.String, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.SP_SUPPORT_INSERT, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<SupportDTO>().GetSuccessExecutionResult(dto, string.Empty);
        }

        public ExecutionResult<SupportDTO> Update(SupportDTO dto, string user)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id_ticket", dto.id_ticket, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@id_user", dto.id_user, DbType.String, ParameterDirection.Input);
            parameters.Add("@supName", dto.supName, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
            parameters.Add("@subject", dto.subject, DbType.String, ParameterDirection.Input);
            parameters.Add("@message", dto.message, DbType.String, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.SP_SUPPORT_UPDATE, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<SupportDTO>().GetSuccessExecutionResult(dto, string.Empty);
        }

        public ExecutionResult<SupportDTO> Delete(int id_ticket)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id_ticket", id_ticket, DbType.Int32, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.SP_SUPPORT_DELETE, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<SupportDTO>().GetSuccessExecutionResult(new SupportDTO(), string.Empty);
        }
    }
}


