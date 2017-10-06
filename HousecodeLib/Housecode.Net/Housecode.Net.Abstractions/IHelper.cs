using System;
namespace Housecode.Net.Abstractions {
    public interface IHelper {
		string BasePath { get; }
		void Log(string message, bool isError = false);
		void Share(string title, string url);
    }
}
