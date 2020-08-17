using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFormAcceptanceTests.Actions.Utils
{
    public sealed class Email
    {
        public string Id { get; set; }
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string PlainTextBody { get; set; }

        public string HtmlBody { get; set; }
    }
}
