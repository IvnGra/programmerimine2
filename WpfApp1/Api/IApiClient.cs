using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.WpfApp.Api;

namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<Result<List<User>>> List();
        Task<Result> Save(User user);
        Task<Result> Delete(int id);
    }

}