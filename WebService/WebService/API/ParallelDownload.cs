using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebService.API {
    public static class ParallelDownload {
        /// <summary>
        /// Download the specified URLs to file.
        /// </summary>
        /// <returns>The download.</returns>
        /// <param name="model">Model.</param>
        public static Task<bool> Download(List<ParallelDownloadModel> model){
            return Task.Run(() => {
                var res = Parallel.ForEach(model, async (obj) => {
                    using (var web = new WebService()) {
                        try {
                            var data = await web.GetByteArrayData(obj.URL);
                            if (data != null) {
                                Helper.WriteAllBytes(data, obj.FilePath);
                            }
                        } catch(Exception ex) {
                            Helper.Log(ex);
                        }
                    }
                });
                return res.IsCompleted;
            });
        }
    }

    public class ParallelDownloadModel {
        public string URL { get; set; }
        public string FilePath { get; set; }
    }
}
