namespace Housecode.Net.API {
	/**
	 * Created by Cahyo Agung
	 * @link      http://www.housecode.net
	 * @copyright Copyright (c) 2017 Housecode
	 * @version   1.0.0
	 */
	public class FileParam {
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
		public byte[] File { get; private set; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
		public string FileName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Housecode.Net.API.FileParam"/> class.
        /// </summary>
        /// <param name="file">File.</param>
        /// <param name="filename">Filename.</param>
		public FileParam(byte[] file, string filename) {
			File = file;
			FileName = filename;
		}
	}
}
