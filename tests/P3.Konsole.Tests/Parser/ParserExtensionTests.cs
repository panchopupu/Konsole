using P3.Konsole.Parser;
using P3.Konsole.Tests.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3.Konsole.Tests.Parser
{
    public class ParserExtensionTests
    {
        [Fact]
        public void When_RequiredParameterNotSpecified_Expect_Throw() {
            // -- arrange

            // -- act
            Action action = () => ParserExtensions.ParseCommandArgs<TestParameters>(new string[] { });

            // -- assert
            Assert.Throws<MissingParameterParseException>(action);
        }

        [Theory]
        [InlineData("-s required -d -v")]
        [InlineData("-s required -d -f")]
        [InlineData("-s required -d")]
        [InlineData("-d -s required")]
        public void When_ValueNotSpecified_Expect_Throw(string args) {
            // -- arrange

            // -- act
            Action action = () => ParserExtensions.ParseCommandArgs<TestParameters>(args.Split(" "));

            // -- assert
            Assert.Throws<MissingValueParseException>(action);
        }

        [Theory]
        [InlineData("-s required -d -v")]
        public void When_RequestingStringArray_Expect_ReturnSameArray(string args) {
            // -- arrange
            var arr = args.Split(" ");

            // -- act
            var result =  ParserExtensions.ParseCommandArgs<string[]>(arr);

            // -- assert
            Assert.Same(arr, result);
        }

        [Theory]
        [InlineData("-s required -zzz ")]
        [InlineData("-s required --zzzz ")]
        [InlineData("-s required zzzz ")]
        public void When_UnknownParameter_Expect_Throw(string args) {
            // -- arrange

            // -- act
            Action action = () => ParserExtensions.ParseCommandArgs<TestParameters>(args.Split(" "));

            // -- assert
            Assert.Throws<UnknownParameterParseException>(action);
        }

        [Theory]
        [InlineData("-s required -f")]
        public void When_HasValueFalsePassed_Expect_ValueTrue(string args) {
            // -- arrange

            // -- act
            var result = ParserExtensions.ParseCommandArgs<TestParameters>(args.Split(" "));

            // -- assert
            Assert.True(result.Flag);
        }

        [Theory]
        [InlineData("-s required")]
        public void When_HasValueFalseNotPassed_Expect_ValueFalse(string args) {
            // -- arrange

            // -- act
            var result = ParserExtensions.ParseCommandArgs<TestParameters>(args.Split(" "));

            // -- assert
            Assert.False(result.Flag);
        }
    }
}
