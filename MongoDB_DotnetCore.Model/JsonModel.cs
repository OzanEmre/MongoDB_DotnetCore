namespace MongoDB_DotnetCore.Model
{
    public class JsonResultModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }


        public JsonResultModel()
        {
            Success = true;
            Message = "";
        }

        public JsonResultModel(object result, string message, bool success)
       : base()
        {
            Success = success;
            Message = message;
            Result = result;
        }

        public JsonResultModel(object result, string message)
            : base()
        {
            Success = true;
            Message = message;
            Result = result;
        }

        public JsonResultModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}