using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.WpfApp.Api;

namespace WpfApp1.Api
{
    public class Result<T> : Result
    {
    public T Value { get; set; }
    public string Error { get; set; }

    }
}

