using System;
using Android.Content;
using Android.Runtime;
using Housecode.Net.Abstractions;
using Xamarin.Forms;

namespace Housecode.Net {
    [Preserve(AllMembers = true)]
    public class HelperImpl : IHelper {
        public string BasePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); } }

		public void Log(string message, bool isError = false) {
			if (isError)
				Android.Util.Log.Error("Housecode.Net", message);
			else
				Android.Util.Log.Verbose("Housecode.Net", message);
		}

		public void Share(string title, string url) {
			var uri = new Uri(url);
			var sharingIntent = new Intent();
			sharingIntent.SetAction(Intent.ActionSend);
			sharingIntent.SetType("text/plain");
			sharingIntent.PutExtra(Intent.ExtraText, title + "\n" + uri.AbsoluteUri);
			Forms.Context.StartActivity(Intent.CreateChooser(sharingIntent, "Choose an App to share"));
		}
    }
}
