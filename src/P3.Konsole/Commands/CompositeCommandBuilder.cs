using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole.Commands
{
    public class CompositeCommandBuilder : ICompositeCommandBuilder
    {
        protected List<ICommandDefinition> _commandDefinitions = new List<ICommandDefinition>();
        protected ICommandDefinition _catchAll;
        protected string _name;

        public CompositeCommandBuilder() { }
        public CompositeCommandBuilder(string name) {
            _name = name;
        }

        public ICompositeCommandBuilder DefineCatchAllCommand(ICommandDefinition commandDefinition) {
            _catchAll = commandDefinition;
            return this;
        }

        public ICompositeCommandBuilder DefineCommand(ICommandDefinition commandDefinition) {
            _commandDefinitions.Add(commandDefinition);
            return this;
        }

        public CompositeCommandDefinition Build() {
            return new CompositeCommandDefinition(_name, _commandDefinitions, _catchAll);
        }
    }
}