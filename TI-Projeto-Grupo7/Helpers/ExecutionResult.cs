namespace TI_Projeto_Grupo7.Helpers
{
    public class ExecutionResult<T>
    {
        public bool Status { get; set; }
        public T Results { get; set; }  
        public List<string>? Message { get; set; }
    }
}
