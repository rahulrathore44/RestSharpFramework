using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public interface ICommand
    {
        IResponse ExecuteRequest(); // for response in string format
        IResponse<T> ExecuteRequest<T>(); // for the de-serialize object from response
        byte[] DownloadData();
        Task<byte[]> DownloadDataAsync();
    }
}
