using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public class RestApiExecutor
    {
        private ICommand Command;

        public void SetCommand(ICommand _command)
        {
            Command = _command;
        }

        public IResponse ExecuteRequest()
        {
            return Command.ExecuteRequest();
        }

        public IResponse<T> ExecuteRequest<T>()
        {
            return Command.ExecuteRequest<T>();
        }
    }
}
