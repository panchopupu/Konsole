using P3.Konsole.Commands;
using System.Linq;
using NSubstitute;
using P3.Konsole.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using P3.Konsole.Tests.Assets;

namespace P3.Konsole.Tests.Commands
{
    public class CompositeCommandDefinitionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task ExecuteCommandAsync_When_NoArgsAndCatchAll_Then_ThrowInvalidCommand(string argsC) {
            // -- arrange
            var args = argsC?.Split(" ");
            var defs = new ICommandDefinition[]{ };
            var catchAll = (ICommandDefinition)null;
            var sut = new CompositeCommandDefinition("sut",defs, catchAll);

            // -- act
            Func<Task> action = () =>sut.ExecuteCommandAsync(null,args);


            // -- assert
            await Assert.ThrowsAsync<InvalidCommandException>(action);
        }

        [Theory]
        [InlineAutoNData("", true, "catch")]
        [InlineAutoNData(null, true, "catch")]
        [InlineAutoNData("param param2", true, "catch")]
        [InlineAutoNData("child", true, "child")]
        [InlineAutoNData("child param", true, "child")]
        [InlineAutoNData("child param", false, "child")]
        public async Task ExecuteCommandAsync_When_Always_Then_ExecuteCorrectCommand(
            string argsC,
            bool hasCatchAll,
            string invokedCmd,

            ICommandDefinition catchAllCmd,
            IList<ICommandDefinition> defs,
            IServiceProvider sp) {

            // -- arrange
            string executedAction = null;
            var args = argsC?.Split(" ");
            catchAllCmd.Name.Returns("catch");
            defs.First().Name.Returns("child");
            var sut = new CompositeCommandDefinition("sut", defs, hasCatchAll ? catchAllCmd : null);

            defs.ToList().ForEach(c => c.WhenForAnyArgs(async a => await a.ExecuteCommandAsync(null, null)).Do(a => executedAction = c.Name));
            catchAllCmd.WhenForAnyArgs(async a => await a.ExecuteCommandAsync(null, null)).Do(a => executedAction = catchAllCmd.Name);


            // -- act
            await sut.ExecuteCommandAsync(sp, args);


            // -- assert
            Assert.Equal(invokedCmd, executedAction);
        }
    }
}
