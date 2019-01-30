﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone
{
    //User Class
    class Program
    {
        static void Main(string[] args)
        {
            // networkHub = NetworkHub.GetHub();

            Network vodaphoneNetwork = new Network();
            vodaphoneNetwork.NetworkName = "Vodaphone";
            NetworkHub.GlobalNetwork.registerNetwork(vodaphoneNetwork);
           
            Phone phone;//create a general variable

            phone = new Android();
            phone.setNetwork(vodaphoneNetwork);
            phone.setSubscriberNumber("0000000");
          

            phone = new ApplePhone();
            phone.setNetwork(vodaphoneNetwork);
            phone.setSubscriberNumber("1111111");
         


            phone = new Android();
            phone.setNetwork(vodaphoneNetwork);
            phone.setSubscriberNumber("2222222");
           


            Network airtelNetwork = new Network();
            airtelNetwork.NetworkName = "Airtel";
            NetworkHub.GlobalNetwork.registerNetwork(airtelNetwork);

            //List <Phone> phonelist = new List<Phone>();


            phone = new ApplePhone();
            phone.setNetwork(airtelNetwork);
            phone.setSubscriberNumber("33333");
            // airtelNetwork.Phonelist.Add(phone);
            //airtelNetwork.registerPhone(phone);


            phone = new Android();
            phone.setNetwork(airtelNetwork);
            phone.setSubscriberNumber("44444");
            //airtelNetwork.registerPhone(phone);

            phone = new ApplePhone();
            phone.setNetwork(airtelNetwork);
            phone.setSubscriberNumber("55555");
           // airtelNetwork.registerPhone(phone);

            phone = new ApplePhone();
            phone.setNetwork(airtelNetwork);
            phone.setSubscriberNumber("1111111");
           // airtelNetwork.registerPhone(phone);


            //airtelNetwork.Phonelist[1].Call("44444", 23, "hello");
            //airtelNetwork.Phonelist[2].Call("55555", 21, "hej");
            //airtelNetwork.Phonelist[2].Call("456", 20, "Please send an Ambulance quickly!");
            //airtelNetwork.Phonelist[2].Call("76767676", 88, "ghgjhghjg");

            // airtelNetwork.Phonelist[1].Call("1111111", 11, "outer network call");
            Phone p = airtelNetwork.getPhone("44444");
            p.Call("1111111", 11, "outer network call");

            //airtelNetwork.getPhone("44444").Call("1111111", 11, "outer network call");

            airtelNetwork.Phonelist[1].Call("55555", 11, "inter network call");


            //myPhone = airtelNetwork.Phonelist[2];
            //log = myPhone.GetCallLog();

            Console.WriteLine(airtelNetwork.Phonelist[1].GetCallLog());

            Console.ReadKey();
        }
    }

    public class NetworkHub
    {
        private static List<Network> networks = new List<Network>();

        private static NetworkHub networkHub = null; //only one instance should be available


        private NetworkHub()//prohibits direct object creation
        {

        }

        public static NetworkHub GlobalNetwork
        {
            get
            {
                if (networkHub == null)
                    networkHub = new NetworkHub();

                return networkHub;
            }
        }

        public List<Network> Networks
        {
            get
            {
                return NetworkHub.networks;
            }
        }

        public void registerNetwork(Network network)
        {
            networks.Add(network);
        }

    }

    public class Network
    {
        public string NetworkName;
        public List<Phone> Phonelist = new List<Phone>();

        // bool registerPhoneNumber = false;
        public bool registerPhone(Phone phone)
        {
            bool match = false;
            for (int i = 0; i <= NetworkHub.GlobalNetwork.Networks.Count - 1; i++)
            {
                for (int k = 0; k <= NetworkHub.GlobalNetwork.Networks[i].Phonelist.Count - 1; k++)
                {
                    if (NetworkHub.GlobalNetwork.Networks[i].Phonelist[k].getSubscriberNumber() == phone.getSubscriberNumber())
                    {

                        Console.WriteLine("Duplicate registration detected for: " + phone.getSubscriberNumber());
                        match = true;
                        break;

                    }

                }

            }
            if (match == false)
            {

                Phonelist.Add(phone);
                //registerPhoneNumber = true;
            }
            //return registerPhoneNumber;
            return !match;

        }

        public Phone getPhone(string PhoneNumber)
        {
            for (int i = 0; i <= Phonelist.Count - 1; i++)
            {
                if (Phonelist[i].getSubscriberNumber() == PhoneNumber)
                {
                    return Phonelist[i];
                }
            }

            return null;
        }

    }

    public class EmmergencyService
    {
        public static string[] immergencyPhoneNumberList = { "999", "112", "456" };
        public static string[] immergencyServiceOrganisation = { "Police", "Fire Service", "Ambulance" };

    }

    public abstract class Phone
    {
        protected Network network;
        public virtual void setNetwork(Network network)
        {
            this.network = network;
            network.registerPhone(this);

        }
        public virtual Network getNetwork()
        {
            return this.network;
        }

        protected string SubscriberNumber;
        public virtual void setSubscriberNumber(string subscribeNumber)
        {
            this.SubscriberNumber = subscribeNumber;
        }
        public virtual string getSubscriberNumber()
        {
            return SubscriberNumber;
        }


        //List<CallLog> CallLogList { get; set; }
        // void setCallLogList(List<CallLog> callLogs);
        abstract public List<CallLog> getCallLogList();
        abstract public void Call(string receiverSubcriberNumber, int durationInSec, string message);
        abstract public string GetCallLog();
    }

    public class CallLog
    {
        public string subcriberNumber;
        public string callType;
        public int durationInSec;
        public string message;
    }

    public class Android : Phone
    {
        //public   List<Phone> Phonelist = new List<Phone>();//Chaining,anonymous function
        //public Network Network { get; set; }

        public override void setNetwork(Network network)
        {
            base.setNetwork(network);

            Console.WriteLine("Play Store Log: " + this.SubscriberNumber + ", registered with "+ network.NetworkName);

        }

        //public string SubcriberNumber { get; set; }

        //public List<CallLog> CallLogList { get; set; } = new List<CallLog>();
        private List<CallLog> callLogList = new List<CallLog>();
      
        public override List<CallLog> getCallLogList()
        {
            return callLogList;
        }

        public override void Call(string receiverSubcriberNumber, int durationInSec, string message)
        {
            CallLog callLog;
            if (this.SubscriberNumber == receiverSubcriberNumber)
            {
                callLog = new CallLog();
                callLog.subcriberNumber = this.SubscriberNumber;
                string x = "OutGoing";
                callLog.callType = x.Substring(0, 3);
                callLog.durationInSec = 0;
                callLog.message = null;
                callLogList.Add(callLog);
                Console.WriteLine("Self call is not possible");
                return;
            }
            bool match = false;
            for (int i = 0; i <= NetworkHub.GlobalNetwork.Networks.Count - 1; i++)
            {
                for (int j = 0; j <= NetworkHub.GlobalNetwork.Networks[i].Phonelist.Count - 1; j++)
                {

                    if (NetworkHub.GlobalNetwork.Networks[i].Phonelist[j].getSubscriberNumber() == receiverSubcriberNumber)
                    {
                        //outgoing call log
                        CallLog callLogOutGoing = new CallLog();
                        callLogOutGoing.subcriberNumber = receiverSubcriberNumber;
                        string x = "OutGoing";
                        callLogOutGoing.callType = x.Substring(0, 3);
                        callLogOutGoing.durationInSec = durationInSec;
                        callLogOutGoing.message = message;
                        this.callLogList.Add(callLogOutGoing);

                        //incoming call log
                        CallLog callLogIncoming = new CallLog();
                        callLogIncoming.subcriberNumber = this.SubscriberNumber;
                        string y = "Incoming";
                        callLogIncoming.callType = y.Substring(0, 2);
                        callLogIncoming.durationInSec = durationInSec;
                        callLogIncoming.message = message;
                        NetworkHub.GlobalNetwork.Networks[i].Phonelist[j].getCallLogList().Add(callLogIncoming);///
                        match = true;

                    }

                }
            }
            for (int i = 0; i <= EmmergencyService.immergencyPhoneNumberList.Length - 1; i++)
            {
                if (EmmergencyService.immergencyPhoneNumberList[i] == receiverSubcriberNumber)
                {
                    CallLog callLogOutGoing = new CallLog();
                    callLogOutGoing.subcriberNumber = receiverSubcriberNumber;
                    string x = "OutGoing";
                    callLogOutGoing.callType = x.Substring(0, 3);
                    callLogOutGoing.durationInSec = durationInSec;
                    callLogOutGoing.message = EmmergencyService.immergencyServiceOrganisation[i] + ": " + message;
                    callLogList.Add(callLogOutGoing);
                    match = true;
                }
            }
            if (match == false)
            {
                callLog = new CallLog();
                callLog.subcriberNumber = receiverSubcriberNumber;
                string x = "Outgoing";
                callLog.callType = x.Substring(0, 3);
                callLog.durationInSec = 0;
                callLog.message = null;
                callLogList.Add(callLog);
                Console.WriteLine("The subscriber number " + receiverSubcriberNumber + " not found!");
            }

        }

        public override string GetCallLog()
        {
            string myCallLog = "";
            myCallLog = myCallLog + "Printing calllog for " + this.SubscriberNumber + "\n";
            myCallLog = myCallLog + "Call Type " + "Subscription Number" + "Duration " + "Message" + "\n";

            for (int x = 0; x <= this.callLogList.Count - 1; x++)
            {
                myCallLog = myCallLog + this.callLogList[x].callType + " " + this.callLogList[x].subcriberNumber + " " + this.callLogList[x].durationInSec + " " + this.callLogList[x].message + "\n";

            }
            return myCallLog;
        }

        public bool Bluetooth()
        {
            return true;
        }


    }

    public class ApplePhone : Phone
    {
      

        //public string SubcriberNumber { get; set; }

        //public List<CallLog> CallLogList { get; set; } = new List<CallLog>();
        private List<CallLog> callLogList = new List<CallLog>();

        public override List<CallLog> getCallLogList()
        {
            return callLogList;
        }

        public override void Call(string receiverSubcriberNumber, int durationInSec, string message)
        {
            CallLog callLog;
            if (this.SubscriberNumber == receiverSubcriberNumber)
            {
                callLog = new CallLog();
                callLog.subcriberNumber = this.SubscriberNumber;
                string x = "OutGoing";
                callLog.callType = x.Substring(0, 3);
                callLog.durationInSec = 0;
                callLog.message = null;
                callLogList.Add(callLog);
                Console.WriteLine("Self call is not possible");
                return;
            }
            bool match = false;
            for (int i = 0; i <= NetworkHub.GlobalNetwork.Networks.Count - 1; i++)
            {
                for (int j = 0; j <= NetworkHub.GlobalNetwork.Networks[i].Phonelist.Count - 1; j++)
                {

                    if (NetworkHub.GlobalNetwork.Networks[i].Phonelist[j].getSubscriberNumber() == receiverSubcriberNumber)
                    {
                        //outgoing call log
                        CallLog callLogOutGoing = new CallLog();
                        callLogOutGoing.subcriberNumber = receiverSubcriberNumber;
                        string x = "OutGoing";
                        callLogOutGoing.callType = x.Substring(0, 3);
                        callLogOutGoing.durationInSec = durationInSec;
                        callLogOutGoing.message = message;
                        this.callLogList.Add(callLogOutGoing);

                        //incoming call log
                        CallLog callLogIncoming = new CallLog();
                        callLogIncoming.subcriberNumber = this.SubscriberNumber;
                        string y = "Incoming";
                        callLogIncoming.callType = y.Substring(0, 2);
                        callLogIncoming.durationInSec = durationInSec;
                        callLogIncoming.message = message;
                        NetworkHub.GlobalNetwork.Networks[i].Phonelist[j].getCallLogList().Add(callLogIncoming);///
                        match = true;

                    }

                }
            }
            for (int i = 0; i <= EmmergencyService.immergencyPhoneNumberList.Length - 1; i++)
            {
                if (EmmergencyService.immergencyPhoneNumberList[i] == receiverSubcriberNumber)
                {
                    CallLog callLogOutGoing = new CallLog();
                    callLogOutGoing.subcriberNumber = receiverSubcriberNumber;
                    string x = "OutGoing";
                    callLogOutGoing.callType = x.Substring(0, 3);
                    callLogOutGoing.durationInSec = durationInSec;
                    callLogOutGoing.message = EmmergencyService.immergencyServiceOrganisation[i] + ": " + message;
                    callLogList.Add(callLogOutGoing);
                    match = true;
                }
            }
            if (match == false)
            {
                callLog = new CallLog();
                callLog.subcriberNumber = receiverSubcriberNumber;
                string x = "Outgoing";
                callLog.callType = x.Substring(0, 3);
                callLog.durationInSec = 0;
                callLog.message = null;
                callLogList.Add(callLog);
                Console.WriteLine("The subscriber number " + receiverSubcriberNumber + " not found!");
            }

        }

        public override string GetCallLog()
        {
            string myCallLog = "";
            myCallLog = myCallLog + "Printing calllog for " + this.SubscriberNumber + "\n";
            myCallLog = myCallLog + "Call Type " + "Subscription Number" + "Duration " + "Message" + "\n";

            for (int x = 0; x <= this.callLogList.Count - 1; x++)
            {
                myCallLog = myCallLog + this.callLogList[x].callType + " " + this.callLogList[x].subcriberNumber + " " + this.callLogList[x].durationInSec + " " + this.callLogList[x].message + "\n";

            }
            return myCallLog;
        }


    }
}



