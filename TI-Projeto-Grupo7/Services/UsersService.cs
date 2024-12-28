using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Services
{
    public class UsersService
    {
        private readonly MyOptions _myOptions;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IOptions<MyOptions> myOptions, ILogger<UsersService> logger)
        {
            _myOptions = myOptions.Value;
            _logger = logger;
        }

        public ExecutionResult<List<UsersDTO>> Get(int? id_user = null)
        {

            try{

                List<UsersDTO> listu = new List<UsersDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_user", id_user, DbType.Int32, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
                {
                    listu = conn.Query<UsersDTO>(Constants.SP_USERS_GET, parameters, commandType: CommandType.StoredProcedure).ToList();
                }

                return new ExecutionResultFactory<List<UsersDTO>>().GetSuccessExecutionResult(listu, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<List<UsersDTO>>().GetFailedExecutionResult("Failed to retrive users.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<List<UsersDTO>>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

            public async Task<ExecutionResult<List<UsersDTO>>> GetAsync(int? id_user = null) {

            try{

                List<UsersDTO> listu = new List<UsersDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_user", id_user, DbType.Int32, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    listu = (await conn.QueryAsync<UsersDTO>(Constants.SP_USERS_GET, parameters, commandType: CommandType.StoredProcedure)).ToList();
                }

                return new ExecutionResultFactory<List<UsersDTO>>().GetSuccessExecutionResult(listu, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<List<UsersDTO>>().GetFailedExecutionResult("Failed to retrive users.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<List<UsersDTO>>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        } 
        public ExecutionResult<UsersDTO> Insert(UsersDTO dto, string user)
        {
            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@firstname", dto.firstname, DbType.String, ParameterDirection.Input);
                parameters.Add("@surname", dto.surname, DbType.String, ParameterDirection.Input);
                parameters.Add("@nif", dto.nif, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@user_address", dto.user_address, DbType.String, ParameterDirection.Input);
                parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
                parameters.Add("@phone_number", dto.phone_number, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@password", dto.password, DbType.String, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_USERS_INSERT, parameters, commandType: CommandType.StoredProcedure);

                }
                return new ExecutionResultFactory<UsersDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("Failed to insert the user.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
        public async Task<ExecutionResult<UsersDTO>> InsertAsync(UsersDTO dto, string user){

            try{

                int result;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@firstname", dto.firstname, DbType.String, ParameterDirection.Input);
                parameters.Add("@surname", dto.surname, DbType.String, ParameterDirection.Input);
                parameters.Add("@nif", dto.nif, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@user_address", dto.user_address, DbType.String, ParameterDirection.Input);
                parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
                parameters.Add("@phone_number", dto.phone_number, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@password", dto.password, DbType.String, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = await conn.ExecuteAsync(Constants.SP_USERS_INSERT, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<UsersDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while inserting a user.");

                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("Failed to insert the user.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
        public ExecutionResult<UsersDTO> Update(UsersDTO dto, string user) {

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@firstname", dto.firstname, DbType.String, ParameterDirection.Input);
                parameters.Add("@surname", dto.surname, DbType.String, ParameterDirection.Input);
                parameters.Add("@nif", dto.nif, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@user_address", dto.user_address, DbType.String, ParameterDirection.Input);
                parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
                parameters.Add("@phone_number", dto.phone_number, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@password", dto.password, DbType.String, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_USERS_UPDATE, parameters, commandType: CommandType.StoredProcedure);
                
                }

                return new ExecutionResultFactory<UsersDTO>().GetSuccessExecutionResult(dto, string.Empty);
            
            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("Failed to update the user.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
        public async Task<ExecutionResult<UsersDTO>> UpdateAsync(UsersDTO dto, string user) {

            try {
                
                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@firstname", dto.firstname, DbType.String, ParameterDirection.Input);
                parameters.Add("@surname", dto.surname, DbType.String, ParameterDirection.Input);
                parameters.Add("@nif", dto.nif, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@user_address", dto.user_address, DbType.String, ParameterDirection.Input);
                parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
                parameters.Add("@phone_number", dto.phone_number, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@password", dto.password, DbType.String, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = await conn.ExecuteAsync(Constants.SP_USERS_UPDATE, parameters, commandType: CommandType.StoredProcedure);
                
                }

                return new ExecutionResultFactory<UsersDTO>().GetSuccessExecutionResult(dto, string.Empty);
            }
            catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("Failed to update the user.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
        public ExecutionResult<UsersDTO> Delete(int id_user) {

            try{
                
                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_user", id_user, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_USERS_DELETE, parameters, commandType: CommandType.StoredProcedure);
                
                }

                return new ExecutionResultFactory<UsersDTO>().GetSuccessExecutionResult(new UsersDTO(), string.Empty);
            
            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("Failed to delete the user.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public async Task<ExecutionResult<UsersDTO>> DeleteAsync(int id_user) {

            try {
                
                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_user", id_user, DbType.Int32, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = await conn.ExecuteAsync(Constants.SP_USERS_DELETE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<UsersDTO>().GetSuccessExecutionResult(new UsersDTO(), string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching users.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("Failed to delete the user.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<UsersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
    }
}
