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
        private readonly PendingAccountsService _pendingAccounts;

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

        public ExecutionResult<AccountsDTO> ProcessApprovedAccount(int id_pendingAccount, string username)
        {
            try
            {

                var pendingAccountResult = _pendingAccounts.Get(id_pendingAccount);

                if (!pendingAccountResult.Status || pendingAccountResult.Results == null || !pendingAccountResult.Results.Any())
                {
                    return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("Pending account not found.");
                }

                var pendingAccount = pendingAccountResult.Results.FirstOrDefault();

                if (pendingAccount.account_state != 1)
                {
                    return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("Pending account is not approved.");
                }

                var newAccount = new AccountsDTO
                {
                    id_pendingAccount = pendingAccount.id_accountPending,
                    account_number = GenerateAccountNumber(IsAccountNumberUnique),
                    balance = 0 
                };

                return Insert(newAccount, username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing approved account for pending account ID: {id_pendingAccount}", id_pendingAccount);
                return new ExecutionResultFactory<AccountsDTO>().GetFailedExecutionResult("An error occurred while processing the approved account.");
            }
        }
        private bool IsAccountNumberUnique(int accountNumber)
        {
            var result = Get();

            if (!result.Status)
            {
                _logger.LogError("Failed to retrieve accounts to validate account number uniqueness.");
                throw new InvalidOperationException("Unable to validate account number uniqueness.");
            }

            var existingAccountNumbers = result.Results?.Select(a => a.account_number).ToList();

            return existingAccountNumbers == null || !existingAccountNumbers.Contains(accountNumber);
        }

        private int GenerateAccountNumber(Func<int, bool> isAccountNumberUnique)
        {
            var random = new Random();
            int accountNumber;

            do
            {
                accountNumber = random.Next(1000000000, int.MaxValue);
            }
            while (!isAccountNumberUnique(accountNumber)); 

            return accountNumber;
        }
    }
}

