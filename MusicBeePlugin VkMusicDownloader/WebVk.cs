using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VkMusicDownloader.Ex;

namespace VkMusicDownloader
{
    public static class WebVk
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
            string m3u8String = await WebClientEx.DownloadStringAsync(address);
            
            Process ffmpeg = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    FileName = "ffmpeg",
                    Arguments = "-i pipe: -f mp3 -b:a 320k pipe:",
                    UseShellExecute = false
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
            Task outputTask = Task.Run(() =>
            {
                using (FileStream output = File.OpenWrite(fileName))
                {
                    ffmpeg.StandardOutput.BaseStream.CopyTo(output);
                }
            });

            string[] lines = m3u8String.Split('\n');
            await ConvertM3U8Async(lines, GetBaseUrl(address), block 
                => ffmpeg.StandardInput.BaseStream.Write(block, 0, block.Length));
            ffmpeg.StandardInput.Close();

            await outputTask;

            #region Subfunctions

            string GetBaseUrl(string address)
            {
                Regex regex = new Regex(@"(^.*/)index\.m3u8");
                Match match = regex.Match(address);
                if (!match.Success)
                    throw new Exception($"Could not get base url from address \"{address}\".");
                
                return match.Groups[1].Value;
            }

            async Task ConvertM3U8Async(IEnumerable<string> m3u8Lines, string baseUrl, Action<byte[]> blockCallback)
            {
                Regex methodRegex = new Regex("^#EXT-X-KEY:METHOD=(AES-128|NONE)(,URI=\"(.*)\")?");

                string method = "";
                string keyUrl = "";
                foreach (string line in m3u8Lines)
                {
                    if (line.Length == 0)
                        continue;
                    if (line[0] == '#')
                    {
                        if (line.Equals("#EXT-X-ENDLIST"))
                            break;

                        Match match = methodRegex.Match(line);
                        if (match.Success)
                        {
                            method = match.Groups[1].Value;
                            keyUrl = match.Groups[3].Value;
                        }
                        continue;
                    }
                    if (method.Length == 0)
                        continue;

                    byte[] data = await WebClientEx.DownloadDataAsync(baseUrl + line);
                    if (method.Equals("AES-128"))
                    {
                        byte[] key = await WebClientEx.DownloadDataAsync(keyUrl);
                        data = await DecryptAsync(data, key);
                    }

                    blockCallback(data);
                }
            }

            async Task<byte[]> DecryptAsync(byte[] data, byte[] key)
            {
                Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = new byte[key.Length];

                using (MemoryStream outStream = new MemoryStream())
                {
                    using (CryptoStream crStream = new CryptoStream(new MemoryStream(data),
                    aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        await crStream.CopyToAsync(outStream);
                    }
                    return outStream.ToArray();
                }
            }

            #endregion
        }

        private static async Task DownloadMp3Audio(string address, string fileName)
        {
            await WebClientEx.DownloadFileAsync(address, fileName);
        }
    }
}
