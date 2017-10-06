using System;
using Housecode.Net.Abstractions;

namespace Housecode.Net {
	public static class CrossSetup {
		private static readonly Lazy<ISetup> _implementation = new Lazy<ISetup>(CreateSetup, System.Threading.LazyThreadSafetyMode.PublicationOnly);

		public static ISetup Current {
			get {
				var ret = _implementation.Value;
				if (ret == null) {
					throw NotImplementedInReferenceAssembly();
				}
				return ret;
			}
		}

		private static ISetup CreateSetup() {
#if PORTABLE
            return null;
#else
			return new ISetupImplementaion();
#endif
		}

		internal static Exception NotImplementedInReferenceAssembly() {
			return new NotImplementedException("Not implemented in Portable version. Please reference the NuGet package from your main application project.");
		}
	}

	internal class ISetupImplementaion : ISetup {
		public IScreen Screen { get; }
		public IHelper Helper { get; }

		public ISetupImplementaion() {
#if PORTABLE
            Screen = null;
            Helper = null;
#else
			Screen = new ScreenImpl();
			Helper = new HelperImpl();
#endif
		}
	}
}
