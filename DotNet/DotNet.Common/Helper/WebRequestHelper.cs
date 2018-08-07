using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNet.Common
{
    public class WebRequestHelper
    {
        private static WebProxy m_proxy;

        static WebRequestHelper()
        {
            m_proxy = WebProxy.GetDefaultProxy();

            //m_proxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ProxyUserName"],ConfigurationManager.AppSettings["ProxyPassword"],ConfigurationManager.AppSettings["ProxyDomain"]);
        }

        public static string GetResponseContent(string url)
        {
            string content = string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.CreateDefault(new Uri(url)) as HttpWebRequest;
                request.Proxy = m_proxy;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {

                    content = reader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return string.Empty;
            }
            return content;
        }

        public static void DownLoadUK1Order()
        {
            string loginUrl = "http://www.onlinelondonpharmacy.com";
            string downloadUrl = "http://www.onlinelondonpharmacy.com/Affiliate/default2.aspx";
            string username = "";
            string password = "";
            HttpClient httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 9999999;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

            HttpResponseMessage response = httpClient.GetAsync(new Uri(loginUrl)).Result;
            String result = response.Content.ReadAsStringAsync().Result;

            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("__EVENTTARGET", ""));
            paramList.Add(new KeyValuePair<string, string>("__EVENTARGUMENT", ""));
            paramList.Add(new KeyValuePair<string, string>("__VIEWSTATE", "/wEPDwUJNDU1NTUwMjY5ZBgBBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WAQUNY2hrUmVtZW1iZXJNZb5YecEvpZ+nUkJuPaPZty3QCPdO"));
            paramList.Add(new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", "90059987"));
            paramList.Add(new KeyValuePair<string, string>("txtUserId", username));
            paramList.Add(new KeyValuePair<string, string>("txtPassword", password));
            paramList.Add(new KeyValuePair<string, string>("btnSubmit", "Login"));
            response = httpClient.PostAsync(new Uri(loginUrl), new FormUrlEncodedContent(paramList)).Result;
            string result1 = response.Content.ReadAsStringAsync().Result;
            string startViewState = "id=\"__VIEWSTATE\" value=\"";
            result1 = result1.Remove(0, result1.IndexOf(startViewState) + startViewState.Length);
            string viewState = result1.Substring(0, result1.IndexOf("\" />"));

            List<KeyValuePair<String, String>> paramList2 = new List<KeyValuePair<String, String>>();
            paramList2.Add(new KeyValuePair<string, string>("__EVENTTARGET", "ctl00$main$btnDisplay1"));
            paramList2.Add(new KeyValuePair<string, string>("__EVENTARGUMENT", ""));
            paramList2.Add(new KeyValuePair<string, string>("__VIEWSTATE", viewState.Trim()));
            paramList2.Add(new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", "8AA75D42"));
            paramList2.Add(new KeyValuePair<string, string>("ctl00$main$txtDate1", "22/08/2017"));
            paramList2.Add(new KeyValuePair<string, string>("ctl00$main$txtDate2", "27/08/2017"));
            paramList2.Add(new KeyValuePair<string, string>("ctl00$main$drpStatus", "0"));
            paramList2.Add(new KeyValuePair<string, string>("ctl00$main$txtCustomerName", ""));
            paramList2.Add(new KeyValuePair<string, string>("ctl00$main$hdnReShipsOrderID", ""));
            paramList2.Add(new KeyValuePair<string, string>("ctl00$main$hdnOrderTrackingCancel", ""));

            response = httpClient.PostAsync(new Uri(downloadUrl), new FormUrlEncodedContent(paramList2)).Result;
            string result2 = response.Content.ReadAsStringAsync().Result;

            string startViewState2 = "id=\"__VIEWSTATE\" value=\"";
            result2 = result2.Remove(0, result2.IndexOf(startViewState2) + startViewState2.Length);
            string viewState2 = result2.Substring(0, result2.IndexOf("\" />"));
            List<KeyValuePair<String, String>> paramList3 = new List<KeyValuePair<String, String>>();
            paramList3.Add(new KeyValuePair<string, string>("__EVENTTARGET", "ctl00$main$lnkDownloadTracking"));
            paramList3.Add(new KeyValuePair<string, string>("__EVENTARGUMENT", ""));
            paramList3.Add(new KeyValuePair<string, string>("__VIEWSTATE", viewState2.Trim()));
            paramList3.Add(new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", "8AA75D42"));
            paramList3.Add(new KeyValuePair<string, string>("ctl00$main$txtDate1", ""));
            paramList3.Add(new KeyValuePair<string, string>("ctl00$main$txtDate2", ""));
            paramList3.Add(new KeyValuePair<string, string>("ctl00$main$drpStatus", "0"));
            paramList3.Add(new KeyValuePair<string, string>("ctl00$main$txtCustomerName", ""));
            paramList3.Add(new KeyValuePair<string, string>("ctl00$main$hdnReShipsOrderID", ""));
            paramList3.Add(new KeyValuePair<string, string>("ctl00$main$hdnOrderTrackingCancel", ""));

            response = httpClient.PostAsync(new Uri(downloadUrl), new FormUrlEncodedContent(paramList3)).Result;

            var stream = response.Content.ReadAsStreamAsync().Result;


            FileStream fs = new FileStream("D:\\" + DateTime.Now.ToString("yyyyMMhhss") + ".xls", FileMode.Create);
            try
            {
                //fs.Write(stream, 0, stream.Length);
                StreamReader reader = new StreamReader(stream);
                {
                    //get original file name
                
                    string sFileName = "Orders.xls";
                    string sResponseFromServer = reader.ReadToEnd();

                    //save the file

                    Byte[] info = new System.Text.UTF8Encoding(true).GetBytes(sResponseFromServer);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                fs.Close();

            }

            httpClient.Dispose();

        }

        public static async Task<string> Login()
        {
            string url = "";
            Uri address = new Uri(url);
            var postData = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("username", ""),
                                   new KeyValuePair<string, string>("password ", "")
                               };

            HttpContent content = new FormUrlEncodedContent(postData);
            var cookieJar = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookieJar,
                UseCookies = true,
                UseDefaultCredentials = false
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = address
            };


            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
