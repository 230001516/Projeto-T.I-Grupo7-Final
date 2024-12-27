using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Services
{
    public class TransferService
    {
        private readonly MyOptions _myOptions;
        private readonly ILogger<TransferService> _logger;

     public TransferService(IOptions<MyOptions> myOptions, ILogger<TransferService> logger)
        {
            _myOptions = myOptions.Value;
            _logger = logger;
        }

        public ExecutionResult<List<TransfersDTO>> Get(int? id_transfer = null)
        {

            try
            {

                List<TransfersDTO> listt = new List<TransfersDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_transfer", id_transfer, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    listt = conn.Query<TransfersDTO>(Constants.SP_TRANSFERS_GET, parameters, commandType: CommandType.StoredProcedure).ToList();

                }

                return new ExecutionResultFactory<List<TransfersDTO>>().GetSuccessExecutionResult(listt, string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while fetching transfers.");
                return new ExecutionResultFactory<List<TransfersDTO>>().GetFailedExecutionResult("Failed to retrive transfers.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<List<TransfersDTO>>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<TransfersDTO> Insert(TransfersDTO dto, string user)
        {

            try
            {

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_account", dto.id_account, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@transfer_value", dto.transfer_value, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@account_number", dto.account_number, DbType.Int32, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    result = conn.Execute(Constants.SP_TRANSFERS_INSERT, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<TransfersDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while fetching transfers.");
                return new ExecutionResultFactory<TransfersDTO>().GetFailedExecutionResult("Failed to insert the transfer.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<TransfersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<TransfersDTO> Update(TransfersDTO dto, string user)
        {

            try
            {

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_transfer", dto.id_transfer, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@id_account", dto.id_account, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@transfer_date", dto.transfer_date, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@transfer_value", dto.transfer_value, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@account_number", dto.account_number, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    result = conn.Execute(Constants.SP_TRANSFERS_UPDATE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<TransfersDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while fetching transfers.");
                return new ExecutionResultFactory<TransfersDTO>().GetFailedExecutionResult("Failed to update the transfer.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<TransfersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<TransfersDTO> Delete(int id_transfer)
        {

            try
            {

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_transfer", id_transfer, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {

                    result = conn.Execute(Constants.SP_TRANSFERS_DELETE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<TransfersDTO>().GetSuccessExecutionResult(new TransfersDTO(), string.Empty);

            }
            catch (SqlException ex)
            {

                _logger.LogError(ex, "An error occurred while fetching accounts.");
                return new ExecutionResultFactory<TransfersDTO>().GetFailedExecutionResult("Failed to delete transfer.");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<TransfersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
    } 
}
