using System;
namespace Housecode.Net.Abstractions {
    public interface ISetup {
        IScreen Screen { get; }
        IHelper Helper { get; }
    }
}
