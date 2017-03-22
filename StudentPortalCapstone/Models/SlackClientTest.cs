using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class SlackClientTest
    {
        void TestPostMessage()
        {
            string urlWithAccessToken = "https://hooks.slack.com/services/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";

            SlackClient client = new SlackClient(urlWithAccessToken);

            client.PostMessage(username: "olivia",
                       text: "THIS IS A TEST MESSAGE!!",
                       channel: "#testingapi");
        }
    }
}