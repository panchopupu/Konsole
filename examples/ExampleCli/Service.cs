using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleCli
{
    public interface IService
    {
        string DoSomething();
    }

    public class Service : IService
    {
        public string DoSomething() {
            return Guid.NewGuid().ToString();
        }
    }
}