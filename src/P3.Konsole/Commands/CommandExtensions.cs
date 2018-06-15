using P3.Konsole.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P3.Konsole.Commands
{
    public static class CommandExtensions
    {
        public static Task ExecuteAsync<T>(this ICommand<T> command, string[] args, CancellationToken token = default(CancellationToken))
            => command.ExecuteAsync(args.ParseCommandArgs<T>(), token);
    }
}