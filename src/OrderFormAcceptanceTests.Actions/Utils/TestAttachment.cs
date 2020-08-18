using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace OrderFormAcceptanceTests.Actions.Utils
{
    public sealed class TestAttachment : Attachment
    {
        public string ContentAsString { get; set; }
        public TestAttachment(Stream contentStream, string fileName, ContentType mediaType): base(contentStream, mediaType)
        {
            using (StreamReader sr = new StreamReader(contentStream))
            {
                this.ContentAsString = sr.ReadToEnd();
            }
            this.Name = fileName;
            this.ContentType = mediaType;
        }
    }
}
