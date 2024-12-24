namespace TI_Projeto_Grupo7.Helpers
{
    public class ExecutionResultFactory<T>
    {
        public ExecutionResultFactory() { }

        public ExecutionResult<T> GetSuccessExecutionResult(T result, string message)
        {
            return GetSuccessExecutionResult(result, new List<string> { message });
        }

        public ExecutionResult<T> GetSuccessExecutionResult(T result, List<string> messages)
        {
            return new ExecutionResult<T>
            {
                Results = result,
                Message = messages,
                Status = true 
            };
        }

        public ExecutionResult<T> GetFailedExecutionResult(string message)
        {
            return GetFailedExecutionResult(new List<string> { message });
        }

        public ExecutionResult<T> GetFailedExecutionResult(List<string> messages)
        {
            return new ExecutionResult<T>
            {
                Results = default, 
                Message = messages,
                Status = false
            };
        }
    }
}