using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Response
{
    public class ApiResponse<T>
    {
      public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }


        private ApiResponse(bool  success, T data,List<string>errors)
        {
            Success = success;
            Data = data;
            Errors = errors ?? new List<string>();
        }
        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>(true, data, null);
        }

        public static ApiResponse<T> ErrorResponse(List<string> errors)
        {
            return new ApiResponse<T>(false,default,errors);
        }

        public static ApiResponse<T> ErrorResponse(string error)
        {
            return new ApiResponse<T>(false,default,new List<string> { error });
        }

    }
}
