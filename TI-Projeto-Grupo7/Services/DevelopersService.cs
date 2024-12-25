using Dapper;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Services
{
    public class DevelopersService
    {
        private readonly MyOptions _myOptions;
        private readonly ILogger<DevelopersService> _logger;

        public DevelopersService(IOptions<MyOptions> myOptions, ILogger<DevelopersService> logger){

            _myOptions = myOptions.Value;
            _logger = logger;

        }

        public ExecutionResult<List<DevelopersDTO>> Get(int? id_developer = null){

            try{

                List<DevelopersDTO> listd = new List<DevelopersDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_developer", id_developer, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    listd = conn.Query<DevelopersDTO>(Constants.SP_DEVELOPERS_GET, parameters, commandType: CommandType.StoredProcedure).ToList();

                }

                return new ExecutionResultFactory<List<DevelopersDTO>>().GetSuccessExecutionResult(listd, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching developers.");
                return new ExecutionResultFactory<List<DevelopersDTO>>().GetFailedExecutionResult("Failed to retrive develoepers.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<List<DevelopersDTO>>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<DevelopersDTO> Insert(DevelopersDTO dto, string user){

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@devName", dto.devName, DbType.String, ParameterDirection.Input);
                parameters.Add("@description", dto.devDescription, DbType.String, ParameterDirection.Input);
                parameters.Add("@twitter", dto.twitter, DbType.String, ParameterDirection.Input);
                parameters.Add("@instagram", dto.instagram, DbType.String, ParameterDirection.Input);
                parameters.Add("@linkedin", dto.linkedin, DbType.String, ParameterDirection.Input);
                parameters.Add("@image", dto.devImage, DbType.String, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_DEVELOPERS_INSERT, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<DevelopersDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching developers.");
                return new ExecutionResultFactory<DevelopersDTO>().GetFailedExecutionResult("Failed to insert developers.");

            }catch (Exception ex){
                
                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<DevelopersDTO>().GetFailedExecutionResult("An unexpected error occurred.");
                
            }
        }

        public ExecutionResult<DevelopersDTO> Update(DevelopersDTO dto, string user){

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_developer", dto.id_developer, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@devName", dto.devName, DbType.String, ParameterDirection.Input);
                parameters.Add("@description", dto.devDescription, DbType.String, ParameterDirection.Input);
                parameters.Add("@twitter", dto.twitter, DbType.String, ParameterDirection.Input);
                parameters.Add("@instagram", dto.instagram, DbType.String, ParameterDirection.Input);
                parameters.Add("@linkedin", dto.linkedin, DbType.String, ParameterDirection.Input);
                parameters.Add("@image", dto.devImage, DbType.String, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_DEVELOPERS_UPDATE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<DevelopersDTO>().GetSuccessExecutionResult(dto, string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching developers.");
                return new ExecutionResultFactory<DevelopersDTO>().GetFailedExecutionResult("Failed to update developers.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<DevelopersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }

        public ExecutionResult<DevelopersDTO> Delete(int id_developer){

            try{

                int result;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id_developer", id_developer, DbType.Int32, ParameterDirection.Input);


                using (IDbConnection conn = new SqlConnection(_myOptions.ConnString)){

                    result = conn.Execute(Constants.SP_DEVELOPERS_DELETE, parameters, commandType: CommandType.StoredProcedure);

                }

                return new ExecutionResultFactory<DevelopersDTO>().GetSuccessExecutionResult(new DevelopersDTO(), string.Empty);

            }catch (SqlException ex){

                _logger.LogError(ex, "An error occurred while fetching developers.");
                return new ExecutionResultFactory<DevelopersDTO>().GetFailedExecutionResult("Failed to delete developer.");

            }catch (Exception ex){

                _logger.LogError(ex, "An unexpected error occurred.");
                return new ExecutionResultFactory<DevelopersDTO>().GetFailedExecutionResult("An unexpected error occurred.");

            }
        }
    }
}

