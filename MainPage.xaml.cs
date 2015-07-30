/**
* Copyright 2015 IBM Corp.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EventSourceNotificationsWP8.Resources;
using IBM.Worklight;
using Newtonsoft.Json.Linq;

namespace EventSourceNotificationsWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private WLClient client;
        internal WindowsChallengeHandler challengeHandler;
        public static MainPage _this;

        // Constructor
        public MainPage()
        {
            _this = this;
            InitializeComponent();

            ReceivedTextBlock.Text = "";
            AboutBox.Text = "IBM MobileFirst Platform\nPush Notification Sample - Event Source";
            client = WLClient.getInstance();
            WLPush push = client.getPush();
            OnReadyToSubscribeListener MyOnReadyListener = new OnReadyToSubscribeListener();
            push.onReadyToSubscribeListener = MyOnReadyListener;
            challengeHandler = new WindowsChallengeHandler();
            client.registerChallengeHandler((BaseChallengeHandler<JObject>)challengeHandler);
            client.connect(new MyConnectResponseListener(this));
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            WLPush push = WLClient.getInstance().getPush();
            MyResponseListener listener = new MyResponseListener("Subscribe");
            push.subscribe("MyAlias", null, listener);
        }

        private void Unsubscribe_Click(object sender, RoutedEventArgs e)
        {
            WLPush push = WLClient.getInstance().getPush();
            MyResponseListener listener = new MyResponseListener("Unsubscribe");
            push.unsubscribe("MyAlias", listener);
        }

        private void IsSubscribed_Click(object sender, RoutedEventArgs e)
        {
            string subscribed = WLClient.getInstance().getPush().isSubscribed("MyAlias") ? "" : " not ";
            MessageBox.Show("You are " + subscribed + " subscribed to push notifications ");
        }

        public void AddTextToReceivedTextBlock(String param)
        {
            ReceivedTextBlock.Text += param;
        }
    }
}