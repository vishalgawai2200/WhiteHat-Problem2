﻿using System;

namespace SimpleEchoBot
{
    public class ConversationData
    {
        // The time-stamp of the most recent incoming message.
        public string Timestamp { get; set; }

        // The ID of the user's channel.
        public string ChannelId { get; set; }

        // Track whether we have already asked the user's name
        public bool PromptedUserForName { get; set; } = false;

        public string SessionId { get; set; }

        public IMoMProcessorClient MoMProcessorClient { get; set; }
    }
}
