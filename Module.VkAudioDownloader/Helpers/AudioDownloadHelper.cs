using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Module.VkAudioDownloader.Helpers
{
    public static class AudioDownloadHelper
    {
        public static async Task DownloadAudioAsync(string address, string fileName)
        {
            if (address.Contains(".m3u8"))
            {
                await DownloadM3U8Audio(address, fileName);
            }
            else
            {
                await DownloadMp3Audio(address, fileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName"></param>
        /// <exception cref="Exception">TOO many</exception>
        /// <returns></returns>
        private static async Task DownloadM3U8Audio(string address, string fileName)
        {
            var m3u8String = await WebClientEx.DownloadStringAsync(address);
            
            var ffmpeg = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    FileName = "ffmpeg",
                    Arguments = "-i pipe: -f mp3 -b:a 320k pipe:",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            try
            {
                ffmpeg.Start();
            }
            catch (Win32Exception)
            {
                throw new Exception("Not found \"ffmpeg\" utility.");
            }
            var outputTask = Task.Run(() =>
            {
                using var output = File.OpenWrite(fileName);
                ffmpeg.StandardOutput.BaseStream.CopyTo(output);
            });

            var lines = m3u8String.Split('\n');
            await ConvertM3U8Async(lines, GetBaseUrl(address), block 
                => ffmpeg.StandardInput.BaseStream.Write(block, 0, block.Length));
            ffmpeg.StandardInput.Close();

            await outputTask;
        }
        
        private static string GetBaseUrl(string address)
        {
            var regex = new Regex(@"(^.*/)index\.m3u8");
            var match = regex.Match(address);
            if (!match.Success)
                throw new Exception($"Could not get base url from address \"{address}\".");
                
            return match.Groups[1].Value;
        }

        private static async Task ConvertM3U8Async(IEnumerable<string> m3u8Lines, string baseUrl, Action<byte[]> blockCallback)
        {
            var methodRegex = new Regex("^#EXT-X-KEY:METHOD=(AES-128|NONE)(,URI=\"(.*)\")?");

            var method = "";
            var keyUrl = "";
            foreach (var line in m3u8Lines)
            {
                if (line.Length == 0)
                    continue;
                if (line[0] == '#')
                {
                    if (line.Equals("#EXT-X-ENDLIST"))
                        break;

                    var match = methodRegex.Match(line);
                    if (match.Success)
                    {
                        method = match.Groups[1].Value;
                        keyUrl = match.Groups[3].Value;
                    }
                    continue;
                }
                if (method.Length == 0)
                    continue;

                var data = await WebClientEx.DownloadDataAsync(baseUrl + line);
                if (method.Equals("AES-128"))
                {
                    var key = await WebClientEx.DownloadDataAsync(keyUrl);
                    data = await DecryptAsync(data, key);
                }

                blockCallback(data);
            }
        }

        private static async Task<byte[]> DecryptAsync(byte[] data, byte[] key)
        {
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = new byte[key.Length];

            using var outStream = new MemoryStream();
            using (var crStream = new CryptoStream(new MemoryStream(data),
                aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                await crStream.CopyToAsync(outStream);
            }
            return outStream.ToArray();
        }
        
        private static async Task DownloadMp3Audio(string address, string fileName)
        {
            await WebClientEx.DownloadFileAsync(address, fileName);
        }
    }
}
