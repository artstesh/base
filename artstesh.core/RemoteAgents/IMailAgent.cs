﻿namespace artstesh.core.RemoteAgents
{
    public interface IMailAgent
    {
        string SendMail(string mailto, string caption, string message, string attachFile = null);
    }
}