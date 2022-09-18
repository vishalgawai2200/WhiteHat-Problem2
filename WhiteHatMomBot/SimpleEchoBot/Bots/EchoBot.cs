// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.16.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleEchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        private BotState _conversationState;
        private BotState _userState;

        private IStatePropertyAccessor<ConversationData> _conversationStateAccessors;

        private ILogger<EchoBot> _logger;

        public EchoBot(ConversationState conversationState, UserState userState, ILogger<EchoBot> logger)
        {
            _conversationState = conversationState;
            _userState = userState;
            _logger = logger;

        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            var conversationStateAccessors = _conversationState.CreateProperty<ConversationData>(nameof(ConversationData));
            var conversationData = await conversationStateAccessors.GetAsync(turnContext, () => new ConversationData());

            if(conversationData.SessionId == null)
            {
                
                conversationData.SessionId = Guid.NewGuid().ToString();
                _logger.LogInformation($"Create and set session id = {conversationData.SessionId}");

            }

            var momProcessorClient  = new MomProcessorClient(conversationData.SessionId);


            //var replyText = $"Echo: {turnContext.Activity.Text}";

            turnContext.Activity.TextFormat = "xml";
            string text = turnContext.Activity.Text;  //ADDNOTE: TEST1  DELETENODE: 1
            var subs = text.Split(':');

            if (subs == null || subs.Length == 0)
                return;

            string result = string.Empty;
            try
            {
                var action = subs[0].ToUpperInvariant().Trim();
                action = action.Replace("WHITEHATECHOBOT", string.Empty).Trim();
                
                switch (action)
                {
                    case "ADDNOTE":
                        string note = subs[1];
                        momProcessorClient.AddNote(note);
                        break;
                    case "DELETENODE":
                        int index = int.Parse(subs[1]);
                        momProcessorClient.DeleteNode(index);
                        break;
                    case "EMAILMOM":
                        _logger.LogInformation($"sending mail for {conversationData.SessionId}");
                        momProcessorClient.SendMail();
                        result = $"Sent email with subject: WhiteHatBot-MOM-{conversationData.SessionId}-{DateTime.Now}";
                        break;
                    case "GETMOM":
                        var notes = momProcessorClient.GetMinutesOfMeeting();
                        result = "\n" + string.Join("\n", notes);
                        break;
                    case "ADDPARTICIPANTS":
                        string participants = subs[1];
                        _logger.LogInformation($@"Add Participants: {participants}");

                        if(subs.Length >  2)  //href handling from skype
                        {
                            var emailId = subs[2];
                            participants = emailId.Substring(0, emailId.IndexOf('"'));
                        }

                        momProcessorClient.AddParticipants(participants);
                        break;
                    case "RESET":
                        conversationData.SessionId = null;                        
                        break;
                    default:
                        _logger.LogInformation($"no action performed - action : {action}");
                        result = $"No action performed - action : {action}";
                        break;
                }

                result = "Success: " + result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                result = ex.Message;
            }

            //await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);

            await turnContext.SendActivityAsync(MessageFactory.Text(result), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }



        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type == "conversationUpdate")
            {
                var conversationStateAccessors = _conversationState.CreateProperty<ConversationData>(nameof(ConversationData));
                var conversationData = await conversationStateAccessors.GetAsync(turnContext, () => new ConversationData());

                if (conversationData.SessionId == null)
                {
                    conversationData.SessionId = Guid.NewGuid().ToString();

                }
            }


            await base.OnTurnAsync(turnContext, cancellationToken);

            //// Save any state changes that might have occurred during the turn.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            //await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }


    }
}
