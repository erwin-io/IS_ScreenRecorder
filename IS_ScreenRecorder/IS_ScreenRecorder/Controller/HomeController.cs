using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace IS_ScreenRecorder.Controller
{
    public class HomeController : ApiController
    {
        public HomeController()
        {

        }
        //Return HTML home/recorder 
        [HttpGet]
        [ActionName("recorder")]
        public HttpResponseMessage Recorder()
        {
            var response = new HttpResponseMessage();
            // add your homepage html here
            string htmlpath = Directory.GetCurrentDirectory() + @"\web\index.html";
            string htmlContent = File.ReadAllText(htmlpath);
            response.Content = new StringContent(htmlContent);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
        //Return HTML home/startrecord 
        [HttpGet]
        [ActionName("startrecord")]
        public HttpResponseMessage StartRecord()
        {
            var response = new HttpResponseMessage();
            // add your homepage html here
            string htmlpath = Directory.GetCurrentDirectory() + @"\web\index.html";
            string htmlContent = File.ReadAllText(htmlpath);
            response.Content = new StringContent(htmlContent);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
