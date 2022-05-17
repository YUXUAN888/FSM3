using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SquareMinecraftLauncher.Core.Forge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace SquareMinecraftLauncher.Core.fabricmcs
{
    internal class fabricmcInstall
    {
        internal string FileExist(string file)
        {
            if (File.Exists(file))
            {
                return null;
            }
            return file;
        }

        public string GetFile(string path)
        {
            if (this.FileExist(path) == null)
            {
                try
                {
                    StreamReader reader1 = new StreamReader(path);
                    string str = reader1.ReadToEnd();
                    reader1.Close();
                    return str;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return "";
        }
        public void wj(string path, string text)
        {
            try
            {
                File.WriteAllText(path, text, Encoding.UTF8);
            }
            catch (Exception exception1) { }
        }
        List<ForgeY.LibrariesItem> libItem = new List<ForgeY.LibrariesItem>();
        Download web = new Download();
        public void GetLoaderVersionJson(string loaderVersion, string version, string Idversion)
        {
            var json = web.getHtml("https://maven.fabricmc.net/net/fabricmc/fabric-loader/" + loaderVersion + "/fabric-loader-" + loaderVersion + ".json");
            if (json != null)
            {
                var jo = JsonConvert.DeserializeObject<fabricmcJson.Root>(json);
                foreach (var i in jo.libraries.common)//增加fabicmcLibraries
                {
                    ForgeY.LibrariesItem lib = new ForgeY.LibrariesItem();
                    lib.name = i.name;
                    if (i.url != null)
                    {
                        ForgeY.Downloads downloads = new ForgeY.Downloads();
                        ForgeY.Artifact Artifact = new ForgeY.Artifact();
                        downloads.artifact = Artifact;
                        if (i.url == "http://repo.maven.apache.org/maven2/")
                        {
                            i.url = "https://repo.maven.apache.org/maven2/";
                        }
                        Artifact.url = i.url;
                        lib.downloads = downloads;
                    }
                    libItem.Add(lib);
                }
                /*/增加fabricmc本身/*/
                ForgeY.LibrariesItem lib1 = new ForgeY.LibrariesItem();
                lib1.name = "net.fabricmc:fabric-loader:" + loaderVersion;
                ForgeY.Downloads downloads1 = new ForgeY.Downloads();
                ForgeY.Artifact Artifact1 = new ForgeY.Artifact();
                downloads1.artifact = Artifact1;
                Artifact1.url = "https://maven.fabricmc.net/";
                lib1.downloads = downloads1;
                libItem.Add(lib1);
                ForgeY.LibrariesItem lib2 = new ForgeY.LibrariesItem();
                lib2.name = "net.fabricmc:intermediary:" + Idversion;
                ForgeY.Downloads downloads2 = new ForgeY.Downloads();
                ForgeY.Artifact Artifact2 = new ForgeY.Artifact();
                downloads2.artifact = Artifact2;
                Artifact2.url = "https://maven.fabricmc.net/";
                lib2.downloads = downloads2;
                libItem.Add(lib2);
                /*/结束/*/
                var json1 = GetFile(Directory.GetCurrentDirectory() + @"\.minecraft\versions\" + version + @"\" + version + ".json");
                var jo1 = JsonConvert.DeserializeObject<ForgeY.Root>(json1);
                foreach (var i in jo1.libraries)//增加版本libraries
                {
                    ForgeY.LibrariesItem lib = new ForgeY.LibrariesItem();
                    lib.name = i.name;
                    lib.downloads = i.downloads;
                    lib.url = i.url;
                    lib.natives = i.natives;
                    libItem.Add(lib);
                }
                string mainclass = jo.mainClass.client;//fabricmcMainClass
                string Arguments = ArgumentsJson(json1); //组成新的Arguments
                Arguments = Arguments.Substring(0, Arguments.Length - 1);
                wj(Directory.GetCurrentDirectory() + @"\.minecraft\versions\" + version + @"\" + version + ".json", newJson(libItem, Arguments, mainclass, jo1));
                return;
            }
        }
        internal string ArgumentsJson(string json)
        {
            var obj2 = (JObject)JsonConvert.DeserializeObject(json);
            string str2 = null;
            for (int i = 0; (obj2["arguments"]["game"].ToArray<JToken>().Length - 1) > 0; i++)
            {
                try
                {
                    obj2["arguments"]["game"][i].ToString();
                    if ((obj2["arguments"]["game"][i].ToString()[0] == '-') || (obj2["arguments"]["game"][i].ToString()[0] == '$'))
                    {
                        str2 = str2 + "\"" + obj2["arguments"]["game"][i].ToString() + "\",";
                        continue;
                    }
                    if (obj2["arguments"]["game"][i - 1].ToString()[0] == '-')
                    {
                        str2 = str2 + "\"" + obj2["arguments"]["game"][i].ToString() + "\",";
                        continue;
                    }
                }
                catch (Exception)
                {
                }
                break;
            }
            return str2;
        }
        public string newJson(List<Forge.ForgeY.LibrariesItem> libraries, string Arguments, string mainClass, Forge.ForgeY.Root r)
        {
            string id = "\"id\":\"" + r.assetIndex.id;
            string size = "\"size\":\"" + r.assetIndex.size;
            string url = "\"url\":\"" + r.assetIndex.url;
            string ret = "{\"arguments\":{\"game\":[" + Arguments + "]},\"assetIndex\":{" + id + "\"," + size + "\"," + url + "\"},\"assets\":\"" + r.assets + "\",";
            ret += "\"downloads\":{\"Client\":{\"sha1\":\"" + r.downloads.client.sha1 + "\",\"size\":\"" + r.downloads.client.size + "\",\"url\":\"" + r.downloads.client.url + "\"}},";
            ret += "\"id\":\"" + r.id + "\",";
            ret += "\"libraries\":[";
            foreach (var i in libraries)//将新组成的libraries数组写成json
            {
                if (i.downloads == null)
                {
                    ret += "{\"downloads\":{\"artifact\":{\"url\":\"" + "https://libraries.minecraft.net/\"}},";
                }
                else
                {
                    ret += "{\"downloads\":{\"artifact\":{\"url\":\"" + i.downloads.artifact.url + "\"}},";
                }
                ret += "\"name\":\"" + i.name + "\"";
                if (i.natives != null)
                {
                    if (i.natives.windows != null)
                    {
                        ret += ",\"natives\":{\"windows\":\"natives - windows\"}";
                    }
                }
                ret += "},";
            }
            ret = ret.Substring(0, ret.Length - 1);
            ret += "],";
            ret += "\"mainClass\":\"" + mainClass + "\"}";
            return ret;
        }
    }
}