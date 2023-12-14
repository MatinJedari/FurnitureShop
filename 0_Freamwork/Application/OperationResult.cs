namespace _0_Freamwork.Application
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool IsSuccedded { get; set; }
        public OperationResult()
        {
            IsSuccedded = false;
        }

        public OperationResult Succedded(string message = "Operation done successfully.")
        {
            Message = message;
            IsSuccedded = true;
            return this;
        }

        public OperationResult Failed(string message = "Operation failed.")
        {
            Message = message;
            IsSuccedded = false;
            return this;
        }
    }
}
