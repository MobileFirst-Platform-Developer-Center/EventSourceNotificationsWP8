/*
 * COPYRIGHT LICENSE: This information contains sample code provided in source code form. You may copy, modify, and distribute
 * these sample programs in any form without payment to IBM® for the purposes of developing, using, marketing or distributing
 * application programs conforming to the application programming interface for the operating platform for which the sample code is written.
 * Notwithstanding anything to the contrary, IBM PROVIDES THE SAMPLE SOURCE CODE ON AN "AS IS" BASIS AND IBM DISCLAIMS ALL WARRANTIES,
 * EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, ANY IMPLIED WARRANTIES OR CONDITIONS OF MERCHANTABILITY, SATISFACTORY QUALITY,
 * FITNESS FOR A PARTICULAR PURPOSE, TITLE, AND ANY WARRANTY OR CONDITION OF NON-INFRINGEMENT. IBM SHALL NOT BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR OPERATION OF THE SAMPLE SOURCE CODE.
 * IBM HAS NO OBLIGATION TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS OR MODIFICATIONS TO THE SAMPLE SOURCE CODE.
 */

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using IBM.Worklight;

namespace EventSourceNotificationsWP8
{
    class MyResponseListener : WLResponseListener
    {
        private string operation;

        public MyResponseListener(string op)
        {
            operation = op;
        }

        public void onFailure(WLFailResponse response)
        {
            string responseTxt = response.getResponseText();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.AddTextToReceivedTextBlock(operation + "Failed!\n" + responseTxt);
                MainPage._this.PanoramaControl.SetValue(Panorama.SelectedItemProperty, MainPage._this.Console);
                MainPage._this.Measure(new Size());
            });
        }

        public void onSuccess(WLResponse response)
        {
            string responseTxt = response.getResponseJSON().ToString();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.AddTextToReceivedTextBlock(operation + "Success\n" + responseTxt);
                MainPage._this.PanoramaControl.SetValue(Panorama.SelectedItemProperty, MainPage._this.Console);
                MainPage._this.Measure(new Size());
            });
        }
    }
}
