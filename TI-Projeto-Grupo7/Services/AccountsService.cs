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
        private readonly ILogger<AccountsService> _logger; 

        public AccountsService(IOptions<MyOptions> myOptions, ILogger<AccountsService> logger)
        {
            _myOptions = myOptions.Value;
            _logger = logger;
        }

        public ExecutionResult<List<AccountsDTO>> Get(int? id_account = null){

            try{

                List<AccountsDTO> lista = new List<AccountsDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_account", id_account, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    lista = conn.Query<AccountsDTO>(Constants.SP_ACCOUNTS_GET, parameters, commandType: CommandType.StoredProcedure).ToList();
                
                }

                return new ExecutionResultFactory<List<AccountsDTO>>().GetSuccessExecutionResult(lista, string.Empty);
            
            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching accounts.");
                return new ExecutionResultFactory<List<AccountsDTO>>().GetFailedExecutionResult("Failed to retrive accounts.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<List<AccountsDTO>>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<AccountsDTO> Insert(AccountsDTO dto, string user){

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@balance", dto.balance, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@account_number", dto.account_number, DbType.Int32, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_ACCOUNTS_INSERT, parameters, commandType: CommandType.StoredProcedure);
                
                }

                return new ExecutionResultFactory<AccountsDTO>().GetSuccessExecutionResult(dto, string.Empty);
            
            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching accounts.");
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("Failed to insert account.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<AccountsDTO> Update(AccountsDTO dto, string user){

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_account", dto.id_account, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@id_pendingAccount", dto.id_pendingAccount, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@balance", dto.balance, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@account_number", dto.account_number, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_ACCOUNTS_UPDATE, parameters, commandType: CommandType.StoredProcedure);
                
                }

                return new ExecutionResultFactory<AccountsDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching accounts.");
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("Failed to update the account.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<AccountsDTO> Delete(int id_account) {

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_account", id_account, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)) {

                    result = conn.Execute(Constants.SP_ACCOUNTS_DELETE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<AccountsDTO>().GetSuccessExecutionResult(new AccountsDTO(), string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching accounts.");
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("Failed to delete account.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
    }
}

