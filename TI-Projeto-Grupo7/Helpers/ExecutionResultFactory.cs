namespace TI_Projeto_Grupo7.Helpers
{
    public class ExecutionResultFactory<T>{

        public ExecutionResultFactory() { }

        public ExecutionResult<T> GetSuccessExecutionResult(T result, string message){ 
            
            return GetSuccessExecutionResult(result, new List<string>() { message });

        }

        public ExecutionResult<T> GetSuccessExecutionResult(T result, List<string> message)
        {

            return new ExecutionResult<T>()
            {
                Results = result,
                Message = message,
                Status = true
            };
        }
    }
}
