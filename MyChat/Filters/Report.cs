using System;
using System.Collections.Generic;
using MindLink.Recruitment.MyChat.Data;

namespace MindLink.Recruitment.MyChat.Filters
{
    public class Report : IFilter
    {
        /// <summary>
        /// Displays how many messages each user sent in descending order.
        /// </summary>
        /// <inheritdoc />
        public Conversation Filter(Conversation conversation)
        {
            if (conversation == null)
            {
                throw new ArgumentNullException("There must be a conversation to report on.");
            }

            var listOfUsers = new List<string>();
            var report = new Dictionary<string, int>();

            var activities = new List<Activity>();

            foreach(var message in conversation.messages)
            {
                if(!report.ContainsKey(message.senderId))
                {
                    report.Add(message.senderId, 1);
                    listOfUsers.Add(message.senderId);
                }
                else
                {
                    report[message.senderId] += 1;
                }
            }

            for(int i = 0; i < listOfUsers.Count; i++)
            {
                activities.Add(new Activity(listOfUsers[i], report[listOfUsers[i]]));
            }

            activities.Sort((Activity a, Activity b) =>
            {
                if (a.count > b.count)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });

            conversation.activity = activities;

            var newConversation = conversation;

            return newConversation;
        }
    }
}
