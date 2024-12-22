using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Services
{
    public class AccountsService
    {
        private readonly MyOptions _myOptions;

        public AccountsService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }

        public ExecutionResult<List<AccountsDTO>> Get(int? id_account = null)
        {
            List<AccountsDTO> lista = new List<AccountsDTO>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id_account", id_account, DbType.Int32, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                lista = conn.Query<AccountsDTO>(Constants.SP_ACCOUNTS_GET, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return new ExecutionResultFactory<List<AccountsDTO>>().GetSuccessExecutionResult(lista, string.Empty);

        }

        public ExecutionResult<AccountsDTO> Insert(AccountsDTO dto, string user)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@balance", dto.balance, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@account_number", dto.account_number, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.SP_ACCOUNTS_INSERT, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<AccountsDTO>().GetSuccessExecutionResult(dto, string.Empty);
        }

        public ExecutionResult<AccountsDTO> Update(AccountsDTO dto, string user)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id_account", dto.id_account, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@id_pendingAccount", dto.id_pendingAccount, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@balance", dto.balance, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@account_number", dto.account_number, DbType.Int32, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.SP_ACCOUNTS_UPDATE, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<AccountsDTO>().GetSuccessExecutionResult(dto, string.Empty);
        }

        public ExecutionResult<AccountsDTO> Delete(int id_account)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id_account", id_account, DbType.Int32, ParameterDirection.Input);


            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.SP_ACCOUNTS_DELETE, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<AccountsDTO>().GetSuccessExecutionResult(new AccountsDTO(), string.Empty);
        }
    }
}

