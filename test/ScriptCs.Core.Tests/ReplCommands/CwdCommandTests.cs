﻿using Moq;
using ScriptCs.Contracts;
using ScriptCs.ReplCommands;
using Xunit;

namespace ScriptCs.Tests.ReplCommands
{
    public class CwdCommandTests
    {
        public class CommandNameProperty
        {
            [Fact]
            public void ReturnsCwd()
            {
                // act
                var cmd = new CwdCommand(new Mock<IConsole>().Object);

                // assert
                Assert.Equal("cwd", cmd.CommandName);
            }
        }

        public class ExecuteMethod
        {
            [Fact]
            public void PrintsCurrentWorkingDirectoryToConsole()
            {
                // arrange
                var console = new Mock<IConsole>();
                var fs = new Mock<IFileSystem>();
                var executor = new Mock<IScriptExecutor>();

                fs.Setup(x => x.CurrentDirectory).Returns(@"c:\dir");
                executor.Setup(x => x.FileSystem).Returns(fs.Object);

                var cmd = new CwdCommand(console.Object);

                // act
                var result = cmd.Execute(executor.Object, null);

                // assert
                console.Verify(x => x.WriteLine(@"c:\dir"));
            }
        }
    }
}