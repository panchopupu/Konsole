using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole.Commands
{
    public static class CompositeCommandBuilderExtensions
    {
        public static ICompositeCommandBuilder DefineCompositeCommand(this ICompositeCommandBuilder builder, string name, Action<ICompositeCommandBuilder> action) {
            var childBuilder = new CompositeCommandBuilder(name);
            action(childBuilder);
            return builder.DefineCommand(childBuilder.Build());
        }

        public static ICompositeCommandBuilder DefineCompositeCatchAllCommand(this ICompositeCommandBuilder builder, Action<ICompositeCommandBuilder> action) {
            var childBuilder = new CompositeCommandBuilder();
            action(childBuilder);
            return builder.DefineCatchAllCommand(childBuilder.Build());
        }

        public static ICompositeCommandBuilder DefineCommand<TCommand, TParameter>(this ICompositeCommandBuilder builder, string name) where TCommand : class, ICommand<TParameter>
            => builder.DefineCommand(new CommandDefinition<TCommand, TParameter>(name));
        public static ICompositeCommandBuilder DefineCommand<TCommand>(this ICompositeCommandBuilder builder, string name) where TCommand : class, ICommand<string[]>
            => builder.DefineCommand(new CommandDefinition<TCommand, string[]>(name));

        public static ICompositeCommandBuilder DefineCatchAllCommand<TCommand, TParameter>(this ICompositeCommandBuilder builder) where TCommand : class, ICommand<TParameter>
            => builder.DefineCatchAllCommand(new CommandDefinition<TCommand, TParameter>(null));
        public static ICompositeCommandBuilder DefineCatchAllCommand<TCommand>(this ICompositeCommandBuilder builder) where TCommand : class, ICommand<string[]>
            => builder.DefineCatchAllCommand(new CommandDefinition<TCommand, string[]>(null));
    }
}