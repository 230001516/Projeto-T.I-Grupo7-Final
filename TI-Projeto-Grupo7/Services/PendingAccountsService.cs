using Dapper;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Services
{
    public class PendingAccountsService
    {
        private readonly MyOptions _myOptions;
        private readonly ILogger<PendingAccountsService> _logger;

        public PendingAccountsService(IOptions<MyOptions> myOptions, ILogger<PendingAccountsService> logger)
        {

            _myOptions = myOptions.Value;
            _logger = logger;

        }

        public ExecutionResult<List<PendingAccountsDTO>> Get(int? id_accountPending = null)
        {

            try
            {

                List<PendingAccountsDTO> listpa = new List<PendingAccountsDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_accountPending", id_accountPending, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    listpa = conn.Query<PendingAccountsDTO>(Constants.SP_PENDING_ACCOUNTS_GET, parameters, commandType: CommandType.StoredProcedure).ToList();

                }

                return new ExecutionResultFactory<List<PendingAccountsDTO>>().GetSuccessExecutionResult(listpa, string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while fetching pending accounts.");
                return new ExecutionResultFactory<List<PendingAccountsDTO>>().GetFailedExecutionResult("Failed to retrive pending accounts.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<List<PendingAccountsDTO>>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<PendingAccountsDTO> Insert(PendingAccountsDTO dto, string user)
        {

            try
            {

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_user", dto.id_user, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@id_worker", dto.id_worker, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@account_state", dto.account_state, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@motive", dto.motive, DbType.String, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    result = conn.Execute(Constants.SP_PENDING_ACCOUNTS_INSERT, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<PendingAccountsDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while inserting pending accounts.");
                return new ExecutionResultFactory<PendingAccountsDTO>().GetFailedExecutionResult("Failed to insert pending accounts.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<PendingAccountsDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<PendingAccountsDTO> Update(PendingAccountsDTO dto, string user)
        {

            try
            {

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_accountPending", dto.id_accountPending, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@id_user", dto.id_user, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@id_worker", dto.id_worker, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@account_state", dto.account_state, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@motive", dto.motive, DbType.String, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    result = conn.Execute(Constants.SP_PENDING_ACCOUNTS_UPDATE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<PendingAccountsDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while updating pending accounts.");
                return new ExecutionResultFactory<PendingAccountsDTO>().GetFailedExecutionResult("Failed to update pending accounts.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<PendingAccountsDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<PendingAccountsDTO> Delete(int id_accountPending)
        {

            try
            {

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_accountPending", id_accountPending, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    result = conn.Execute(Constants.SP_PENDING_ACCOUNTS_DELETE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<PendingAccountsDTO>().GetSuccessExecutionResult(new PendingAccountsDTO(), string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while deleting pending accounts.");
                return new ExecutionResultFactory<PendingAccountsDTO>().GetFailedExecutionResult("Failed to delete pending account.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<PendingAccountsDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
    }
}